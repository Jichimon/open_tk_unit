using OpenTK.Mathematics;
using OpenTkUnit.Utils;

namespace OpenTkUnit.Core.Drawable;

public class DrawablePolygon : OpenGlDrawableObject
{
    public DrawablePolygon(DrawablePoint[] vertices, uint[] indices, DrawablePoint? centerOfMass = null, DrawablePoint? position = null, Color4? color = null) 
        : base(vertices.ParseToVector3Array(), indices, centerOfMass?.ParseToVector3(), position?.ParseToVector3(), color)
    {
    }
}
