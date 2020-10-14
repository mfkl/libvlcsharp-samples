namespace EncryptedStream
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Button1 = new System.Windows.Forms.Button();
            this.TextBox4 = new System.Windows.Forms.TextBox();
            this.Button4 = new System.Windows.Forms.Button();
            this.TextBox3 = new System.Windows.Forms.TextBox();
            this.Button5 = new System.Windows.Forms.Button();
            this.videoView1 = new LibVLCSharp.WinForms.VideoView();
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.BackColor = System.Drawing.Color.Maroon;
            this.Button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Location = new System.Drawing.Point(414, 305);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(116, 60);
            this.Button1.TabIndex = 20;
            this.Button1.Text = "STOP";
            this.Button1.UseVisualStyleBackColor = false;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TextBox4
            // 
            this.TextBox4.Location = new System.Drawing.Point(12, 305);
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new System.Drawing.Size(173, 20);
            this.TextBox4.TabIndex = 19;
            this.TextBox4.Text = "U[#x5:jg0$e-^etBx#MjWH5Zu_ndd9";
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(178, 331);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(60, 34);
            this.Button4.TabIndex = 18;
            this.Button4.Text = "Choose";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // TextBox3
            // 
            this.TextBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox3.Location = new System.Drawing.Point(12, 331);
            this.TextBox3.Multiline = true;
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new System.Drawing.Size(160, 34);
            this.TextBox3.TabIndex = 17;
            this.TextBox3.Text = "filepath here";
            // 
            // Button5
            // 
            this.Button5.BackColor = System.Drawing.Color.Green;
            this.Button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button5.ForeColor = System.Drawing.Color.White;
            this.Button5.Location = new System.Drawing.Point(292, 305);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(116, 60);
            this.Button5.TabIndex = 16;
            this.Button5.Text = "Play";
            this.Button5.UseVisualStyleBackColor = false;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // videoView1
            // 
            this.videoView1.BackColor = System.Drawing.Color.Black;
            this.videoView1.Location = new System.Drawing.Point(12, 12);
            this.videoView1.MediaPlayer = null;
            this.videoView1.Name = "videoView1";
            this.videoView1.Size = new System.Drawing.Size(518, 278);
            this.videoView1.TabIndex = 0;
            this.videoView1.Text = "videoView1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 373);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.TextBox4);
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.TextBox3);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.videoView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.TextBox TextBox4;
        internal System.Windows.Forms.Button Button4;
        internal System.Windows.Forms.TextBox TextBox3;
        internal System.Windows.Forms.Button Button5;
        private LibVLCSharp.WinForms.VideoView videoView1;
    }
}

