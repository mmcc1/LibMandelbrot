using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MandelbrotImage
{
    public class Variables
    {
        public string Filepath { get; set; }
        public int Iterations { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public double Xmin { get; set; }
        public double Xmax { get; set; }
        public double Ymin { get; set; }
        public double Ymax { get; set; }
    }

    class Program
    {
        private const string version = "v1.0.0.0";

        static void Main(string[] args)
        {
            Variables v = ParseArgs(args);

            Console.WriteLine("Mandelbrot Image " + version);
            Console.WriteLine("Generating image.  This may take some time depending on the iterations requested.");

            LibMandelbrot.Mandelbrot m = new LibMandelbrot.Mandelbrot();
            Bitmap image = m.Draw(v.Iterations, v.ImageWidth, v.ImageHeight, v.Xmin, v.Xmax, v.Ymin, v.Ymax);//-0.952465, -0.567696, -0.302106, -0.102307);
            image.Save(v.Filepath, ImageFormat.Tiff);

            Console.WriteLine("Image written");
        }

        #region Parse Args

        private static Variables ParseArgs(string[] args)
        {
            //MandelbrotImage -f filename -i iterations -iw ImageWidth -ih ImageHeight -xn xmin -xx xmax -yn ymin -yx ymax
            //MandelbrotImage -h

            Variables v = new Variables();

            if (args.Length == 16 || args.Length == 1)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-f")
                    {
                        v.Filepath = args[i + 1];
                        i++;
                    }

                    if (args[i] == "-i")
                    {
                        v.Iterations = int.Parse(args[i + 1]);
                        i++;
                    }

                    if (args[i] == "-iw")
                    {
                        v.ImageWidth = int.Parse(args[i + 1]);
                        i++;
                    }

                    if (args[i] == "-ih")
                    {
                        v.ImageHeight = int.Parse(args[i + 1]);
                        i++;
                    }

                    if (args[i] == "-xn")
                    {
                        v.Xmin = double.Parse(args[i + 1]);
                        i++;
                    }

                    if (args[i] == "-xx")
                    {
                        v.Xmax = double.Parse(args[i + 1]);
                        i++;
                    }

                    if (args[i] == "-yn")
                    {
                        v.Ymin = double.Parse(args[i + 1]);
                        i++;
                    }
                    
                    if (args[i] == "-yx")
                    {
                        v.Ymax = double.Parse(args[i + 1]);
                        i++;
                    }

                    if (args[i] == "-h" || args[i] == "-help")
                    {
                        Console.WriteLine("Mandelbrot Image " + version);
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("To generate an image:");
                        Console.WriteLine("MandelbrotImage -f test.tif -i 10000 -iw 1920 -ih 1080 -xn -2 -xx 2 -yn -2 -yx 2");
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Parameters:");
                        Console.WriteLine("-f                   - Image filename");
                        Console.WriteLine("-i                   - Iterations");
                        Console.WriteLine("-iw                  - Image Width");
                        Console.WriteLine("-ih                  - Image Height");
                        Console.WriteLine("-xn                  - X min");
                        Console.WriteLine("-xx                  - X max");
                        Console.WriteLine("-yn                  - Y min");
                        Console.WriteLine("-yx                  - Y max");
                        Environment.Exit(0);
                    }
                }
            }
            else
            {
                Terminate("Invalid number of parameters.");
            }

            return v;
        }

        #endregion

        private static void Terminate(string message)
        {
            Console.WriteLine("MandelbrotImage " + version);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(message);
            Console.WriteLine("Incorrect usage.  Please type 'Vernam512 -h' for help.");
            Environment.Exit(0);
        }

    }
}
