using System;

namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static float[] ToPolar(float x, float y)
        {
            float[] coordinates = new float[2];
            coordinates[0] = MathF.Sqrt(x * x + y * y);
            coordinates[1] = MathF.Atan2(y, x);
            return coordinates;
        }

        static float[] ToCartesian(float hyp, float rad)
        {
            float[] coordinates = new float[2];
            coordinates[0] = MathF.Sin(rad) * hyp;
            coordinates[1] = MathF.Cos(rad) * hyp;
            return coordinates;
        }
    }
}
