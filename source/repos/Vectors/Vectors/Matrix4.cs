using System;

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
    public Matrix4(Vector4 first, Vector4 second, Vector4 third, Vector4 fourth)
    {
        m00 = first.x; m01 = second.x; m02 = third.x; m03 = third.x;
        m10 = first.y; m11 = second.y; m12 = third.y; m13 = third.y;
        m20 = first.z; m21 = second.z; m22 = third.z; m23 = third.z;
        m30 = first.w; m31 = second.w; m32 = third.w; m33 = third.w;
    }

    // takes in 16 floats to use as parameters. floats must be in column-major order
    public Matrix4(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9, float m10, float m11, float m12, float m131, float m14, float m15, float m16)
    {
        m00 = m1; m01 = m5; m02 = m9; m03 = m131;
        m10 = m2; m11 = m6; m12 = m10; m13 = m14;
        m20 = m3; m21 = m7; m22 = m11; m23 = m15;
        m30 = m4; m31 = m8; m32 = m12; m33 = m16;
    }

    public void WriteMatrix()
    {
        Console.WriteLine(m00 + " " + m01 + " " + m02 + " " + m03);
        Console.WriteLine(m10 + " " + m11 + " " + m12 + " " + m13);
        Console.WriteLine(m20 + " " + m21 + " " + m22 + " " + m23);
        Console.WriteLine(m30 + " " + m31 + " " + m32 + " " + m33);
    }

    public Matrix4 SetRotateX(float a)
    {
        return this * new Matrix4(new Vector4(1, 0, 0, 0), new Vector4(0, MathF.Cos(a), -MathF.Sin(a), 0), new Vector4(0, MathF.Sin(a), MathF.Cos(a), 0), new Vector4());
    }

    public Matrix4 SetRotateY(float a)
    {
        return this * new Matrix4(new Vector4(MathF.Cos(a), 0, MathF.Sin(a), 0), new Vector4(0, 1, 0, 0), new Vector4(-MathF.Sin(a), 0, MathF.Cos(a), 0), new Vector4());
    }

    public Matrix4 SetRotateZ(float a)
    {
        return this * new Matrix4(new Vector4(MathF.Cos(a), -MathF.Sin(a), 0, 0), new Vector4(MathF.Sin(a), MathF.Cos(a), 0, 0), new Vector4(0, 0, 1, 0), new Vector4());
    }

    public void SetTranslation(float a, float b)
    {
        m03 = a;
        m13 = b;
    }

    public void SetTranslation(float a, float b, float c)
    {
        m03 = a;
        m13 = b;
        m23 = c;
    }

    // vector transformation
    public static Vector4 operator *(Matrix4 m, Vector4 v)
    {
        return new Vector4(v.x * m.m00 + v.y * m.m01 + v.z * m.m02 + v.w * m.m03,
            v.x * m.m10 + v.y * m.m11 + v.z * m.m12 + v.w * m.m13,
            v.x * m.m20 + v.y * m.m21 + v.z * m.m22 + v.w * m.m23,
            v.x * m.m30 + v.y * m.m31 + v.z * m.m32 + v.w * m.m33);
    }

    // matrix multiplication
    public static Matrix4 operator *(Matrix4 m1, Matrix4 m2)
    {
        Vector4 row1 = new Vector4(m1.m00, m1.m01, m1.m02, m1.m03);
        Vector4 row2 = new Vector4(m1.m10, m1.m11, m1.m12, m1.m13);
        Vector4 row3 = new Vector4(m1.m20, m1.m21, m1.m22, m1.m23);
        Vector4 row4 = new Vector4(m1.m30, m1.m31, m1.m32, m1.m33);
        Vector4 col1 = new Vector4(m2.m00, m2.m10, m2.m20, m2.m30);
        Vector4 col2 = new Vector4(m2.m01, m2.m11, m2.m21, m2.m31);
        Vector4 col3 = new Vector4(m2.m02, m2.m12, m2.m22, m2.m32);
        Vector4 col4 = new Vector4(m2.m03, m2.m13, m2.m23, m2.m33);

        return new Matrix4(new Vector4(row1.Dot(col1), row2.Dot(col1), row3.Dot(col1), row4.Dot(col1)),
            new Vector4(row1.Dot(col2), row2.Dot(col2), row3.Dot(col2), row4.Dot(col2)),
            new Vector4(row1.Dot(col3), row2.Dot(col3), row3.Dot(col3), row4.Dot(col3)),
            new Vector4(row1.Dot(col4), row2.Dot(col4), row3.Dot(col4), row4.Dot(col4)));
    }
}