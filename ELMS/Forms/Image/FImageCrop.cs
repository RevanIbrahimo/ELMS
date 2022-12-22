using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using ELMS.Class;

namespace ELMS.Forms
{
    public partial class FImageCrop : DevExpress.XtraEditors.XtraForm
    {
        public FImageCrop()
        {
            InitializeComponent();
            hbr = new SolidBrush(Color.FromArgb(128, Color.Yellow));
        }
        public string PictureOwner;
        public int count = 0;

        public delegate void DoEvent(string a, int crop_count);
        public event DoEvent SelectionImage;

        string imagepath = null;


        private Bitmap img;
        private Rectangle re;
        private Brush hbr;
        private bool mbdown = false;


        private int _offsetX;
        private int _offsetY;
        private double _zoom = 1.0;
        private int _x;
        private int _y;
        private int _w;
        private int _h;

        private void BLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Şəkilin yüklənməsi";
                dlg.Filter = "All files (*.jpeg;*.jpg;*.bmp;*.png)|*.jpeg;*.jpg;*.bmp;*.png|Image files (*.jpeg;*.jpg)|*.jpeg;*.jpg|Bmp files (*.bmp)|*.bmp|Png files (*.png)|*.png";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    PictureBox.Image = new Bitmap(dlg.FileName);
                    BOK.Visible = true;
                    DescriptionLabel.Visible = false;
                    Type pboxType = PictureBox.GetType();
                    PropertyInfo irProperty = pboxType.GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);
                    Rectangle rectangle = (Rectangle)irProperty.GetValue(PictureBox, null);

                    _offsetX = rectangle.X;
                    _offsetY = rectangle.Y;

                    _zoom = (double)rectangle.Width / (double)PictureBox.Image.Width;
                }
                dlg.Dispose();
            }
        }

        private void BClear_Click(object sender, EventArgs e)
        {
            PictureBox.Image = null;            
            BOK.Visible = false;            
            DescriptionLabel.Visible = true;
            BCamera.Visible = true;
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            if (PictureBox.Image != null)
            {                
                img = (Bitmap)PictureBox.Image.Clone();
                try
                {
                    if (_x < 0)
                        _x = 0;
                    if (_y < 0)
                        _y = 0;
                    if (_w > 0 && _h > 0)
                    {
                        Rectangle r = new Rectangle((int)(_x / _zoom), (int)(_y / _zoom), (int)(_w / _zoom), (int)(_h / _zoom));
                        if (r.X + r.Width > PictureBox.Image.Width)
                            r = new Rectangle(r.X, r.Y, PictureBox.Image.Width - r.X, r.Height);
                        if (r.Y + r.Height > PictureBox.Image.Height)
                            r = new Rectangle(r.X, r.Y, r.Width, PictureBox.Image.Height - r.Y);
                        Bitmap bmpCropped = ((Bitmap)PictureBox.Image).Clone(r, PictureBox.Image.PixelFormat);
                        bmpCropped.Save(imagepath);
                        this.Close();
                    }
                    else
                        XtraMessageBox.Show("Kəsmək üçün şəkilin heç bir hissəsi seçilməyib.");

                    count++;
                }
                catch (Exception exx)
                {
                    XtraMessageBox.Show("Şəkil seçilmədi." + "\nError - " + exx.Message, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
            else
                XtraMessageBox.Show("Şəkil yüklənməyib");
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _w = e.X - _offsetX - _x;
                _h = e.Y - _offsetY - _y;
                PictureBox.Invalidate();
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _x = e.X - _offsetX;
                _y = e.Y - _offsetY;
                Cursor = Cursors.Cross;
            }
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (PictureBox.Image != null)
            {
                Rectangle r = new Rectangle(_x + _offsetX, _y + _offsetY, _w, _h);
                if (r.Width > 0 && r.Height > 0)
                    e.Graphics.DrawRectangle(Pens.Red, r);
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            mbdown = false;
        }

        private void FImageCrop_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PictureBox.Image != null)
                PictureBox.Image.Dispose();
            if (!String.IsNullOrEmpty(imagepath))
            {
                this.SelectionImage(imagepath, count);
            }
        }

        private void FImageCrop_Load(object sender, EventArgs e)
        {
            DescriptionProgressPanel.Hide();
            DescriptionLabel.Location = new Point(Bounds.X + Bounds.Width / 2 - DescriptionLabel.Width / 2, Bounds.Y + Bounds.Height / 2 - DescriptionLabel.Height / 2);
            imagepath = GlobalVariables.V_ExecutingFolder + "\\TEMP\\Images\\Crop" + count + "_" + PictureOwner + ".jpeg";
        }

        private void BCamera_Click(object sender, EventArgs e)
        {
            PictureBox.Image = null;
            DescriptionProgressPanel.Location = new Point(Bounds.X + Bounds.Width / 2 - DescriptionProgressPanel.Width / 2, Bounds.Y + Bounds.Height / 2 - DescriptionProgressPanel.Height / 2);
            DescriptionProgressPanel.Show();
            Application.DoEvents();
            
            Process.Start(GlobalVariables.V_ExecutingFolder + "\\WebCamera.exe");
            DescriptionProgressPanel.Hide();
            Process[] workers = Process.GetProcessesByName("WebCamera");
            foreach (Process worker in workers)
            {
                worker.WaitForExit();
                worker.Dispose();
                var directory = new DirectoryInfo(GlobalVariables.V_ExecutingFolder + "\\WebCameraImages");
                var myImage = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
                if (File.Exists(GlobalVariables.V_ExecutingFolder + "\\WebCameraImages\\" + myImage))
                {
                    PictureBox.Image = Image.FromFile(GlobalVariables.V_ExecutingFolder + "\\WebCameraImages\\" + myImage);
                    BOK.Visible = true;
                    DescriptionLabel.Visible = false;
                    BCamera.Visible = false;
                }
            }
        }
    }
}