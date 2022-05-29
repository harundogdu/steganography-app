using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiberGüvenlikOdev
{
    public partial class DosyaGonderForm : Form
    {
        public string filePath, fileName;
        public Bitmap image;
        public DosyaGonderForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e) // Dosya Seç Butonu
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                filePath = file.FileName; // yolunu dönüyor
                fileName = file.SafeFileName; // adını dönüyor
                pictureBox1.ImageLocation = filePath;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                image = new Bitmap(file.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e) // Dosya Gönder Butonu
        {
            if (textToEncryptBox.Text != null && textToEncryptBox.Text != "")
            {
                List<char> secretMessageList = new List<char>();

                foreach (char item in textToEncryptBox.Text)
                    secretMessageList.Add(item);

                image.Encode(secretMessageList, Convert.ToInt32(secretKeyText.Text)).Save(fileName);
                MessageBox.Show("Dosya Şifrelendi Paylaşıldı !");
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(DataLayer.GetLocalIPAddress()), DataLayer.APP_PORT);
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Bind(iep);
                server.Listen(10);

                using (Socket client = server.Accept())
                {
                    ImageConverter converter = new ImageConverter();
                    byte[] buffer = (byte[])converter.ConvertTo(image, typeof(byte[]));

                    client.Send(buffer, buffer.Length, SocketFlags.None);
                }

                server.Close();

            }
        }
    }
}
