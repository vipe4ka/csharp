using System;
using System.Numerics;
using System.Security.Cryptography;

public class MyECDH
{
    // Параметры кривой nistP521
    private static string gxHex = "00C6858E06B70404E9CD9E3ECB662395B4429C648139053FB521F828AF606B4D3DBAA14B5E77EFE75928FE1DC127A2FFA8DE3348B3C1856A429BF97E7E31C2E5BD66";
    private static string gyHex = "011839296A789A3BC0045C8A5FB42C7D1BD998F54449579B446817AFBD17273E662C97EE72995EF42640C550B9013FAD0761353C7086A272C24088BE94769FD16650";
    private static string aHex = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC";
    private static string primeHex = "01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
    private static string orderHex = "01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFA51868783BF2F966B7FCC0148F709A5D03BB5C9B8899C47AEBB6FB71E91386409";

    // Преобразуем параметры в BigInteger
    private static BigInteger curveGX = BigInteger.Parse(gxHex, System.Globalization.NumberStyles.HexNumber);
    private static BigInteger curveGY = BigInteger.Parse(gyHex, System.Globalization.NumberStyles.HexNumber);
    private static BigInteger curveA = BigInteger.Parse(aHex, System.Globalization.NumberStyles.HexNumber);
    private static BigInteger curvePrime = BigInteger.Parse(primeHex, System.Globalization.NumberStyles.HexNumber);
    private static BigInteger curveOrder = BigInteger.Parse(orderHex, System.Globalization.NumberStyles.HexNumber);

    public MyECDH()
    {
        PrivateKey = GeneratePrivateKey();
        PublicKey = GeneratePublicKey();
    }

    public struct ECPoint
    {
        public BigInteger X { get; }
        public BigInteger Y { get; }
        public bool IsInfinity { get; }

        public ECPoint(BigInteger x, BigInteger y)
        {
            X = x;
            Y = y;
            IsInfinity = false;
        }

        public static ECPoint Infinity = new ECPoint(true);

        private ECPoint(bool isInfinity)
        {
            X = 0;
            Y = 0;
            IsInfinity = isInfinity;
        }
    }

    public ECPoint PublicKey { get; set; }
    public BigInteger PrivateKey { get; set; }

    // Генерация приватного ключа
    private BigInteger GeneratePrivateKey()
    {
        byte[] privateKeyBytes = new byte[66]; // Длина порядка кривой nistP521
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(privateKeyBytes);
        }

        BigInteger privateKey = new BigInteger(privateKeyBytes, isUnsigned: true, isBigEndian: true);

        return privateKey % curveOrder;
    }

    // Генерация публичного ключа
    private ECPoint GeneratePublicKey()
    {
        return Multiply(new ECPoint(curveGX, curveGY), PrivateKey);
    }

    // Умножение точки на скаляр
    private ECPoint Multiply(ECPoint point, BigInteger scalar)
    {
        ECPoint result = ECPoint.Infinity;
        ECPoint addend = point;

        while (scalar > 0)
        {
            if ((scalar & 1) != 0)
            {
                result = AddPoints(result, addend);
            }
            addend = DoublePoint(addend);
            scalar >>= 1;
        }

        return result;
    }

    // Сложение двух точек на кривой
    private ECPoint AddPoints(ECPoint p1, ECPoint p2)
    {
        if (p1.IsInfinity) return p2;
        if (p2.IsInfinity) return p1;
        if (p1.X == p2.X && p1.Y != p2.Y) return ECPoint.Infinity;

        BigInteger lambda;

        if (p1.X == p2.X && p1.Y == p2.Y)
        {
            lambda = (3 * p1.X * p1.X + curveA) * ModInverse(2 * p1.Y, curvePrime) % curvePrime;
        }
        else
        {
            lambda = (p2.Y - p1.Y) * ModInverse(p2.X - p1.X, curvePrime) % curvePrime;
        }
        BigInteger x3 = (lambda * lambda - p1.X - p2.X) % curvePrime;
        BigInteger y3 = (lambda * (p1.X - x3) - p1.Y) % curvePrime;

        return new ECPoint((x3 + curvePrime) % curvePrime, (y3 + curvePrime) % curvePrime);
    }

    // Умножение точки на 2
    private ECPoint DoublePoint(ECPoint point)
    {
        if (point.IsInfinity) return point;

        BigInteger lambda = (3 * point.X * point.X + curveA) * ModInverse(2 * point.Y, curvePrime) % curvePrime;
        BigInteger x3 = (lambda * lambda - 2 * point.X) % curvePrime;
        BigInteger y3 = (lambda * (point.X - x3) - point.Y) % curvePrime;

        return new ECPoint((x3 + curvePrime) % curvePrime, (y3 + curvePrime) % curvePrime);
    }

    // Нахождение обратного элемента по модулю
    private BigInteger ModInverse(BigInteger value, BigInteger modulus)
    {
        BigInteger result = PowMod(value, modulus - 2, modulus);
        return result < 0 ? result + modulus : result;
    }

    // Получение общего секрета на основе публичного ключа другой стороны
    public byte[] DeriveSharedSecret(ECPoint otherPublicKey)
    {
        ECPoint sharedPoint = Multiply(otherPublicKey, PrivateKey);
        return sharedPoint.X.ToByteArray();
    }

    // Модульное возведение в степень
    private static BigInteger PowMod(BigInteger a, BigInteger x, BigInteger mod)
    {
        BigInteger result = 1;
        a %= mod;

        while (x > 0)
        {
            if ((x & 1) == 1)
                result = (result * a) % mod;

            a = (a * a) % mod;
            x >>= 1;
        }
        return result;
    }
}
