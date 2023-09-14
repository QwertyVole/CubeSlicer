using System;
using System.Windows.Forms;
using System.Collections.Generic;
//
namespace WindowsFormsAppMain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class Point
        {
            public double x, y, z = 0;
            public bool valid = true;

            public Point duplicateFrom(Point pt)
            {
                Point pt2 = new Point();
                pt2.x = pt.x;
                pt2.y = pt.y;
                pt2.z = pt.z;
                return pt2;
            }
        }

        public class Vector
        {
            public double n1, n2, n3 = 0;
        }
        static Vector NormalVector = new Vector();  

        // Function to find cross product
        // of two vector array.
        public static Vector crossProduct(Vector vect_A, Vector vect_B)
        {
            Vector cross_P = new Vector();
            cross_P.n1 = vect_A.n2 * vect_B.n3 - vect_A.n3 * vect_B.n2;
            cross_P.n2 = vect_A.n3 * vect_B.n1 - vect_A.n1 * vect_B.n3;
            cross_P.n3 = vect_A.n1 * vect_B.n2 - vect_A.n2 * vect_B.n1;

            return cross_P;
        }
        static Vector p2pVector(Point p1, Point p2)
        {
            Vector result = new Vector
            {
                n1 = p1.x - p2.x,
                n2 = p1.y - p2.y,
                n3 = p1.z - p2.z
            };
            return result;
        }

        static Point pointFromEdge(double x, double y, double z, double variable, double d)
        {
            Point pnt = new Point();
            pnt.y = y;
            pnt.z = z;
            pnt.x = x;

            switch (variable)
            {
                case (0):
                    pnt.x = (-d - (pnt.z * NormalVector.n3) - (pnt.y * NormalVector.n2))/NormalVector.n1;
                    if (pnt.x > 1 || pnt.x < 0)
                    {
                        pnt.valid = false;
                    }
                    break;
                case (1):
                    pnt.y = (-d - (pnt.z * NormalVector.n3) - (pnt.x * NormalVector.n1))/NormalVector.n2;
                    if (pnt.y > 1 || pnt.y < 0)
                    {
                        pnt.valid = false;
                    }
                    break;

                case (2):
                    pnt.z = (-d - (pnt.y * NormalVector.n2) - (pnt.x * NormalVector.n1))/NormalVector.n3;
                    if (pnt.z > 1 || pnt.z < 0)
                    {
                        pnt.valid = false;
                    }
                    break;
                default:
                    pnt.valid = false;
                    break;
            }
            return pnt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Point point1 = new Point();
            Point point2 = new Point();
            Point point3 = new Point();
            Point buffer = new Point();
            List<Point> borderPoints = new List<Point>();

            point1.x = Convert.ToDouble(textBox1.Text);
            point1.y = Convert.ToDouble(textBox2.Text);
            point1.z = Convert.ToDouble(textBox3.Text);

            point2.x = Convert.ToDouble(textBox4.Text);
            point2.y = Convert.ToDouble(textBox5.Text);
            point2.z = Convert.ToDouble(textBox6.Text);

            point3.x = Convert.ToDouble(textBox7.Text);
            point3.y = Convert.ToDouble(textBox8.Text);
            point3.z = Convert.ToDouble(textBox9.Text);

            //user input
            Vector vector1 = new Vector();
            Vector vector2 = new Vector();
            vector1 = p2pVector(point1, point2);
            vector2 = p2pVector(point1, point3);

            NormalVector = crossProduct(vector1, vector2);

            double d = -(NormalVector.n1 * point1.x + NormalVector.n2 * point1.y + NormalVector.n3 * point1.z);


            for (int i = 0; i < 6; i++)
            {
                buffer = pointFromEdge(0 + (i / 3), 0 + (i / 3), 0, i % 3, d);
                if (buffer.valid == true)
                { borderPoints.Add(new Point().duplicateFrom(buffer)); }
            }

            for (int i = 0; i < 6; i++)
            {
                buffer = pointFromEdge(1 - (i / 3), 0 + (i / 3), 1, i % 3, d);
                if (buffer.valid == true)
                { borderPoints.Add(new Point().duplicateFrom(buffer)); }
            }
            
            string output = "Body: " + Environment.NewLine;
            
            for (int i = 0; i < borderPoints.Count; i++)
            {
                output += " Point " + Convert.ToString(i + 1) 
                    + " x:" + Convert.ToString(Math.Round(borderPoints[i].x,2, MidpointRounding.AwayFromZero)) 
                    + "; y:" + Convert.ToString(Math.Round(borderPoints[i].y, 2, MidpointRounding.AwayFromZero)) 
                    + "; z:" + Convert.ToString(Math.Round(borderPoints[i].z, 2, MidpointRounding.AwayFromZero)) 
                    + ";" + Environment.NewLine;
            }


            MessageBox.Show(output);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            textBox1.Text = Convert.ToString(rnd.Next(0,10)/10.0);
            textBox2.Text = Convert.ToString(rnd.Next(0,10)/10.0); 
            textBox3.Text = Convert.ToString(rnd.Next(0,10)/10.0); 
            textBox4.Text = Convert.ToString(rnd.Next(0,10)/10.0); 
            textBox5.Text = Convert.ToString(rnd.Next(0,10)/10.0);
            textBox6.Text = Convert.ToString(rnd.Next(0,10)/10.0);
            textBox7.Text = Convert.ToString(rnd.Next(0,10)/10.0);
            textBox8.Text = Convert.ToString(rnd.Next(0,10)/10.0);
            textBox9.Text = Convert.ToString(rnd.Next(0,10)/10.0);

        }
    }
}

