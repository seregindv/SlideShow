using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace SlideShow
{
    public partial class CartoonForm : Form
    {
        private string[] _picList;
        private int _currentPicture;
        private Graphics _formGraphics;
        private Rectangle _drawRect;
        public CartoonForm()
        {
            InitializeComponent();
        }

        private int GetCenter(int outerSize, int innerSize)
        {
            return outerSize > innerSize ? (outerSize - innerSize) / 2 : 0;
        }
        
        protected override void OnLoad(EventArgs e)
        {
            string pictureDir = Program.CmdLineArgs.Length > 0 ? Program.CmdLineArgs[0] : Directory.GetCurrentDirectory();
            try
            {
                _picList = Directory.GetFiles(pictureDir, "*.jpg");
                if (0 == _picList.Length)
                    throw new Exception("No file found");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
                return;
            }
            Array.Sort(_picList);
            _formGraphics = this.CreateGraphics();
            _formGraphics.PageUnit = GraphicsUnit.Pixel;
            _currentPicture = -1;
            using (Image firstImage = Image.FromFile(_picList[0]))
            {
                _drawRect = new Rectangle(
                    new Point(
                              GetCenter(this.Width, firstImage.Width),
                              GetCenter(this.Height, firstImage.Height)
                             ),
                    firstImage.Size
                    );
            }
            cartoonTimer.Enabled = true;
            base.OnLoad(e);
        }

        private void cartoonTimer_Tick(object sender, EventArgs e)
        {
            if (_currentPicture >= _picList.Length - 1)
                _currentPicture = -1;
            ++_currentPicture;
            using (var pic = Image.FromFile(_picList[_currentPicture]))
                _formGraphics.DrawImageUnscaledAndClipped(pic, _drawRect);
        }
    }
}