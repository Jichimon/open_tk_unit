using OpenTK.Mathematics;

namespace OpenTkUnit.Utils;

public static class PointsConverter
{
    public static Vector3[] ParseToVector3Array(float[] vertices)
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
}
