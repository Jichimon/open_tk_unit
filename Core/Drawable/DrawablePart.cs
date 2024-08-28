using OpenTK.Mathematics;

namespace OpenTkUnit.Core.Drawable;

public class DrawablePart : IDrawable
{


    public readonly List<DrawablePolygon> _polygons = new();

    private Vector3 _centerOfMass = Vector3.Zero;
    private Vector3 _position = Vector3.Zero;

    public Vector3 CenterOfMass { get => _centerOfMass; set => _centerOfMass = value; }
    public Vector3 Position { get => _position; set => _position = value; }


    public DrawablePart(Vector3? centerOfMass = null, Vector3? position = null)
    {
        _centerOfMass = centerOfMass ?? _centerOfMass;
        _position = position ?? _position;
    }

    public DrawablePart(DrawablePolygon[] polygons, Vector3? centerOfMass = null, Vector3? position = null) 
    {
        _polygons = polygons.ToList();
        _centerOfMass = centerOfMass ?? _centerOfMass;
        _position = position ?? _position;
    }


    public void Build(Vector3? initialPosition = null)
    {
        var position = initialPosition ?? Vector3.Zero;
        foreach (var part in _polygons)
        {
            part.Build(_centerOfMass + position);
        }
    }


    public void Draw(Matrix4 viewProjectionMatrix)
    {
        foreach (var part in _polygons)
        {
            part.Draw(viewProjectionMatrix);
        }
    }


    public void Destroy()
    {
        foreach (var part in _polygons)
        {
            part.Destroy();
        }
    }
}
