using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTkUnit.Core;
using OpenTkUnit.Shapes;

namespace OpenTkUnit;

public class Window : GameWindow
{

    private readonly Scene _letters;
    private Matrix4 _viewProjectionMatrix;
    private Matrix4 _projectionMatrix;
    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
    {
        var letterT = new LetterT(Vector3.Zero);
        _letters = new Scene();
        _letters.AddObject("letter-T", letterT);
    }

    protected void CreateViewProjectionMatrix()
    {

        //posicion de la camara
        Vector3 position = new(8.0f, 5.0f, 8.0f);
        Vector3 front = new(0.0f, 0.0f, -1.0f);
        Vector3 up = new(0.0f, 1.0f, 0.0f);

        Matrix4 view = Matrix4.LookAt(position, front, up);

        _viewProjectionMatrix = view * _projectionMatrix;
    }


    protected override void OnLoad()
    {
        GL.ClearColor(0.12f, 0.32f, 0.2f, 1.0f);
        _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Size.X / Size.Y, 1.0f, 100.0f);
        _letters.Build();
        base.OnLoad();
    }


    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.Enable(EnableCap.DepthTest);

        CreateViewProjectionMatrix();

        _letters.Draw(_viewProjectionMatrix);
        SwapBuffers();
        base.OnRenderFrame(args);
    }


    protected override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, Size.X, Size.Y);
        base.OnResize(e);
    }


    protected override void OnUnload()
    {
        _letters.Destroy();
        base.OnUnload();
    }
}
