// See https://aka.ms/new-console-template for more information
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTkUnit;
using OpenTkUnit.Utils;

Console.WriteLine("Hello, World! I'm using OpenTK!");

var nativeWindowSettings = new NativeWindowSettings()
{
    ClientSize = new Vector2i(Config.WindowWidth, Config.WindowHeight),
    Title = Config.ProgramTitle,
    Flags = ContextFlags.ForwardCompatible,
};

using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
{
    window.Run();
}
