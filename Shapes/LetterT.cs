using OpenTK.Mathematics;
using OpenTkUnit.Core.Drawable;
using OpenTkUnit.Utils;

namespace OpenTkUnit.Shapes;

public class LetterT : DrawableObject
{

    public static readonly string VerticalBarIndex = "vertical-bar";
    public static readonly string HorizontalBarIndex = "horizontal-bar";

    public LetterT(Vector3 position) : base(position) 
    {
        var verticalBar = new LetterTVerticalBar(position);
        var horizontalBar = new LetterTHorizontalBar(Vector3.UnitY * 2, position);

        _parts.Add(VerticalBarIndex, verticalBar);
        _parts.Add(HorizontalBarIndex, horizontalBar);
    }
}


internal class LetterTHorizontalBar : DrawablePart
{
    private static readonly float[] _vertices =
    {
        // Front face (Roof)
        -3.0f,  0.0f,  0.5f, // Bottom left - 0
         3.0f,  0.0f,  0.5f, // Bottom right - 1
         3.0f,  1.0f,  0.5f, // Top right - 2
        -3.0f,  1.0f,  0.5f, // Top left - 3

        // Back face (Roof)
        -3.0f,  0.0f, -0.5f, // Bottom left - 4
         3.0f,  0.0f, -0.5f, // Bottom right - 5
         3.0f,  1.0f, -0.5f, // Top right - 6
        -3.0f,  1.0f, -0.5f, // Top left - 7
    };

    private static readonly uint[] _indices =
    {
        // Front face
        0, 1, 2,
        2, 3, 0,

        // Back face
        4, 5, 6,
        6, 7, 4,

        // Top face
        3, 2, 6,
        6, 7, 3,

        // Bottom face
        4, 5, 1,
        1, 0, 4,

        // Left face
        0, 3, 7,
        7, 4, 0,

        // Right face
        1, 5, 6,
        6, 2, 1,
    };

    public LetterTHorizontalBar(Vector3? centerOfMass = null, Vector3? position = null) : base(centerOfMass, position)
    {
        var polygon = new DrawablePolygon(_vertices.ParseToDrawablePointsArray(), _indices);
        _polygons.Add(polygon);
    }
}


internal class LetterTVerticalBar : DrawablePart
{
    private static readonly float[] _vertices =
    {
        // Front face (Body)
        -1.0f, -3.0f,  0.5f, // Bottom left - 0
         1.0f, -3.0f,  0.5f, // Bottom right - 1
         1.0f,  2.0f,  0.5f, // Top right - 2
        -1.0f,  2.0f,  0.5f, // Top left - 3

        // Back face (Body)
        -1.0f, -3.0f, -0.5f, // Bottom left - 4
         1.0f, -3.0f, -0.5f, // Bottom right - 5
         1.0f,  2.0f, -0.5f, // Top right - 6
        -1.0f,  2.0f, -0.5f, // Top left - 7
    };

    private static readonly uint[] _indices =
    {
        // Front face
        0, 1, 2,
        2, 3, 0,

        // Back face
        4, 5, 6,
        6, 7, 4,

        // Top face
        3, 2, 6,
        6, 7, 3,

        // Bottom face
        4, 5, 1,
        1, 0, 4,

        // Left face
        0, 3, 7,
        7, 4, 0,

        // Right face
        1, 5, 6,
        6, 2, 1,
    };

    public LetterTVerticalBar(Vector3? position = null) : base(null, position)
    {
        var polygon = new DrawablePolygon(_vertices.ParseToDrawablePointsArray(), _indices);
        _polygons.Add(polygon);
    }
}
