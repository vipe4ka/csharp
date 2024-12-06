using System.Security.Cryptography;
using System.Numerics;

namespace AlexeevaLazareva
{
    public class RSAKeys
    {
        private BigInteger _publicKey;
        private BigInteger _privateKey;
        private BigInteger _mod;

        public RSAKeys()
        {
            GenerateKeys();
        }

        public RSAKeys(BigInteger publicKey, BigInteger privateKey, BigInteger mod)
        {
            this.publicKey = publicKey;
            this.privateKey = privateKey;
            this.mod = mod;
        }

        public BigInteger publicKey
        {
            get { return _publicKey; }
            set
            {
                if (value <= 0) throw new ArgumentException("Value must be a positive number.");
                _publicKey = value;
            }
        }
        public BigInteger privateKey
        {
            get { return _privateKey; }
            set
            {
                if (value <= 0) throw new ArgumentException("Value must be a positive number.");
                _privateKey = value;
            }
        }
        public BigInteger mod
        {
            get { return _mod; }
            set
            {
                if (value <= 0) throw new ArgumentException("Value must be a positive number.");
                _mod = value;
            }
        }

        public static BigInteger PowMod(BigInteger a, BigInteger x, BigInteger mod)
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

        private void GenerateKeys()
        {
            BigInteger p = GeneratePrime();
            BigInteger q = GeneratePrime();
            BigInteger n = p * q;
            BigInteger phi = (p - 1) * (q - 1);
            BigInteger e = 65537;
            BigInteger d = ModInverse(e, phi);

            _publicKey = e;
            _privateKey = d;
            _mod = n;
        }

        //расширенный алгоритм Евклида
        private static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m, y = 0, x = 1, a0 = a, m1 = m;
            if (m1 == 1) return 0;
            while (a0 > 1)
            {
                BigInteger q = a0 / m1, t = m1;
                m1 = a0 % m1;
                a0 = t;
                t = y;
                y = x - q * y;
                x = t;
            }
            if (x < 0) x += m0;

            return x;
        }

        private static BigInteger GeneratePrime()
        {
            BigInteger primeNum;
            do
            {
                primeNum = GenerateRandBigNum();
            } while (!IsProbablyPrime(primeNum));
            return primeNum;
        }

        private static BigInteger GenerateRandBigNum()
        {
            int bits = 1024; //разрядность числа
            int bytes = (bits + 7) / 8;

            byte[] randBytes = new byte[bytes]; //буфер для случайных байт
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randBytes); //криптостойкий генератор
            }
            randBytes[randBytes.Length - 1] |= 0x80; //установка старшего бита для создания числа длиной в bits

            return new BigInteger(randBytes, isUnsigned: true, isBigEndian: true);
        }

        //тест Миллера-Рабина
        private static bool IsProbablyPrime(BigInteger n, int k = 100) //k - количество шагов проверки
        {
            if (n % 2 == 0) return false;

            BigInteger t = n - 1, s = 0;
            while (t % 2 == 0)
            {
                t /= 2;
                s++;
            }

            Random rnd = new Random();
            for (int i = 0; i < k; i++)
            {
                //генерируем a в отрезке [2; n - 2]
                byte[] randBytes = (n - 3).ToByteArray();
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randBytes); //криптостойкий генератор
                }
                BigInteger rndNum = new BigInteger(randBytes, isUnsigned: true, isBigEndian: true);

                BigInteger a = (rndNum % (n - 3)) + 2;
                BigInteger x = PowMod(a, t, n);
                if (x == 1 || x == n - 1) continue;
                for (int j = 0; j < s - 1; j++)
                {
                    x = PowMod(x, 2, n);
                    if (x == n - 1) break;
                    if (x == 1) return false;
                }
                if (x != n - 1) return false;
            }
            return true;
        }
    }
}