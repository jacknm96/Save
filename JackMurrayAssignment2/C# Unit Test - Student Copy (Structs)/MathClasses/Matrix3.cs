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

        // constructs multiple of identity matrix
        public Matrix3(float a)
        {
            m00 = a;
            m11 = a;
            m22 = a;
        }

        // takes in 3 vector3s to use as its columns
        public Matrix3(Vector3 col1, Vector3 col2, Vector3 col3)
        {
            m00 = col1.x; m10 = col2.x; m20 = col3.x;
            m01 = col1.y; m11 = col2.y; m21 = col3.y;
            m02 = col1.z; m12 = col2.z; m22 = col3.z;
        }

        // takes in 9 floats to use as parameters. floats must be in column-major order
        public Matrix3(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9)
        {
            m00 = m1; m10 = m4; m20 = m7;
            m01 = m2; m11 = m5; m21 = m8;
            m02 = m3; m12 = m6; m22 = m9;
        }

        // prints the matrix
        public void WriteMatrix()
        {
            Console.WriteLine(m00 + " " + m10 + " " + m20);
            Console.WriteLine(m01 + " " + m11 + " " + m21);
            Console.WriteLine(m02 + " " + m12 + " " + m22);
        }

        // sets matrix to rotation matrix about x axis
        public void SetRotateX(float a)
        {
            m00 = 1; m10 = 0; m20 = 0;
            m01 = 0; m11 = (float)Math.Cos(a); m21 = -(float)Math.Sin(a);
            m02 = 0; m12 = (float)Math.Sin(a); m22 = (float)Math.Cos(a);
        }

        // sets matrix to rotation matrix about y axis
        public void SetRotateY(float a)
        {
            m00 = (float)Math.Cos(a); m10 = 0; m20 = (float)Math.Sin(a);
            m01 = 0; m11 = 1; m21 = 0;
            m02 = -(float)Math.Sin(a); m12 = 0; m22 = (float)Math.Cos(a);
        }

        // sets matrix to rotation matrix about z axis
        public void SetRotateZ(float a)
        {
            m00 = (float)Math.Cos(a); m10 = -(float)Math.Sin(a); m20 = 0;
            m01 = (float)Math.Sin(a); m11 = (float)Math.Cos(a); m21 = 0;
            m02 = 0; m12 = 0; m22 = 1;
        }

        // translates position in 2d space
        public void SetTranslation(float a, float b)
        {
            m02 = a;
            m12 = b;
        }

        // translates position in 3d space
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
