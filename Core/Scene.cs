using OpenTK.Mathematics;
using OpenTkUnit.Core.Drawable;

namespace OpenTkUnit.Core;

public class Scene : IDrawable
{
    private readonly Dictionary<string, DrawableObject> _objects = new();

    private Vector3 _centerOfMass = Vector3.Zero;
    private Vector3 _position = Vector3.Zero;

    public Vector3 CenterOfMass { get => _centerOfMass; set => _centerOfMass = value; }
    public Vector3 Position { get => _position; set => _position = value; }


    public Scene(Vector3? position = null)
    {
        _position = position ?? _position;
    }

    public Scene(Vector3 position, Dictionary<string, DrawableObject> objects)
    {
        _position = position;
        _objects = objects;
    }


    public bool AddObject(string objectIdentifier, DrawableObject drawableObject)
    {
        return _objects.TryAdd(objectIdentifier, drawableObject);
    }


    public void Build(Vector3? initialPosition = null)
    {
        foreach (var part in _objects.Values)
        {
            part.Build(initialPosition);
        }
    }


    public void Draw(Matrix4 viewProjectionMatrix)
    {
        foreach (var part in _objects.Values)
        {
            part.Draw(viewProjectionMatrix);
        }
    }


    public void Destroy()
    {
        foreach (var part in _objects.Values)
        {
            part.Destroy();
        }
    }
}
