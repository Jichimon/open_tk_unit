using OpenTK.Mathematics;

namespace OpenTkUnit.Core.Drawable;

public class CompositeDrawableObject : IDrawable
{
    public readonly Dictionary<string, IDrawable> _parts = new();

    private Vector3 _centerOfMass = Vector3.Zero;
    private Vector3 _position = Vector3.Zero;

    public Vector3 CenterOfMass { get => _centerOfMass; set => _centerOfMass = value; }
    public Vector3 Position { get => _position; set => _position = value; }



    public CompositeDrawableObject(Vector3 position, Dictionary<string, IDrawable> parts)
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
