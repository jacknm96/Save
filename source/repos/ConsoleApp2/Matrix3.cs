using System;

namespace MathClasses
{
    public class Matrix3
    {
        public float m00, m10, m20;
        public float m01, m11, m21;
        public float m02, m12, m22;

        // default constructor creates the identity matrix
        public Matrix3()
        {
            m00 = 1f;
            m11 = 1f;
            m22 = 1f;
        }

        public Matrix3(float a)
        {
            m00 = a;
            m11 = a;
            m22 = a;
        }

        // takes in 3 vector3s to use as its columns
        public Matrix3(Vector3 col1, Vector3 col2, Vector3 col3)
        {
            m00 = col1.x; m01 = col1.y; m02 = col1.z;
            m10 = col2.x; m11 = col2.y; m12 = col2.z;
            m20 = col3.x; m21 = col3.y; m22 = col3.z;
        }

        // takes in 9 floats to use as parameters. floats must be in column-major order
        public Matrix3(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9)
        {
            m00 = m1; m01 = m2; m02 = m3;
            m10 = m4; m11 = m5; m12 = m6;
            m20 = m7; m21 = m8; m22 = m9;
        }

        public void WriteMatrix()
        {
            Console.WriteLine(m00 + " " + m01 + " " + m02);
            Console.WriteLine(m10 + " " + m11 + " " + m12);
            Console.WriteLine(m20 + " " + m21 + " " + m22);
        }

        public void SetRotateX(float a)
        {
            m00 = 1; m10 = 0; m20 = 0;
            m01 = 0; m11 = MathF.Cos(a); m21 = -MathF.Sin(a);
            m02 = 0; m12 = MathF.Sin(a); m22 = MathF.Cos(a);
        }

        public void SetRotateY(float a)
        {
            m00 = MathF.Cos(a); m10 = 0; m20 = MathF.Sin(a);
            m01 = 0; m11 = 1; m21 = 0;
            m02 = -MathF.Sin(a); m12 = 0; m22 = MathF.Cos(a);
        }

        public void SetRotateZ(float a)
        {
            m00 = MathF.Cos(a); m10 = -MathF.Sin(a); m20 = 0;
            m01 = MathF.Sin(a); m11 = MathF.Cos(a); m21 = 0;
            m02 = 0; m12 = 0; m22 = 1;
        }

        public void SetTranslation(float a, float b)
        {
            m02 = a;
            m12 = b;
        }

        public void SetTranslation(float a, float b, float c)
        {
            m02 = a;
            m12 = b;
            m22 = c;
        }

        // vector transformation
        public static Vector3 operator *(Matrix3 m, Vector3 v)
        {
            return new Vector3(v.Dot(new Vector3(m.m00, m.m10, m.m20)),
                v.Dot(new Vector3(m.m01, m.m11, m.m21)),
                v.Dot(new Vector3(m.m02, m.m12, m.m22)));
        }

        // matrix multiplication
        public static Matrix3 operator *(Matrix3 m1, Matrix3 m2)
        {
            
            Vector3 col1 = new Vector3(m2.m00, m2.m01, m2.m02);
            Vector3 col2 = new Vector3(m2.m10, m2.m11, m2.m12);
            Vector3 col3 = new Vector3(m2.m20, m2.m21, m2.m22);
            return new Matrix3(m1 * col1, m1 * col2, m1 * col3);
        }
    }
}
