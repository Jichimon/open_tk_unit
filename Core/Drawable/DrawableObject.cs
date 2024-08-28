using OpenTK.Mathematics;

namespace OpenTkUnit.Core.Drawable;

public class DrawableObject : IDrawable
{
    public readonly Dictionary<string, DrawablePart> _parts = new();

    private Vector3 _centerOfMass = Vector3.Zero;
    private Vector3 _position = Vector3.Zero;

    public Vector3 CenterOfMass { get => _centerOfMass; set => _centerOfMass = value; }
    public Vector3 Position { get => _position; set => _position = value; }



    public DrawableObject(Vector3? position = null)
    {
        _position = position ?? _position;
    }

    public DrawableObject(Vector3 position, Dictionary<string, DrawablePart> parts)
    {
        _position = position;
        _parts = parts;
    }


    public void Build(Vector3? initialPosition = null)
    {
        foreach (var part in _parts.Values)
        {
            part.Build(initialPosition);
        }
    }


    public void Draw(Matrix4 viewProjectionMatrix)
    {
        foreach (var part in _parts.Values)
        {
            part.Draw(viewProjectionMatrix);
        }
    }


    public void Destroy()
    {
        foreach (var part in _parts.Values)
        {
            part.Destroy();
        }
    }
}
