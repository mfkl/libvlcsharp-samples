using System;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System.Timers;
using System.Runtime.InteropServices;

namespace LibVLCSharp.Forms.MediaElement.Winforms
{
    public partial class WinformControl: UserControl
    {
        public MediaPlayer Player;
        public LibVLC libVLC;

        public bool IsFullScreen = false;
        System.Timers.Timer Timer1 = new System.Timers.Timer(1000);
        System.Timers.Timer Timer2 = new System.Timers.Timer(50);

        public WinformControl()
        {
            if (!DesignMode)
            {
                Core.Initialize();
            }

            InitializeComponent();

            libVLC = new LibVLC();
            Player = new MediaPlayer(libVLC);
            VideoView.MediaPlayer = Player;

            
            Timer1.Elapsed += new ElapsedEventHandler(CrntTimeTimer_Tick);
            Timer2.Elapsed += new ElapsedEventHandler(FullScreenTimer_Tick);

            Player.Playing += (sender, e) =>
            {
                if (Player.Media != null)
                {
                    Player.Media.Dispose(); // If video found then dispose it
                }
                PlayBtn.Image = Properties.Resources.play;
                Timer1.Start();

                Player.EnableMouseInput = false;
                Player.EnableKeyInput = false;
                Player.AspectRatio = VideoView.Width + ":" + VideoView.Height;
                VideoSeekBar.Maximum = Convert.ToInt32(Player.Media.Duration);
                TimeSpan t = TimeSpan.FromMilliseconds(Player.Media.Duration);
                TotalTimeLabel.Text = string.Format("{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);

            };






            Player.EndReached += (sender, e) =>
            {
                Timer1.Stop();
                Timer2.Stop();
                Player.AspectRatio = VideoView.Width + ":" + VideoView.Height;

                if (IsFullScreen)
                {
                    ShowCursor(1);
                    PanelBackControls.Visible = true;
                    tableLayoutPanel1.RowCount = 2;
                    fullScreenForm.Controls.Remove(tableLayoutPanel1);
                    this.Controls.Add(tableLayoutPanel1);
                    fullScreenForm.Hide();
                    IsFullScreen = false;
                    FulScrBtn.Image = Properties.Resources.full;
                                    }
            };
        }


        private void CrntTimeTimer_Tick(object sender, EventArgs e)
        {
            // Timer will change time taken every after 1 second like 00:51
            if (Player.Media != null & !(Convert.ToInt32(Player.Time) > VideoSeekBar.Maximum) & !(Convert.ToInt32(Player.Time) <= 0))
            {
                VideoSeekBar.Value = Convert.ToInt32(Player.Time);
            }
            var t = TimeSpan.FromMilliseconds(Player.Time);
            CrntTimeLabel.Text = string.Format("{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);

        }

        private TimeSpan activityThreshold = TimeSpan.FromSeconds(3);
        private bool cursorHidden = false;
        private bool shouldHide;
        [DllImport("user32")]
        private static extern long ShowCursor(long bShow);

        private void FullScreenTimer_Tick(object sender, EventArgs e)
        {
            // This timer is for hiding controls and cursor 
            // After every 3 seconds when no mouse movement
            if (IsFullScreen & Player.Media != null)
            {
                shouldHide = User32Interop.GetLastInput() > activityThreshold;
                if (cursorHidden != shouldHide)
                {
                    if (shouldHide)
                    {
                        ShowCursor(0);
                        PanelBackControls.Visible = false;
                        tableLayoutPanel1.RowCount = 1;
                    }
                    else
                    {
                        ShowCursor(1);
                        PanelBackControls.Visible = true;
                        tableLayoutPanel1.RowCount = 2;
                    }
                    cursorHidden = shouldHide;
                }
            }
            else
                Timer2.Stop();
        }


        private void Play()
        {
            if (Player.Media != null)
            {

                if (Player.IsPlaying)
                {
                    Player.Pause();
                    PlayBtn.Image = Properties.Resources.pause;
                    Timer1.Stop();
                }
                else
                {
                    Player.Play();
                    PlayBtn.Image = Properties.Resources.play;
                    Timer1.Start();
                }


                VideoSeekBar.Maximum = Convert.ToInt32(Player.Media.Duration);
                Player.AspectRatio = VideoView.Width + ":" + VideoView.Height;

                // Gets total video duration and insert into label2 like 00:54
                TimeSpan t = TimeSpan.FromMilliseconds(Player.Media.Duration);
                TotalTimeLabel.Text = string.Format("{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);

            }
        }


        private void PlayBtn_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void VolBar_Scroll(object sender, EventArgs e)
        {
            Player.Volume = VolBar.Value;
            Label3.Text = VolBar.Value + "%";
            Properties.Settings.Default.volume = VolBar.Value;
            Properties.Settings.Default.Save();

        }
        Form fullScreenForm = new Form();

        private void Fullscreen()
        {
               fullScreenForm.ShowInTaskbar = false;
                fullScreenForm.ShowIcon = false;
            if (Player.Media != null)
            {
                InitFullScreenForm();

                if (IsFullScreen)
                {
                    fullScreenForm.Controls.Remove(tableLayoutPanel1);
                    this.Controls.Add(tableLayoutPanel1);
                    fullScreenForm.Hide();

                    IsFullScreen = false;
                    FulScrBtn.Image = Properties.Resources.full;
                    Timer2.Stop();

                }
                else
                {
                   // fullScreenForm = new Form();
                    InitFullScreenForm();
                    fullScreenForm.Controls.Add(tableLayoutPanel1);
                    fullScreenForm.Show();
                    IsFullScreen = true;
                    FulScrBtn.Image = Properties.Resources.exit_full;
                    Timer2.Start();
                }
            }
        }
        private void InitFullScreenForm()
        {
            fullScreenForm.FormBorderStyle = FormBorderStyle.None;
            fullScreenForm.WindowState = FormWindowState.Maximized;
            tableLayoutPanel1.Dock = DockStyle.Fill;
        }
        private void FulScrBtn_Click(object sender, EventArgs e)
        {            
                Fullscreen();            
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            if (Player.Media != null)
            {
                Timer1.Stop();
                Player.Stop();
                VideoSeekBar.Value = 1;
                CrntTimeLabel.Text = "00:00";
                TotalTimeLabel.Text = "00:00";
                PlayBtn.Image = Properties.Resources.pause;
            }
        }


        private void ChangeProgress(ProgressBar bar, MouseEventArgs e)
        {
            // It will change progressbar value
            if (e.Button == MouseButtons.Left)
            {
                var mousepos = Math.Min(Math.Max(e.X, 0), bar.ClientSize.Width);
                var value = System.Convert.ToInt32(bar.Minimum + (bar.Maximum - bar.Minimum) * mousepos / (double)bar.ClientSize.Width);
                if (value > bar.Value & value < bar.Maximum)
                {
                    bar.Value = value + 1;
                    bar.Value = value;
                }
                else
                    bar.Value = value;
                if (Player.Media != null)
                    Player.Time = VideoSeekBar.Value;
            }
        }

        private void VideoView_DoubleClick(object sender, EventArgs e)
        {
            Fullscreen();
            Play();
        }

        private void VideoView_Click(object sender, EventArgs e)
        {
            // If someone single clicks on Video 
            // then play or pause            
            Play();

        }

        private void VideoView_SizeChanged(object sender, EventArgs e)
        {
            // if form width/height changed
            // Then make video aspect ratio according to width/height
            if (Player.Media != null)
                Player.AspectRatio = VideoView.Width + ":" + VideoView.Height;
        }

        private void VolBtn_Click(object sender, EventArgs e)
        {
            // Speaker/Volume Button click
            if (Player.Mute)
            {
                Player.Mute = false;
                VolBtn.Image = Properties.Resources.volume;
            }
            else
            {
                Player.Mute = true;
                VolBtn.Image = Properties.Resources.mute;
            }
            Properties.Settings.Default.volume = VolBar.Value;
            Properties.Settings.Default.Save();
        }

        private void VideoSeekBar_MouseDown(object sender, MouseEventArgs e)
        {
            // If some move down on ProgressBar1
            // Then change progress
            ChangeProgress(VideoSeekBar, e);
        }

        private void VideoSeekBar_MouseMove(object sender, MouseEventArgs e)
        {
            // If some move mouse on ProgressBar1
            // Then change progress
            ChangeProgress(VideoSeekBar, e);
        }


       

        private void Form1_Shown(object sender, EventArgs e)
        {
            VolBar.Value = Properties.Settings.Default.volume <= 0 ? 1 : Properties.Settings.Default.volume;

            // Player.Play(new Media(libVLC, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4")));


        }
    }

}
static class User32Interop
{
    // It returns time of last mouse/keyboard move
    public static TimeSpan GetLastInput()
    {
        var plii = new LASTINPUTINFO();
        plii.cbSize = Convert.ToUInt32(Marshal.SizeOf(plii));
        if (GetLastInputInfo(ref plii))
            return TimeSpan.FromMilliseconds(Environment.TickCount - plii.dwTime);
        else
            throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
    }
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
    struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }
}