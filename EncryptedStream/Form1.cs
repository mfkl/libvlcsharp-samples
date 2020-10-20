using LibVLCSharp.Shared;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace EncryptedStream
{
    public partial class Form1 : Form
    {
        LibVLC _libVLC;
        public Form1()
        {
            InitializeComponent();
            Core.Initialize();
            _libVLC = new LibVLC();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            videoView1.MediaPlayer.Stop();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            // read file path from TextBox3.Text
            var fileStream = new FileStream(path: TextBox3.Text, mode: FileMode.Open, access: FileAccess.Read);


            var streamWrapper = new SeekableStreamWrapper(() =>
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                RijndaelManaged AES = new RijndaelManaged();
                SHA256Cng SHA256 = new SHA256Cng();

                // read key from TextBox4.Text
                AES.Key = SHA256.ComputeHash(Encoding.ASCII.GetBytes(TextBox4.Text));
                AES.Mode = CipherMode.ECB;
                return new CryptoStream(fileStream, AES.CreateDecryptor(), CryptoStreamMode.Read, true);
            });
            videoView1.MediaPlayer= new MediaPlayer(_libVLC)
            {
                Media = new Media(_libVLC, new StreamMediaInput(streamWrapper))
            };
            videoView1.MediaPlayer.Play();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            if (o.ShowDialog() == DialogResult.OK)

            {
                TextBox3.Text = o.FileName;
            }
        }
    }


}
