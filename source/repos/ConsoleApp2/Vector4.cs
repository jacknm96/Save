using System;

namespace MathClasses
{
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

        public Vector4 Cross(Vector4 other)
        {
            return new Vector4((y * other.z) - (z * other.y), (z * other.x) - (x * other.z), (x * other.y) - (y * other.x), 0);
        }

        // returns the normalized form of this vector without modifying itself
        public Vector4 Normalized()
        {
            float m = Magnitude();
            return new Vector4(x / m, y / m, z / m, w);
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
}
