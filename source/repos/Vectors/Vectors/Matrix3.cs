using System;

public class Matrix3
{
    public float m00, m01, m02;
    public float m10, m11, m12;
    public float m20, m21, m22;

    // default constructor creates the identity matrix
    public Matrix3()
    {
        m00 = 1f;
        m11 = 1f;
        m22 = 1f;
    }

    public Matrix3(float a)
    {
        m00 = 1f;
        m11 = 1f;
        m22 = 1f;
    }

    // takes in 3 vector3s to use as its columns
    public Matrix3(Vector3 first, Vector3 second, Vector3 third)
    {
        m00 = first.x; m01 = second.x; m02 = third.x;
        m10 = first.y; m11 = second.y; m12 = third.y;
        m20 = first.z; m21 = second.z; m22 = third.z;
    }

    // takes in 9 floats to use as parameters. floats must be in column-major order
    public Matrix3(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9)
    {
        m00 = m1; m01 = m4; m02 = m7;
        m10 = m2; m11 = m5; m12 = m8;
        m20 = m3; m21 = m6; m22 = m9;
    }

    public void WriteMatrix()
    {
        Console.WriteLine(m00 + " " + m01 + " " + m02);
        Console.WriteLine(m10 + " " + m11 + " " + m12);
        Console.WriteLine(m20 + " " + m21 + " " + m22);
    }

    public Matrix3 SetRotateX(float a)
    {
        return this * new Matrix3(
            new Vector3(1, 0, 0), 
            new Vector3(0, (float)Math.Cos(a), -(float)Math.Sin(a)), 
            new Vector3(0, (float)Math.Sin(a), (float)Math.Cos(a)));
    }

    public Matrix3 SetRotateY(float a)
    {
        return this * new Matrix3(
            new Vector3((float)Math.Cos(a), 0, (float)Math.Sin(a)), 
            new Vector3(0, 1, 0), 
            new Vector3(-(float)Math.Sin(a), 0, (float)Math.Cos(a)));
    }

    public Matrix3 SetRotateZ(float a)
    {
        return this * new Matrix3(
            new Vector3((float)Math.Cos(a), -(float)Math.Sin(a), 0), 
            new Vector3((float)Math.Sin(a), (float)Math.Cos(a), 0), 
            new Vector3(0, 0, 1));
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
        return new Vector3(v.x * m.m00 + v.y * m.m01 + v.z * m.m02,
            v.x * m.m10 + v.y * m.m11 + v.z * m.m12,
            v.x * m.m20 + v.y * m.m21 + v.z * m.m22);
    }

    // matrix multiplication
    public static Matrix3 operator *(Matrix3 m1, Matrix3 m2)
    {
        Vector3 row1 = new Vector3(m1.m00, m1.m01, m1.m02);
        Vector3 row2 = new Vector3(m1.m10, m1.m11, m1.m12);
        Vector3 row3 = new Vector3(m1.m20, m1.m21, m1.m22);
        Vector3 col1 = new Vector3(m2.m00, m2.m10, m2.m20);
        Vector3 col2 = new Vector3(m2.m01, m2.m11, m2.m21);
        Vector3 col3 = new Vector3(m2.m02, m2.m12, m2.m22);

        return new Matrix3(new Vector3(row1.Dot(col1), row2.Dot(col1), row3.Dot(col1)),
            new Vector3(row1.Dot(col2), row2.Dot(col2), row3.Dot(col2)),
            new Vector3(row1.Dot(col3), row2.Dot(col3), row3.Dot(col3)));
    }
}
