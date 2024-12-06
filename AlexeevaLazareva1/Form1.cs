using AlexeevaLazareva;
using System.Numerics;
using System.Security.Cryptography;

namespace AlexeevaLazareva1
{
    public partial class MySign : Form
    {
        private bool isLoggedIn = false;
        public MySign()
        {
            InitializeComponent();
            UpdateInterface();
        }

        void SignFormEnable(bool enable)
        {
            openLoginFormButton.Enabled = !enable;
            file_path.Enabled = enable;
            search_file_button.Enabled = enable;
            sign_path.Enabled = enable;
            search_sign_button.Enabled = enable;
            openkey_path.Enabled = enable;
            search_openkey_button.Enabled = enable;
            secretkey_path.Enabled = enable;
            search_secretkey_button.Enabled = enable;
            sign_button.Enabled = enable;
            check_button.Enabled = enable;
            keysgen_button.Enabled = enable;
            log.Enabled = enable;
        }

        private void UpdateInterface()
        {

            if (isLoggedIn)
            {
                lblStatus.Text = "����� ����������!";
                SignFormEnable(true);
            }
            else
            {
                lblStatus.Text = "����������, �������.";
                SignFormEnable(false);
            }
        }

        private void openLoginFormButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.UserLoggedIn += OnUserLoggedIn;
            loginForm.ShowDialog();
        }

        private void OnUserLoggedIn(string username)
        {
            isLoggedIn = true;
            MessageBox.Show($"����� ����������, {username}!", "�������� ����");
            UpdateInterface();
        }

        private void search_file_button_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) //���������� ���� ��� ��������� ������
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK) file_path.Text = openFileDialog.FileName;
            }
        }

        private void search_sign_button_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) //���������� ���� ��� ��������� ������
            {
                openFileDialog.Filter = "�������� ���� (*.bin)|*.bin";
                if (openFileDialog.ShowDialog() == DialogResult.OK) sign_path.Text = openFileDialog.FileName;
            }
        }

        private void search_key_button_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) //���������� ���� ��� ��������� ������
            {
                openFileDialog.Filter = "���� XML (*.xml)|*.xml";
                if (openFileDialog.ShowDialog() == DialogResult.OK) openkey_path.Text = openFileDialog.FileName;
            }
        }
        private void search_secretkey_button_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) //���������� ���� ��� ��������� ������
            {
                openFileDialog.Filter = "���� XML (*.xml)|*.xml";
                if (openFileDialog.ShowDialog() == DialogResult.OK) secretkey_path.Text = openFileDialog.FileName;
            }
        }
        private void sign_button_Click(object sender, EventArgs e)
        {
            try
            {
                log.Text += "������ �����...\r\n";
                byte[] fileBytes = File.ReadAllBytes(file_path.Text);
                log.Text += "���� ������� ��������. ������ ��������� �����...\r\n";

                RSAKeys keys;
                using (StreamReader reader = new StreamReader(secretkey_path.Text))
                {
                    string xmlContent = reader.ReadToEnd();
                    keys = new RSAKeys(
                        1,
                        BigInteger.Parse(GetInnerXmlValue(xmlContent, "PrivateKey")),
                        BigInteger.Parse(GetInnerXmlValue(xmlContent, "Modulus"))
                    );
                }

                using (var sha256 = SHA256.Create())
                {
                    byte[] hash = sha256.ComputeHash(fileBytes);
                    BigInteger hashValue = new BigInteger(hash, isUnsigned: true, isBigEndian: true);

                    BigInteger sign = PowMod(hashValue, keys.privateKey, keys.mod);

                    log.Text += "���� ������� ��������. ������ �������...\r\n";
                    string signPath = "sign.bin";
                    byte[] bytes = sign.ToByteArray(isUnsigned: true, isBigEndian: true);
                    File.WriteAllBytes(signPath, bytes);
                    log.Text += "������� �������� � ���� \"sign.bin\"\r\n";
                }

            }
            catch (Exception ex)
            {
                log.Text += $"������: {ex.Message}\r\n";
            }
            log.Text += "\r\n==============================\r\n";
        }

        private void check_button_Click(object sender, EventArgs e)
        {
            try
            {
                log.Text += "������ ��������� �����...\r\n";
                RSAKeys keys;
                using (StreamReader reader = new StreamReader(openkey_path.Text))
                {
                    string xmlContent = reader.ReadToEnd();
                    keys = new RSAKeys(
                        BigInteger.Parse(GetInnerXmlValue(xmlContent, "PublicKey")),
                        1,
                        BigInteger.Parse(GetInnerXmlValue(xmlContent, "Modulus"))
                    );
                }
                log.Text += "���� ������� ��������. ������ �������...\r\n";

                byte[] bytes = File.ReadAllBytes(sign_path.Text);
                BigInteger sign = new BigInteger(bytes, isUnsigned: true, isBigEndian: true);
                log.Text += "������� ������� ���������. ������ �����...\r\n";

                byte[] fileBytes = File.ReadAllBytes(file_path.Text);
                log.Text += "���� ������� ��������. �������� �������...\r\n";
                using (var sha256 = SHA256.Create())
                {
                    byte[] hash = sha256.ComputeHash(fileBytes);
                    BigInteger hashValue = new BigInteger(hash, isUnsigned: true, isBigEndian: true);
                    BigInteger recoveredHash = PowMod(sign, keys.publicKey, keys.mod);
                    if (recoveredHash == hashValue) log.Text += "�������� ������ �������.\r\n";
                    else log.Text += "�������� ���������.\r\n";
                }
            }
            catch (Exception ex)
            {
                log.Text += $"������: {ex.Message}\r\n";
            }
            log.Text += "\r\n==============================\r\n";


        }
        private void keysgen_button_Click(object sender, EventArgs e)
        {
            try
            {
                log.Text += "��������� ������...\r\n";
                RSAKeys keys = new RSAKeys();
                log.Text += "����� ������� �������������. ������...\r\n";

                string keyPath = "openkey.xml";
                using (StreamWriter writer = new StreamWriter(keyPath))
                {
                    writer.WriteLine("<RSAKeys>");
                    writer.WriteLine($"<PublicKey>{keys.publicKey}</PublicKey>");
                    writer.WriteLine($"<Modulus>{keys.mod}</Modulus>");
                    writer.WriteLine("</RSAKeys>");
                }

                keyPath = "secretkey.xml";
                using (StreamWriter writer = new StreamWriter(keyPath))
                {
                    writer.WriteLine("<RSAKeys>");
                    writer.WriteLine($"<PrivateKey>{keys.privateKey}</PrivateKey>");
                    writer.WriteLine($"<Modulus>{keys.mod}</Modulus>");
                    writer.WriteLine("</RSAKeys>");
                }

                log.Text += "����� �������� � ����� \"openkey.xml\" � \"secretkey.xml\".";
            }
            catch (Exception ex)
            {
                log.Text += $"������: {ex.Message}\r\n";
            }

            log.Text += "\r\n==============================\r\n";
        }
        private static string GetInnerXmlValue(string xml, string tagName)
        {
            int startIndex = xml.IndexOf($"<{tagName}>") + tagName.Length + 2;
            int endIndex = xml.IndexOf($"</{tagName}>");
            return xml.Substring(startIndex, endIndex - startIndex);
        }

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
}