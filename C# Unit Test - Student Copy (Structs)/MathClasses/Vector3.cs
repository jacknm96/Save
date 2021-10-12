using System;

namespace MathClasses
{
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
}