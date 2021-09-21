using System;

namespace Vectors
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector3 a = new Vector3(2, 6, 7);
            Vector3 b = new Vector3(5, 4, 3);
            Vector3 c = a + b;
            Console.WriteLine(c.x.ToString() + " " + c.y.ToString() + " " + c.z.ToString());
            c = a - b;
            Console.WriteLine(c.x.ToString() + " " + c.y.ToString() + " " + c.z.ToString());
            c = a.Cross(b);
            Console.WriteLine(c.x.ToString() + " " + c.y.ToString() + " " + c.z.ToString());
        }
    }
    public class Vector2
    {
        public float x, y;

        public Vector2()
        {

        }

        public Vector2(float a, float b)
        {
            x = a;
            y = b;
        }

        public float Magnitude()
        {
            return MathF.Sqrt(x * x + y * y);
        }

        public Vector2 Normalized()
        {
            float m = Magnitude();
            return new Vector2(x / m, y / m);
        }

        public void Normalize()
        {
            float m = Magnitude();
            x /= m;
            y /= m;
        }

        public Vector2 GetPerpendicularRH()
        {
            return new Vector2(-y, x);
        }

        public Vector2 GetPerpendicularLH()
        {
            return new Vector2(y, -x);
        }
    }

    public class Vector3
    {
        public float x, y, z;

        public Vector3()
        {

        }

        public Vector3(float a, float b, float c)
        {
            x = a;
            y = b;
            z = c;
        }

        public Vector3(Vector2 vector)
        {
            x = vector.x;
            y = vector.y;
            z = 0f;
        }

        public Vector3(float a, float b)
        {
            x = a;
            y = b;
            z = 0f;
        }

        public float Magnitude()
        {
            return MathF.Sqrt(x * x + y * y + z * z);
        }

        public float Dot(Vector3 other)
        {
            return x * other.x + y * other.y + z * other.z;
        }

        public Vector3 Cross(Vector3 other)
        {
            return new Vector3((y * other.z) - (z * other.y), (z * other.x) - (x * other.z), (x * other.y) - (y * other.x));
        }

        public Vector3 Normalized()
        {
            float m = Magnitude();
            return new Vector3(x / m, y / m, z / m);
        }

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

        public Vector4()
        {

        }

        public Vector4(float a, float b, float c, float d)
        {
            x = a;
            y = b;
            z = c;
            w = d;
        }

        public Vector4(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
            w = 0f;
        }

        public float Magnitude()
        {
            return MathF.Sqrt(x * x + y * y + z * z + w * w);
        }

        public float Dot(Vector4 other)
        {
            return x * other.x + y * other.y + z * other.z + w * other.w;
        }

        public Vector4 Cross(Vector4 other)
        {
            return new Vector4((y * other.z) - (z * other.y), (z * other.x) - (x * other.z), (x * other.y) - (y * other.x), 0);
        }

        public Vector3 Normalized()
        {
            float m = Magnitude();
            return new Vector3(x / m, y / m, z / m);
        }

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
}
