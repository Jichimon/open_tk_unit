using OpenTK.Mathematics;
using OpenTkUnit.Core.Drawable;

namespace OpenTkUnit.Utils;

public static class PointsConverter
{
    public static Vector3[] ParseToVector3Array(this float[] vertices)
    {
        List<Vector3> array = new();
        for (int i = 0; i < vertices.Length; i += 3)
        {
            float x, y, z;
            x = vertices[i];
            y = vertices[i + 1];
            z = vertices[i + 2];
            Vector3 vector = new(x, y, z);
            array.Add(vector);
        }

        return array.ToArray();
    }


    public static Vector3[] ParseToVector3Array(this DrawablePoint[] vertices)
    {
        List<Vector3> array = new();
        foreach (DrawablePoint point in vertices)
        {
            array.Add(point.ParseToVector3());
        }
        return array.ToArray();
    }



    public static DrawablePoint[] ParseToDrawablePointsArray(this float[] vertices)
    {
        List<DrawablePoint> array = new();
        for (int i = 0; i < vertices.Length; i += 3)
        {
            float x, y, z;
            x = vertices[i];
            y = vertices[i + 1];
            z = vertices[i + 2];
            DrawablePoint vector = new(x, y, z);
            array.Add(vector);
        }

        return array.ToArray();
    }
}
