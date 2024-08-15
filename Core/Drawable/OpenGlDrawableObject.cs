using OpenTkUnit.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpenTkUnit.Core.Drawable;

public abstract class OpenGlDrawableObject : IDrawable
{

    //propiedades para dibujar el objeto
    private Vector3[] _vertices;
    private readonly uint[] _indices;
    private Color4 _color;

    //GL Properties
    private int _vertexBufferObject;
    private int _vertexArrayObject;
    private int _elementBufferObject;
    private readonly ShaderHandler _shaders = new(Config.VertexShaderFilePath, Config.FragmentShaderFilePath);

    //guarda la posicion inicial del centro del objeto
    private Vector3 _centerOfMass = Vector3.Zero;
    private Vector3 _position = Vector3.Zero;
    protected Matrix4 _modelMatrix;
    protected Matrix4 _mvpMatrix;

    public Vector3 CenterOfMass { get => _centerOfMass; set => _centerOfMass = value; }
    public Vector3 Position { get => _position; set => _position = value; }

    public OpenGlDrawableObject(Vector3[] vertices, uint[] indices, Vector3? centerOfMass = null, Vector3? position = null, Color4? color = null)
    {
        _modelMatrix = Matrix4.Identity;
        _vertices = vertices;
        _indices = indices;
        _centerOfMass = centerOfMass ?? Config.DefaultOriginPosition;
        _position = position ?? _centerOfMass;
        _color = color ?? Config.DefaultColor;
    }

    #region Init and Build GL object

    private void SetVerticesInitialPosition(Vector3 initialPosition)
    {
        _position = initialPosition!;
        List<Vector3> vertexlist = new();
        foreach (Vector3 vertex in _vertices)
        {
            Vector3 newPosition = vertex + (_centerOfMass + _position);
            vertexlist.Add(newPosition);
        }
        _vertices = vertexlist.ToArray();
    }


    public void Build(Vector3? initialPosition = null)
    {
        if (initialPosition is not null)
        {
            SetVerticesInitialPosition((Vector3)initialPosition!);
        }

        _vertexArrayObject = GL.GenVertexArray();
        _vertexBufferObject = GL.GenBuffer();
        _elementBufferObject = GL.GenBuffer();

        //Enlazamos al buffer de datos el VAO geneardo
        GL.BindVertexArray(_vertexArrayObject);

        //enlazamos el VBO con un buffer de openGL y lo inicializamos
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, GetLength(_vertices) * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        //enlazamos el EBO con un buffer de openGL y lo inicializamos
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

        //configuramos los atributos del vertexbuffer y lo habilitamos (el primer 0 indica el location en el vertexShader)
        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);

        _shaders.Use();
        _shaders.SetUniformVector4(Config.ColorUniformName, (Vector4)_color);
    }

    #endregion


    public void Draw(Matrix4 viewProjectionMatrix)
    {
        _mvpMatrix = _modelMatrix * viewProjectionMatrix;

        _shaders.Use();
        _shaders.SetUniformMatrix4(Config.MvpMatrixUniformName, _mvpMatrix);

        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
    }


    public void Destroy()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.DeleteBuffer(_vertexBufferObject);
    }

    #region Static members

    private static int GetLength(Vector3[] array)
    {
        return array.Length * 3;
    }

    #endregion
}
