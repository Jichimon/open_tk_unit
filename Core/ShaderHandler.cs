using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpenTkUnit.Core;

public class ShaderHandler
{
    public readonly int _handle;
    private readonly Dictionary<string, int> _uniformLocations;

    public ShaderHandler(string vertPath, string fragPath)
    {

        var shaderSource = File.ReadAllText(vertPath);
        var vertexShader = BindAndCompileShader(shaderSource, ShaderType.VertexShader);

        shaderSource = File.ReadAllText(fragPath);
        var fragmentShader = BindAndCompileShader(shaderSource, ShaderType.FragmentShader);

        _handle = GL.CreateProgram();
        LinkShadersIntoGlProgram(vertexShader, fragmentShader);

        // The shader is now ready to go, but first, we're going to cache all the shader uniform locations.
        _uniformLocations = new Dictionary<string, int>();
        CacheUniformLocations();

    }


    private static int BindAndCompileShader(string shaderSource, ShaderType shaderType)
    {
        // GL.CreateShader will create an empty shader (obviously). The ShaderType enum denotes which type of shader will be created.
        var shader = GL.CreateShader(shaderType);
        // Now, bind the GLSL source code
        GL.ShaderSource(shader, shaderSource);
        // And then compile
        CompileShader(shader);
        return shader;
    }


    private void LinkShadersIntoGlProgram(int vertexShader, int fragmentShader)
    {
        GL.AttachShader(_handle, vertexShader);
        GL.AttachShader(_handle, fragmentShader);

        LinkProgram(_handle);

        // When the shader program is linked, it no longer needs the individual shaders attached to it; the compiled code is copied into the shader program.
        // Detach them, and then delete them.
        GL.DetachShader(_handle, vertexShader);
        GL.DetachShader(_handle, fragmentShader);
        GL.DeleteShader(fragmentShader);
        GL.DeleteShader(vertexShader);
    }

    private void CacheUniformLocations()
    {
        // First, we have to get the number of active uniforms in the shader.
        GL.GetProgram(_handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

        // Next, allocate the dictionary to hold the locations.
        for (var i = 0; i < numberOfUniforms; i++)
        {
            var key = GL.GetActiveUniform(_handle, i, out _, out _);
            var location = GL.GetUniformLocation(_handle, key);
            _uniformLocations.Add(key, location);
        }
    }

    private static void CompileShader(int shader)
    {
        GL.CompileShader(shader);

        // Check for compilation errors
        GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
        if (code is not (int)All.True)
        {
            var infoLog = GL.GetShaderInfoLog(shader);
            throw new Exception($"Error occurred whilst compiling Shader({shader}).{Environment.NewLine} {infoLog}");
        }
    }

    private static void LinkProgram(int program)
    {
        GL.LinkProgram(program);

        // Check for linking errors
        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
        if (code is not (int)All.True)
        {
            string logMessage = GL.GetProgramInfoLog(program);
            throw new Exception($"Error occurred whilst linking Program({program} with errorMessage: {logMessage})");
        }
    }


    public void Use()
    {
        GL.UseProgram(_handle);
    }

    // The shader sources provided with this project use hardcoded layout(location)-s. If you want to do it dynamically,
    // you can omit the layout(location=X) lines in the vertex shader, and use this in VertexAttribPointer instead of the hardcoded values.
    public int GetAttribLocation(string attribName)
    {
        return GL.GetAttribLocation(_handle, attribName);
    }

    #region Uniform setters

    /// <summary>
    /// Set a uniform int on this shader.
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    public void SetUniformInt(string name, int data)
    {
        GL.UseProgram(_handle);
        GL.Uniform1(_uniformLocations[name], data);
    }

    /// <summary>
    /// Set a uniform float on this shader.
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    public void SetUniformFloat(string name, float data)
    {
        GL.UseProgram(_handle);
        GL.Uniform1(_uniformLocations[name], data);
    }

    /// <summary>
    /// Set a uniform Matrix4 on this shader
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    /// <remarks>
    ///   <para>
    ///   The matrix is transposed before being sent to the shader.
    ///   </para>
    /// </remarks>
    public void SetUniformMatrix4(string name, Matrix4 data)
    {
        GL.UseProgram(_handle);
        GL.UniformMatrix4(_uniformLocations[name], true, ref data);
    }

    /// <summary>
    /// Set a uniform Vector3 on this shader.
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    public void SetUniformVector3(string name, Vector3 data)
    {
        GL.UseProgram(_handle);
        GL.Uniform3(_uniformLocations[name], data);
    }


    /// <summary>
    /// Set a uniform Vector4 on this shader.
    /// </summary>
    /// <param name="name">The name of the uniform</param>
    /// <param name="data">The data to set</param>
    public void SetUniformVector4(string name, Vector4 data)
    {
        GL.UseProgram(_handle);
        GL.Uniform4(_uniformLocations[name], data);
    }

    #endregion
}
