using System;

namespace Vectors
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

    public class Vector3
    {
        public float x, y, z;

        // default constructor, keep all floats at 0
        public Vector3()
        {

        }

        // constructor taking in 3 floats
        public Vector3(float a, float b, float c)
        {
            x = a;
            y = b;
            z = c;
        }

        // constructor creates a 2d vector
        public Vector3(float a, float b)
        {
            x = a;
            y = b;
            z = 0f;
        }

        public void WriteVector()
        {
            Console.WriteLine(x.ToString());
            Console.WriteLine(y.ToString());
            Console.WriteLine(z.ToString());
        }

        // returns the length of the vector
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        // dot product of this vector and another vector
        public float Dot(Vector3 other)
        {
            return x * other.x + y * other.y + z * other.z;
        }

        // cross product of this vector and another vector
        public Vector3 Cross(Vector3 other)
        {
            return new Vector3((y * other.z) - (z * other.y), (z * other.x) - (x * other.z), (x * other.y) - (y * other.x));
        }

        // returns the normalized version of this vector while not modifying this vector
        public Vector3 Normalized()
        {
            float m = Magnitude();
            return new Vector3(x / m, y / m, z / m);
        }

        // transforms this vector into its normalized form
        public void Normalize()
        {
            float m = Magnitude();
            x /= m;
            y /= m;
            z /= m;
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        public static Vector3 operator *(Vector3 lhs, float mag)
        {
            return new Vector3(lhs.x * mag, lhs.y * mag, lhs.z * mag);
        }

        public static Vector3 operator *(float mag, Vector3 lhs)
        {
            return new Vector3(lhs.x * mag, lhs.y * mag, lhs.z * mag);
        }
    }
    
    public class Vector4
    {
        public float x, y, z, w;

        // default constructor has its w value set to 1
        public Vector4()
        {
            w = 1f;
        }

        // constructor taking in 4 floats
        public Vector4(float a, float b, float c, float d)
        {
            x = a;
            y = b;
            z = c;
            w = d;
        }

        // constructor taking in a vector3 and keeps w at 0
        public Vector4(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
            w = 0f;
        }

        public void WriteVector()
        {
            Console.WriteLine(x.ToString());
            Console.WriteLine(y.ToString());
            Console.WriteLine(z.ToString());
            Console.WriteLine(w.ToString());
        }

        // returns the length of the vector
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z + w * w);
        }

        // returns the dot product of this vector and another
        public float Dot(Vector4 other)
        {
            return x * other.x + y * other.y + z * other.z + w * other.w;
        }

        // returns the normalized form of this vector without modifying itself
        public Vector4 Normalized()
        {
            float m = Magnitude();
            return new Vector4(x / m, y / m, z / m, w / m);
        }

        // transforms this vector into its modified form
        public void Normalize()
        {
            float m = Magnitude();
            x /= m;
            y /= m;
            z /= m;
            w /= m;
        }

        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
        }

        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
        }

        public static Vector4 operator *(Vector4 lhs, float mag)
        {
            return new Vector4(lhs.x * mag, lhs.y * mag, lhs.z * mag, lhs.w * mag);
        }

        public static Vector4 operator *(float mag, Vector4 lhs)
        {
            return new Vector4(lhs.x * mag, lhs.y * mag, lhs.z * mag, lhs.w * mag);
        }
    }

    public class Matrix3
    {
        Vector3 col1, col2, col3;

        // default constructor creates the identity matrix
        public Matrix3()
        {
            col1 = new Vector3(1, 0, 0);
            col2 = new Vector3(0, 1, 0);
            col3 = new Vector3(0, 0, 1);
        }

        // takes in 3 vector3s to use as its columns
        public Matrix3(Vector3 first, Vector3 second, Vector3 third)
        {
            col1 = first;
            col2 = second;
            col3 = third;
        }

        // takes in 9 floats to use as parameters. floats must be in column-major order
        public Matrix3(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9)
        {
            col1 = new Vector3(m1, m2, m3);
            col2 = new Vector3(m4, m5, m6);
            col3 = new Vector3(m7, m8, m9);
        }

        public void WriteMatrix()
        {
            Console.WriteLine(col1.x.ToString() + " " + col2.x.ToString() + " " + col3.x.ToString());
            Console.WriteLine(col1.y.ToString() + " " + col2.y.ToString() + " " + col3.y.ToString());
            Console.WriteLine(col1.z.ToString() + " " + col2.z.ToString() + " " + col3.z.ToString());
        }

        public Matrix3 setRotateX(float a)
        {
            return this * new Matrix3(new Vector3(1, 0, 0), new Vector3(0, (float)Math.Cos(a), -(float)Math.Sin(a)), new Vector3(0, (float)Math.Sin(a), (float)Math.Cos(a)));
        }

        public Matrix3 setRotateY(float a)
        {
            return this * new Matrix3(new Vector3((float)Math.Cos(a), 0, (float)Math.Sin(a)), new Vector3(0, 1, 0), new Vector3(-(float)Math.Sin(a), 0, (float)Math.Cos(a)));
        }

        public Matrix3 setRotateZ(float a)
        {
            return this * new Matrix3(new Vector3((float)Math.Cos(a), -(float)Math.Sin(a), 0), new Vector3((float)Math.Sin(a), (float)Math.Cos(a), 0), new Vector3(0, 0, 1));
        }

        // vector transformation
        public static Vector3 operator *(Matrix3 m, Vector3 v)
        {
            return new Vector3(v.x * m.col1.x + v.y * m.col2.x + v.z * m.col3.x, 
                v.x * m.col1.y + v.y * m.col2.y + v.z * m.col3.y, 
                v.x * m.col1.z + v.y * m.col2.z + v.z * m.col3.z);
        }

        // matrix multiplication
        public static Matrix3 operator *(Matrix3 m1, Matrix3 m2)
        {
            Vector3 row1 = new Vector3(m1.col1.x, m1.col2.x, m1.col3.x);
            Vector3 row2 = new Vector3(m1.col1.y, m1.col2.y, m1.col3.y);
            Vector3 row3 = new Vector3(m1.col1.z, m1.col2.z, m1.col3.z);
            
            return new Matrix3(new Vector3(row1.Dot(m2.col1), row2.Dot(m2.col1), row3.Dot(m2.col1)),
                new Vector3(row1.Dot(m2.col2), row2.Dot(m2.col2), row3.Dot(m2.col2)),
                new Vector3(row1.Dot(m2.col3), row2.Dot(m2.col3), row3.Dot(m2.col3)));
        }
    }

    public class Matrix4
    {
        Vector4 col1, col2, col3, col4;

        // default constructor creates the identity matrix
        public Matrix4()
        {
            col1 = new Vector4(1, 0, 0, 0);
            col2 = new Vector4(0, 1, 0, 0);
            col3 = new Vector4(0, 0, 1, 0);
            col4 = new Vector4(0, 0, 0, 1);
        }

        // takes in 4 vector4s to use as its columns
        public Matrix4(Vector4 first, Vector4 second, Vector4 third, Vector4 fourth)
        {
            col1 = first;
            col2 = second;
            col3 = third;
            col4 = fourth;
        }

        // takes in 16 floats to use as parameters. floats must be in column-major order
        public Matrix4(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9, float m10, float m11, float m12, float m13, float m14, float m15, float m16)
        {
            col1 = new Vector4(m1, m2, m3, m4);
            col2 = new Vector4(m5, m6, m7, m8);
            col3 = new Vector4(m9, m10, m11, m12);
            col4 = new Vector4(m13, m14, m15, m16);
        }

        public void WriteMatrix()
        {
            Console.WriteLine(col1.x.ToString() + " " + col2.x.ToString() + " " + col3.x.ToString() + " " + col4.x.ToString());
            Console.WriteLine(col1.y.ToString() + " " + col2.y.ToString() + " " + col3.y.ToString() + " " + col4.y.ToString());
            Console.WriteLine(col1.z.ToString() + " " + col2.z.ToString() + " " + col3.z.ToString() + " " + col4.z.ToString());
            Console.WriteLine(col1.w.ToString() + " " + col2.w.ToString() + " " + col3.w.ToString() + " " + col4.w.ToString());
        }

        // vector transformation
        public static Vector4 operator *(Matrix4 m, Vector4 v)
        {
            return new Vector4(v.x * m.col1.x + v.y * m.col2.x + v.z * m.col3.x + v.w * m.col4.x,
                v.x * m.col1.y + v.y * m.col2.y + v.z * m.col3.y + v.w * m.col4.y,
                v.x * m.col1.z + v.y * m.col2.z + v.z * m.col3.z + v.w * m.col4.z,
                v.x * m.col1.w + v.y * m.col2.w + v.z * m.col3.w + v.w * m.col4.w);
        }

        // matrix multiplication
        public static Matrix4 operator *(Matrix4 m1, Matrix4 m2)
        {
            Vector4 row1 = new Vector4(m1.col1.x, m1.col2.x, m1.col3.x, m1.col4.x);
            Vector4 row2 = new Vector4(m1.col1.y, m1.col2.y, m1.col3.y, m1.col4.y);
            Vector4 row3 = new Vector4(m1.col1.z, m1.col2.z, m1.col3.z, m1.col4.z);
            Vector4 row4 = new Vector4(m1.col1.w, m1.col2.w, m1.col3.w, m1.col4.w);

            return new Matrix4(new Vector4(row1.Dot(m2.col1), row2.Dot(m2.col1), row3.Dot(m2.col1), row4.Dot(m2.col1)),
                new Vector4(row1.Dot(m2.col2), row2.Dot(m2.col2), row3.Dot(m2.col2), row4.Dot(m2.col2)),
                new Vector4(row1.Dot(m2.col3), row2.Dot(m2.col3), row3.Dot(m2.col3), row4.Dot(m2.col3)),
                new Vector4(row1.Dot(m2.col4), row2.Dot(m2.col4), row3.Dot(m2.col4), row4.Dot(m2.col4)));
        }
    }

    public struct Color
    {
        public UInt32 color;

        public Color(byte r, byte g, byte b, byte a)
        {
            color = 0;
            red = r;
            green = g;
            blue = b;
            alpha = a;
        }

        public byte red
        {
            get
            {
                return (byte)((color >> 24) & 0xff);
            }
            set
            {
                color = (color & 0x00ffffff) | ((UInt32)value << 24);
            }
        }

        public byte green
        {
            get
            {
                return (byte)((color >> 16) & 0xff);
            }
            set
            {
                color = (color & 0xff00ffff) | ((UInt32)value << 16);
            }
        }

        public byte blue
        {
            get
            {
                return (byte)((color >> 8) & 0xff);
            }
            set
            {
                color = (color & 0xffff00ff) | ((UInt32)value << 8);
            }
        }

        public byte alpha
        {
            get
            {
                return (byte)((color) & 0xff);
            }
            set
            {
                color = (color & 0xffffff00) | ((UInt32)value);
            }
        }
    }
}
