using OpenTK.Mathematics;

namespace OpenTkUnit.Core.Drawable;

public class DrawablePoint
{

    public static readonly DrawablePoint Zero = new(0f, 0f, 0f);


    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }


    public DrawablePoint(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public DrawablePoint()
    {
        
    }


    public Vector3 ParseToVector3()
    {
        return new Vector3(X, Y, Z);
    }


    public static DrawablePoint Vector3ToPoint(Vector3 vector3)
    {
        return new DrawablePoint(vector3.X, vector3.Y, vector3.Z);
    }
}
