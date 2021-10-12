using System;

namespace MathClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Vector4 a = new Vector4(2, 6, 7, 3);
            Vector4 b = new Vector4(5, 4, 3, 7);
            Vector4 c = new Vector4(1, 9, 8, 12);
            Vector4 d = new Vector4(3, 9, 8, 5);
            Matrix4 m1 = new Matrix4(a, b, c, d);
            Matrix4 m2 = new Matrix4(b, d, a, c);
            Matrix4 m3 = m1 * m2;
            Vector4 e = m1 * a;
            m3.WriteMatrix();
            e.WriteVector();
            
        }
    }
}
