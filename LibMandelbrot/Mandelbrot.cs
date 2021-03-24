using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

//Based on: http://csharphelper.com/blog/2014/07/draw-a-mandelbrot-set-fractal-in-c/

namespace LibMandelbrot
{
    public class Mandelbrot
    {
        private const double MIN_X = -2.2;
        private const double MAX_X = 1;
        private const double MIN_Y = -1.2;
        private const double MAX_Y = 1.2;

        private double x_min, x_max, y_min, y_max;
        public double z_real, z_imaginary, z2_real, z2_imaginary = 0;

        private int MaxIterations = 64;
        public List<Color> Colors;

        public Mandelbrot()
        {
            Colors = ColorStructToList();
            Reset();
        }

        private List<Color> ColorStructToList()
        {
            return typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public).Select(c => (Color)c.GetValue(null, null)).ToList();
        }

        private void Reset()
        {
            x_min = MIN_X;
            x_max = MAX_X;
            y_min = MIN_Y;
            y_max = MAX_Y;
        }

        public Bitmap Draw(int iterations = 64, int width = 1920, int height = 1080, double m_Xmin = -2, double m_Xmax = 2, double m_Ymin = -2, double m_Ymax = 2)
        {
            this.x_min = m_Xmin;
            this.x_max = m_Xmax;
            this.y_min = m_Ymin;
            this.y_max = m_Ymax;
            this.MaxIterations = iterations;

            return DrawMandelbrot(width, height);
        }

        private Bitmap DrawMandelbrot(int width = 1920, int height = 1080)
        {
            const int MAX_MAG_SQUARED = 4;

            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.Clear(Color.Black);

            double changeInReal = (x_max - x_min) / (width - 1);
            double changeInImaginary = (y_max - y_min) / (height - 1);
            int numberOfColours = Colors.Count;
            double realC = x_min;

            for (int X = 0; X < width; X++)
            {
                double imaginaryC = y_min;

                for (int Y = 0; Y < height; Y++)
                {
                    double realZ = z_real;
                    double imaginaryZ = z_imaginary;
                    double realZ2 = z2_real;
                    double imaginaryZ2 = z2_imaginary;
                    int clr = 1;

                    while ((clr < MaxIterations) && (realZ2 + imaginaryZ2 < MAX_MAG_SQUARED))
                    {
                        realZ2 = realZ * realZ;
                        imaginaryZ2 = imaginaryZ * imaginaryZ;
                        imaginaryZ = 2 * imaginaryZ * realZ + imaginaryC;
                        realZ = realZ2 - imaginaryZ2 + realC;
                        clr++;
                    }

                    bitmap.SetPixel(X, Y, Colors[clr % numberOfColours]);

                    imaginaryC += changeInImaginary;
                }

                realC += changeInReal;
            }

            return bitmap;
        }
    }
}
