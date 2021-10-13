using System;

namespace MathClasses
{
    public class Matrix4
    {
        public float m00, m01, m02, m03;
        public float m10, m11, m12, m13;
        public float m20, m21, m22, m23;
        public float m30, m31, m32, m33;

        // default constructor creates the identity matrix
        public Matrix4()
        {
            m00 = 1f;
            m11 = 1f;
            m22 = 1f;
            m33 = 1f;
        }

        // takes in 4 vector4s to use as its columns
        public Matrix4(Vector4 col1, Vector4 col2, Vector4 col3, Vector4 col4)
        {
            m00 = col1.x; m10 = col2.x; m20 = col3.x; m30 = col4.x;
            m01 = col1.y; m11 = col2.y; m21 = col3.y; m31 = col4.y;
            m02 = col1.z; m12 = col2.z; m22 = col3.z; m32 = col4.z;
            m03 = col1.w; m13 = col2.w; m23 = col3.w; m33 = col4.w;
        }

        // takes in 16 floats to use as parameters. floats must be in column-major order
        public Matrix4(float one, float two, float three, float four,
            float five, float six, float seven, float eight,
            float nine, float ten, float eleven, float twelve,
            float thirteen, float fourteen, float fifteen, float sixteen)
        {
            m00 = one; m10 = five; m20 = nine; m30 = thirteen;
            m01 = two; m11 = six; m21 = ten; m31 = fourteen;
            m02 = three; m12 = seven; m22 = eleven; m32 = fifteen;
            m03 = four; m13 = eight; m23 = twelve; m33 = sixteen;
        }

        //prints matrix
        public void WriteMatrix()
        {
            Console.WriteLine(m00 + " " + m10 + " " + m20 + " " + m30);
            Console.WriteLine(m01 + " " + m11 + " " + m21 + " " + m31);
            Console.WriteLine(m02 + " " + m12 + " " + m22 + " " + m32);
            Console.WriteLine(m03 + " " + m13 + " " + m23 + " " + m33);
        }

        // sets matrix to rotation matrix about x axis
        public void SetRotateX(float a)
        {
            m00 = 1; m10 = 0; m20 = 0; m30 = 0;
            m01 = 0; m11 = (float)Math.Cos(a); m21 = -(float)Math.Sin(a); m31 = 0;
            m02 = 0; m12 = (float)Math.Sin(a); m22 = (float)Math.Cos(a); m32 = 0;
            m03 = 0; m13 = 0; m23 = 0; m33 = 1;
        }

        // sets matrix to rotation matrix about y axis
        public void SetRotateY(float a)
        {
            m00 = (float)Math.Cos(a); m10 = 0; m20 = (float)Math.Sin(a); m30 = 0;
            m01 = 0; m11 = 1; m21 = 0; m31 = 0;
            m02 = -(float)Math.Sin(a); m12 = 0; m22 = (float)Math.Cos(a); m32 = 0;
            m03 = 0; m13 = 0; m23 = 0; m33 = 1;
        }

        // sets matrix to rotation matrix about z axis
        public void SetRotateZ(float a)
        {
            m00 = (float)Math.Cos(a); m10 = -(float)Math.Sin(a); m20 = 0; m30 = 0;
            m01 = (float)Math.Sin(a); m11 = (float)Math.Cos(a); m21 = 0; m31 = 0;
            m02 = 0; m12 = 0; m22 = 1; m32 = 0;
            m03 = 0; m13 = 0; m23 = 0; m33 = 1;
        }

        // translates position in 2d space
        public void SetTranslation(float a, float b)
        {
            m03 = a;
            m13 = b;
        }

        // translates position in 3d space
        public void SetTranslation(float a, float b, float c)
        {
            m03 = a;
            m13 = b;
            m23 = c;
        }

        // vector transformation
        public static Vector4 operator *(Matrix4 m, Vector4 v)
        {
            return new Vector4(v.Dot(new Vector4(m.m00, m.m10, m.m20, m.m30)),
                v.Dot(new Vector4(m.m01, m.m11, m.m21, m.m31)),
                v.Dot(new Vector4(m.m02, m.m12, m.m22, m.m32)),
                v.Dot(new Vector4(m.m03, m.m13, m.m23, m.m33)));
        }

        // matrix multiplication
        public static Matrix4 operator *(Matrix4 m1, Matrix4 m2)
        {
            Vector4 col1 = new Vector4(m2.m00, m2.m01, m2.m02, m2.m03);
            Vector4 col2 = new Vector4(m2.m10, m2.m11, m2.m12, m2.m13);
            Vector4 col3 = new Vector4(m2.m20, m2.m21, m2.m22, m2.m23);
            Vector4 col4 = new Vector4(m2.m30, m2.m31, m2.m32, m2.m33);
            return new Matrix4(m1 * col1, m1 * col2, m1 * col3, m1 * col4);
        }
    }
}