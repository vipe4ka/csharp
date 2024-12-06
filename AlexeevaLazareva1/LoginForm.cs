using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace AlexeevaLazareva1
{
    public partial class LoginForm : Form
    {
        public event Action<string> UserLoggedIn;
        private const string UsersFile = "users.xml";
        public LoginForm()
        {
            InitializeComponent();
            if (!File.Exists(UsersFile))
            {
                var usersDoc = new XDocument(new XElement("Users"));
                usersDoc.Save(UsersFile);
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            if (ValidateUser(login, password))
            {
                UserLoggedIn?.Invoke(login);
                Close();
            }
            else
            {
                MessageBox.Show("Неверные данные для входа.", "Ошибка входа");
            }
        }

        private bool ValidateUser(string login, string password)
        {
            XElement usersXml = XElement.Load("users.xml");
            var userElement = usersXml.Elements("User")
                .FirstOrDefault(x => x.Element("Login")?.Value == login);
            if (userElement == null)
            {
                return false;
            }

            string hashedPassword = userElement.Element("Password")?.Value;
            if (string.IsNullOrEmpty(hashedPassword) || userElement.Element("Password").Value != hashedPassword)
            {
                return false;
            }

            // Загружаем ключи пользователя
            string keyFileName = userElement.Element("KeyFile")?.Value;
            XElement userKeysXml = XElement.Load(keyFileName);

            BigInteger userPrivateKey = Base64ToBigInteger(userKeysXml.Element("PrivateKey")?.Value);
            BigInteger publicKeyX = Base64ToBigInteger(userKeysXml.Element("PublicKeyX")?.Value);
            BigInteger publicKeyY = Base64ToBigInteger(userKeysXml.Element("PublicKeyY")?.Value);
            var userPublicKey = new MyECDH.ECPoint(publicKeyX, publicKeyY);

            // Загружаем ключи приложения для данного пользователя
            BigInteger appPrivateKey = Base64ToBigInteger(userElement.Element("AppPrivateKey")?.Value);
            BigInteger AppPublicKeyX = Base64ToBigInteger(userElement.Element("AppPublicKeyX")?.Value);
            BigInteger AppPublicKeyY = Base64ToBigInteger(userElement.Element("AppPublicKeyY")?.Value);
            var appPublicKey = new MyECDH.ECPoint(AppPublicKeyX, AppPublicKeyY);

            // Проверка секретов
            var appECDH = new MyECDH();
            byte[] secret1 = appECDH.DeriveSharedSecret(userPublicKey); // Приватный ключ приложения + публичный ключ пользователя
            byte[] secret2 = appECDH.DeriveSharedSecret(appPublicKey); // Приватный ключ пользователя + публичный ключ приложения

            if (!secret1.SequenceEqual(secret2))
            {
                Console.WriteLine("Секреты не совпадают. Вход запрещен.");
                return false;
            }

            Console.WriteLine("Секреты совпадают. Вход разрешен.");
            return true;
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Логин и пароль не могут быть пустыми.", "Ошибка регистрации");
            }
            if (AddUser(login, password))
            {
                MessageBox.Show("Пользователь успешно зарегистрирован!", "Регистрация");
            }
            else
            {
                MessageBox.Show("Такой пользователь уже существует", "Ошибка регистрации");
            }
        }

        private bool AddUser(string login, string password)
        {
            XElement usersXml = XElement.Load("users.xml");
            if (usersXml.Elements("User").Any(x => x.Element("Login")?.Value == login))
            {
                return false;
            }

            MyECDH userECDH = new MyECDH();
            string keyFileName = $"{login}_localkeys.xml";
            MyECDH appECDH = new MyECDH();

            var userKeysXml = new XElement("Keys",
                new XElement("PrivateKey", BigIntegerToBase64(userECDH.PrivateKey)),
                new XElement("PublicKeyX", BigIntegerToBase64(userECDH.PublicKey.X)),
                new XElement("PublicKeyY", BigIntegerToBase64(userECDH.PublicKey.Y))
            );
            userKeysXml.Save($"{login}_localkeys.xml");

            var userElement = new XElement("User",
                new XElement("Login", login),
                new XElement("Password", HashPassword(password)),
                new XElement("KeyFile", $"{login}_localkeys.xml"),
                new XElement("AppPublicKeyX", BigIntegerToBase64(appECDH.PublicKey.X)),
                new XElement("AppPublicKeyY", BigIntegerToBase64(appECDH.PublicKey.Y)),
                new XElement("AppPrivateKey", BigIntegerToBase64(appECDH.PrivateKey))
            );
            usersXml.Add(userElement);
            usersXml.Save("users.xml");
            return true;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }
        string BigIntegerToBase64(BigInteger value)
        {
            return Convert.ToBase64String(value.ToByteArray(isUnsigned: true, isBigEndian: true));
        }

        BigInteger Base64ToBigInteger(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            return new BigInteger(bytes, isUnsigned: true, isBigEndian: true);
        }
    }
}