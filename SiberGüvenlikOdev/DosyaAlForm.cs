using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiberGüvenlikOdev
{
    public partial class DosyaAlForm : Form
    {
        public string filePath, fileName;
        public Bitmap image;

        public DosyaAlForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (secretKeyText.Text != null)
                MessageBox.Show(
                    image.Decode(Convert.ToInt32(secretKeyText.Text))
                    );
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label2.Visible = true;
                ipAddressText.Visible = true;
                button2.Location = new Point(13, 235);
            }
            else
            {
                label2.Visible = false;
                ipAddressText.Visible = false;
                button2.Location = new Point(13, 200);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            IPEndPoint iep;
            if (checkBox1.Checked) // uzak sunucu ise ip girilir
                iep = new IPEndPoint(IPAddress.Parse(ipAddressText.Text.Trim()), DataLayer.APP_PORT);
            else
                iep = new IPEndPoint(IPAddress.Parse(DataLayer.GetLocalIPAddress()), DataLayer.APP_PORT);

            using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                client.Connect(iep);
                byte[] buffer = new byte[1000000]; // Dosya Boyutu
                client.Receive(buffer, buffer.Length, SocketFlags.None);
                File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "//encryptedImage.jpg", buffer); // appdata içerisine kayıt eder

                image = new Bitmap(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "//encryptedImage.jpg");
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
