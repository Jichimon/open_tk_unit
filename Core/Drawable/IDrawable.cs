using OpenTK.Mathematics;

namespace OpenTkUnit.Core.Drawable;

public interface IDrawable
{
    public Vector3 CenterOfMass { get; set; }
    public Vector3 Position { get; set; }
    internal void Build(Vector3? initialPosition = null);
    public void Draw(Matrix4 viewProjectionMatrix);
    public void Destroy();
}
