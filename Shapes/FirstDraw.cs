using OpenTkUnit.Core.Drawable;
using OpenTkUnit.Utils;

namespace OpenTkUnit.Shapes;

public class FirstDraw : SimpleDrawableObject
{

    private static readonly float[] _vertices =
    {
    // Top horizontal bar of T (Front face)
    -3.0f,  2.0f,  0.5f, // Bottom left - 0
     3.0f,  2.0f,  0.5f, // Bottom right - 1
     3.0f,  3.0f,  0.5f, // Top right - 2
    -3.0f,  3.0f,  0.5f, // Top left - 3

    // Vertical bar of T (Front face)
    -1.0f, -3.0f,  0.5f, // Bottom left - 4
     1.0f, -3.0f,  0.5f, // Bottom right - 5
     1.0f,  2.0f,  0.5f, // Top right - 6
    -1.0f,  2.0f,  0.5f, // Top left - 7

    // Top horizontal bar of T (Back face)
    -3.0f,  2.0f, -0.5f, // Bottom left - 8
     3.0f,  2.0f, -0.5f, // Bottom right - 9
     3.0f,  3.0f, -0.5f, // Top right - 10
    -3.0f,  3.0f, -0.5f, // Top left - 11

    // Vertical bar of T (Back face)
    -1.0f, -3.0f, -0.5f, // Bottom left - 12
     1.0f, -3.0f, -0.5f, // Bottom right - 13
     1.0f,  2.0f, -0.5f, // Top right - 14
    -1.0f,  2.0f, -0.5f, // Top left - 15
};

    private static readonly uint[] _indices =
    {
    // Front face (Top horizontal bar)
    0, 1, 2,
    2, 3, 0,

    // Front face (Vertical bar)
    4, 5, 6,
    6, 7, 4,

    // Back face (Top horizontal bar)
    8, 9, 10,
    10, 11, 8,

    // Back face (Vertical bar)
    12, 13, 14,
    14, 15, 12,

    // Top face (Top horizontal bar)
    3, 2, 10,
    10, 11, 3,

    // Bottom face (Vertical bar)
    12, 13, 5,
    5, 4, 12,

    // Left face (Top horizontal bar)
    0, 3, 11,
    11, 8, 0,

    // Right face (Top horizontal bar)
    1, 2, 10,
    10, 9, 1,

    // Left face (Vertical bar)
    7, 4, 12,
    12, 15, 7,

    // Right face (Vertical bar)
    5, 6, 14,
    14, 13, 5,
};

    public FirstDraw() : base(PointsConverter.ParseToVector3Array(_vertices), _indices) { }
}
