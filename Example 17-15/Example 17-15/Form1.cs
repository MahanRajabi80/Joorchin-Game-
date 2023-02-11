using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Example_17_15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //-------------------------------------------------------------------
        Button[] AllImages = new Button[16];
        Point[] DefaultLocation = new Point[16];
        //-------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = this.MinimizeBox = false;
            for (int i = 0; i <= 15; i++)
            {
                AllImages[i] = new Button();
                int x = (i % 4) * 125;
                int y = i / 4 * 100;
                AllImages[i].Location = new Point(x, y);
                AllImages[i].Size = new Size(125,100);
                AllImages[i].Tag = i;
                AllImages[i].BackgroundImageLayout = ImageLayout.Zoom;
                AllImages[i].Click += new EventHandler(AllImage_Click);
                panel1.Controls.Add(AllImages[i]);
                DefaultLocation[i] = AllImages[i].Location;
            }
        }
        //-------------------------------------------------------------------
        int jabejayi = -1;
        //-------------------------------------------------------------------
        private void AllImage_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            switch (jabejayi)
            {
                case -1:
                    if (b.FlatStyle == FlatStyle.Standard)
                    {
                        b.FlatStyle = FlatStyle.Flat;
                        jabejayi = Convert.ToInt32(b.Tag);
                    }
                    else
                    {
                        b.FlatStyle = FlatStyle.Standard;
                        jabejayi = -1;
                    }
                    break;
                default:
                    Point temp = AllImages[jabejayi].Location;
                    AllImages[jabejayi].Location = b.Location;
                    b.Location = temp;
                    AllImages[jabejayi].FlatStyle = FlatStyle.Standard;
                    jabejayi = -1;
                    Winner();
                    break;
            }            
        }
        //-------------------------------------------------------------------
        private void Winner()
        {
            for (int i = 0; i <= 15; i++)
                if (DefaultLocation[i] != AllImages[i].Location)
                    return;
            MessageBox.Show("!You Win!");
        }
        //-------------------------------------------------------------------
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Jpeg Files (*.jpg)|*.jpg|Bitmap Files (*.bmp)|*.bmp";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.SafeFileName != "")
            {
                Bitmap mainPicture = new Bitmap(Image.FromFile(openFileDialog1.FileName), 500, 400);
                for (int i = 0; i <= 15; i++)
                {
                    int x = (i % 4) * 125;
                    int y = i / 4 * 100;
                    AllImages[i].Location = new Point(x, y);
                    Rectangle rectPortion = new Rectangle(AllImages[i].Left, AllImages[i].Top, 125, 100);
                    Bitmap get_part = new Bitmap(125, 100);
                    get_part = mainPicture.Clone(rectPortion, System.Drawing.Imaging.PixelFormat.Undefined);
                    AllImages[i].BackgroundImage = get_part;
                }              
            }
        }
        //-------------------------------------------------------------------
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            //بهم زدن ترتیب تصویرها
            Random R = new Random();
            for (int i = 1; i <= R.Next(10, 20); i++)//احتمال جابجایی حداقل 10 و حداکثر 19 بار
            {
                int first, next;
                first = R.Next(0, 16);//[0,15]
                do
                {
                    next = R.Next(0, 16);
                } while (next == first);
                Point temp = AllImages[first].Location;
                AllImages[first].Location = AllImages[next].Location;
                AllImages[next].Location = temp;
            }
        }
    }
}
