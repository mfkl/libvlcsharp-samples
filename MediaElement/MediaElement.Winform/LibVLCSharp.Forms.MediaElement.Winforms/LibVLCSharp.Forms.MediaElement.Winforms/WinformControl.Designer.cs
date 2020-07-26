namespace LibVLCSharp.Forms.MediaElement.Winforms
{
    partial class WinformControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PanelBackControls = new System.Windows.Forms.Panel();
            this.PlayBtn = new System.Windows.Forms.PictureBox();
            this.CrntTimeLabel = new System.Windows.Forms.Label();
            this.VideoSeekBar = new System.Windows.Forms.ProgressBar();
            this.StopBtn = new System.Windows.Forms.PictureBox();
            this.TotalTimeLabel = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.VolBar = new System.Windows.Forms.TrackBar();
            this.VolBtn = new System.Windows.Forms.PictureBox();
            this.FulScrBtn = new System.Windows.Forms.PictureBox();
            this.PanelBackVideoView = new System.Windows.Forms.Panel();
            this.VideoView = new LibVLCSharp.WinForms.VideoView();
            this.tableLayoutPanel1.SuspendLayout();
            this.PanelBackControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StopBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VolBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VolBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FulScrBtn)).BeginInit();
            this.PanelBackVideoView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            // 
            // VideoView
            // 
            this.VideoView.BackColor = System.Drawing.Color.Black;
            this.VideoView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VideoView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoView.Location = new System.Drawing.Point(0, 0);
            this.VideoView.MediaPlayer = null;
            this.VideoView.Name = "VideoView";
            this.VideoView.Size = new System.Drawing.Size(738, 393);
            this.VideoView.TabIndex = 0;
            this.VideoView.Text = "videoView1";
            this.VideoView.SizeChanged += new System.EventHandler(this.VideoView_SizeChanged);
            this.VideoView.Click += new System.EventHandler(this.VideoView_Click);
            this.VideoView.DoubleClick += new System.EventHandler(this.VideoView_DoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.PanelBackControls, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.PanelBackVideoView, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(744, 450);
            this.tableLayoutPanel1.TabIndex = 31;
            // 
            // PanelBackControls
            // 
            this.PanelBackControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.PanelBackControls.Controls.Add(this.PlayBtn);
            this.PanelBackControls.Controls.Add(this.CrntTimeLabel);
            this.PanelBackControls.Controls.Add(this.VideoSeekBar);
            this.PanelBackControls.Controls.Add(this.StopBtn);
            this.PanelBackControls.Controls.Add(this.TotalTimeLabel);
            this.PanelBackControls.Controls.Add(this.Label3);
            this.PanelBackControls.Controls.Add(this.VolBar);
            this.PanelBackControls.Controls.Add(this.VolBtn);
            this.PanelBackControls.Controls.Add(this.FulScrBtn);
            this.PanelBackControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBackControls.Location = new System.Drawing.Point(3, 402);
            this.PanelBackControls.Name = "PanelBackControls";
            this.PanelBackControls.Size = new System.Drawing.Size(738, 45);
            this.PanelBackControls.TabIndex = 32;
            // 
            // PlayBtn
            // 
            this.PlayBtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PlayBtn.BackColor = System.Drawing.Color.Transparent;
            this.PlayBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PlayBtn.Image = global::LibVLCSharp.Forms.MediaElement.Winforms.Properties.Resources.pause;
            this.PlayBtn.Location = new System.Drawing.Point(3, 2);
            this.PlayBtn.Name = "PlayBtn";
            this.PlayBtn.Size = new System.Drawing.Size(39, 39);
            this.PlayBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PlayBtn.TabIndex = 56;
            this.PlayBtn.TabStop = false;
            this.PlayBtn.Click += new System.EventHandler(this.PlayBtn_Click);
            // 
            // CrntTimeLabel
            // 
            this.CrntTimeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CrntTimeLabel.AutoSize = true;
            this.CrntTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.CrntTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CrntTimeLabel.ForeColor = System.Drawing.Color.LightGray;
            this.CrntTimeLabel.Location = new System.Drawing.Point(49, 13);
            this.CrntTimeLabel.Name = "CrntTimeLabel";
            this.CrntTimeLabel.Size = new System.Drawing.Size(44, 17);
            this.CrntTimeLabel.TabIndex = 49;
            this.CrntTimeLabel.Text = "00.00";
            this.CrntTimeLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // VideoSeekBar
            // 
            this.VideoSeekBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VideoSeekBar.BackColor = System.Drawing.Color.Black;
            this.VideoSeekBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VideoSeekBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(164)))), ((int)(((byte)(210)))));
            this.VideoSeekBar.Location = new System.Drawing.Point(95, 16);
            this.VideoSeekBar.MarqueeAnimationSpeed = 1000;
            this.VideoSeekBar.Minimum = 1;
            this.VideoSeekBar.Name = "VideoSeekBar";
            this.VideoSeekBar.Size = new System.Drawing.Size(340, 13);
            this.VideoSeekBar.Step = 1;
            this.VideoSeekBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.VideoSeekBar.TabIndex = 50;
            this.VideoSeekBar.Value = 1;
            this.VideoSeekBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VideoSeekBar_MouseDown);
            this.VideoSeekBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VideoSeekBar_MouseMove);
            // 
            // StopBtn
            // 
            this.StopBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.StopBtn.BackColor = System.Drawing.Color.Transparent;
            this.StopBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StopBtn.Image = global::LibVLCSharp.Forms.MediaElement.Winforms.Properties.Resources.stop;
            this.StopBtn.Location = new System.Drawing.Point(491, 6);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(30, 30);
            this.StopBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.StopBtn.TabIndex = 57;
            this.StopBtn.TabStop = false;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // TotalTimeLabel
            // 
            this.TotalTimeLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TotalTimeLabel.AutoSize = true;
            this.TotalTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.TotalTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalTimeLabel.ForeColor = System.Drawing.Color.LightGray;
            this.TotalTimeLabel.Location = new System.Drawing.Point(441, 13);
            this.TotalTimeLabel.Name = "TotalTimeLabel";
            this.TotalTimeLabel.Size = new System.Drawing.Size(44, 17);
            this.TotalTimeLabel.TabIndex = 55;
            this.TotalTimeLabel.Text = "00.00";
            this.TotalTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label3
            // 
            this.Label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.LightGray;
            this.Label3.Location = new System.Drawing.Point(689, 12);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(49, 24);
            this.Label3.TabIndex = 54;
            this.Label3.Text = "100%";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VolBar
            // 
            this.VolBar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.VolBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VolBar.Location = new System.Drawing.Point(600, 13);
            this.VolBar.Margin = new System.Windows.Forms.Padding(0);
            this.VolBar.Maximum = 100;
            this.VolBar.Name = "VolBar";
            this.VolBar.Size = new System.Drawing.Size(86, 45);
            this.VolBar.TabIndex = 0;
            this.VolBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.VolBar.Value = 100;
            this.VolBar.Scroll += new System.EventHandler(this.VolBar_Scroll);
            // 
            // VolBtn
            // 
            this.VolBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.VolBtn.BackColor = System.Drawing.Color.Transparent;
            this.VolBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VolBtn.Image = global::LibVLCSharp.Forms.MediaElement.Winforms.Properties.Resources.volume;
            this.VolBtn.Location = new System.Drawing.Point(566, 6);
            this.VolBtn.Name = "VolBtn";
            this.VolBtn.Size = new System.Drawing.Size(31, 31);
            this.VolBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.VolBtn.TabIndex = 52;
            this.VolBtn.TabStop = false;
            this.VolBtn.Click += new System.EventHandler(this.VolBtn_Click);
            // 
            // FulScrBtn
            // 
            this.FulScrBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FulScrBtn.BackColor = System.Drawing.Color.Transparent;
            this.FulScrBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FulScrBtn.Image = global::LibVLCSharp.Forms.MediaElement.Winforms.Properties.Resources.full;
            this.FulScrBtn.Location = new System.Drawing.Point(527, 6);
            this.FulScrBtn.Name = "FulScrBtn";
            this.FulScrBtn.Size = new System.Drawing.Size(30, 30);
            this.FulScrBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FulScrBtn.TabIndex = 51;
            this.FulScrBtn.TabStop = false;
            this.FulScrBtn.Click += new System.EventHandler(this.FulScrBtn_Click);
            // 
            // PanelBackVideoView
            // 
            this.PanelBackVideoView.Controls.Add(this.VideoView);
            this.PanelBackVideoView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBackVideoView.Location = new System.Drawing.Point(3, 3);
            this.PanelBackVideoView.Name = "PanelBackVideoView";
            this.PanelBackVideoView.Size = new System.Drawing.Size(738, 393);
            this.PanelBackVideoView.TabIndex = 0;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(600, 413);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.PanelBackControls.ResumeLayout(false);
            this.PanelBackControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StopBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VolBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VolBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FulScrBtn)).EndInit();
            this.PanelBackVideoView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal System.Windows.Forms.Panel PanelBackControls;
        private System.Windows.Forms.PictureBox PlayBtn;
        private System.Windows.Forms.Label CrntTimeLabel;
        internal System.Windows.Forms.ProgressBar VideoSeekBar;
        private System.Windows.Forms.PictureBox StopBtn;
        private System.Windows.Forms.Label TotalTimeLabel;
        private System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TrackBar VolBar;
        private System.Windows.Forms.PictureBox VolBtn;
        private System.Windows.Forms.PictureBox FulScrBtn;
        private System.Windows.Forms.Panel PanelBackVideoView;
        private WinForms.VideoView VideoView;
    }
}
