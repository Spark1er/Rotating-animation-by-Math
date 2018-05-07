using System;
using System.Drawing;
using System.Windows.Forms;

namespace RotatingPlane
{
    public partial class Form2 : Form
    {
        private readonly Bitmap _bmp = new Bitmap(400, 400);

        private Graphics _g;

        // Settings
        private int _radiusSq = 100;
        private int _radiusTr = 80;
        private int _center = 175;
        private int _angle;
        private int _resize = 4;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {
            _g = Graphics.FromImage(_bmp); //Binding the graphics to the Bitmap
            pictureBox1.Image = _bmp;

            RotatingTimer.Start();
        }

        private int[] LineCoord(int angleIn, int radius, int center)
        {
            //[ cos(x)  sin(x) 0]
            //[ -sin(x) cos(x) 0]
            //[    0     0     1]

            var coord = new int[2]; // Setting up the int array for return
            angleIn %= 360;
            angleIn *= 1;

            if (angleIn >= 0 && angleIn <= 180)
            {
                // coord[0] - X
                // coord[1] - Y
                coord[0] = center + (int)(radius * Math.Sin(Math.PI * angleIn / 180));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / 180));
            }
            else
            {
                coord[0] = center - (int)(radius * -Math.Sin(Math.PI * angleIn / 180));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / 180));
            }
            return coord;


        }

        private void RotatingTimer_Tick(object sender, EventArgs e)
        {
            _g = Graphics.FromImage(_bmp);
            _g.Clear(Color.White);

            // Points SQUARE
            var point1 = new Point(LineCoord(_angle, _radiusSq, _center)[0], LineCoord(_angle, _radiusSq, _center)[1]);
            var point2 = new Point(LineCoord(_angle + 90, _radiusSq, _center)[0], LineCoord(_angle + 90, _radiusSq, _center)[1]);
            var point3 = new Point(LineCoord(_angle + 180, _radiusSq, _center)[0], LineCoord(_angle + 180, _radiusSq, _center)[1]);
            var point4 = new Point(LineCoord(_angle + 270, _radiusSq, _center)[0], LineCoord(_angle + 270, _radiusSq, _center)[1]);

            // Points TR
            var point7 = new Point(LineCoord(_angle + 45, _radiusTr, _center)[0], LineCoord(_angle + 45, _radiusTr, _center)[1]);
            var point6 = new Point(LineCoord(_angle + 180, _radiusTr, _center)[0], LineCoord(_angle + 180, _radiusTr, _center)[1]);
            var point5 = new Point(LineCoord(_angle + 270, _radiusTr, _center)[0], LineCoord(_angle + 270, _radiusTr, _center)[1]);

            Point[] pointsTr = { point5, point6, point7, point5 };

            Point[] pointsSq = { point1, point2, point3, point4, point1 }; // Get all points in one array
            pictureBox1.Image = _bmp;

            _g.DrawLines(new Pen(Color.Black), pointsSq);
            _g.DrawLines(new Pen(Color.Red), pointsTr);

            _g.Dispose();

            _angle = _angle + 15;

            if (_radiusTr < 2)
            {
                RotatingTimer.Stop();
                timer1.Start();
            }
            _radiusSq = _radiusSq + _resize;
            _radiusTr = _radiusTr - _resize;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _g = Graphics.FromImage(_bmp);
            _g.Clear(Color.White);

            // Points SQUARE
            var point1 = new Point(LineCoord(_angle, _radiusSq, _center)[0], LineCoord(_angle, _radiusSq, _center)[1]);
            var point2 = new Point(LineCoord(_angle + 90, _radiusSq, _center)[0], LineCoord(_angle + 90, _radiusSq, _center)[1]);
            var point3 = new Point(LineCoord(_angle + 180, _radiusSq, _center)[0], LineCoord(_angle + 180, _radiusSq, _center)[1]);
            var point4 = new Point(LineCoord(_angle + 270, _radiusSq, _center)[0], LineCoord(_angle + 270, _radiusSq, _center)[1]);

            // Points TR
            var point7 = new Point(LineCoord(_angle + 45, _radiusTr, _center)[0], LineCoord(_angle + 45, _radiusTr, _center)[1]);
            var point6 = new Point(LineCoord(_angle + 180, _radiusTr, _center)[0], LineCoord(_angle + 180, _radiusTr, _center)[1]);
            var point5 = new Point(LineCoord(_angle + 270, _radiusTr, _center)[0], LineCoord(_angle + 270, _radiusTr, _center)[1]);

            Point[] pointsTr = { point5, point6, point7, point5 };

            Point[] pointsSq = { point1, point2, point3, point4, point1 }; // Get all points in one array
            pictureBox1.Image = _bmp;

            _g.DrawLines(new Pen(Color.Black), pointsSq);
            _g.DrawLines(new Pen(Color.Red), pointsTr);

            _g.Dispose();

            _angle = _angle + 15;

            if (_radiusSq < 2)
            {
                timer1.Stop();
                RotatingTimer.Start();
            }
            _radiusSq = _radiusSq - _resize;
            _radiusTr = _radiusTr + _resize;
        }
    }
}
