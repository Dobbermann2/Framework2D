using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Framework2D.Graphics
{
    public class Shader
    {
        int vs;
        int fs;
        int gs;

        int handle;

        public Shader(string vsSource, string fsSource)
        {
            vs = LoadShader(ShaderType.VertexShader, vsSource);
            fs = LoadShader(ShaderType.FragmentShader, fsSource);
            if (vs == -1 || fs == -1)
            {
                handle = -1; return;
            }
            handle = GL.CreateProgram();

            GL.AttachShader(handle, vs);
            GL.AttachShader(handle, fs);
            GL.LinkProgram(handle);

            GL.GetProgram(handle, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                throw new Exception($"Error occurred whilst linking Program({handle}): " + GL.GetProgramInfoLog(handle));
            }

            GL.DetachShader(handle, vs);
            GL.DetachShader(handle, fs);
            GL.DeleteShader(vs);
            GL.DeleteShader(fs);
        }

        internal int LoadShader(ShaderType type, string source)
        {
            source = File.ReadAllText((Environment.CurrentDirectory + "/"+ source));
            int shaderHandle = GL.CreateShader(type);
            GL.ShaderSource(shaderHandle, source);
            GL.CompileShader(shaderHandle);

            GL.GetShader(shaderHandle, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shaderHandle);
                throw new Exception($"Error occurred whilst compiling Shader({shaderHandle}).\n\n{infoLog}");
            }

            return shaderHandle;
        }

        internal void Use()
        {
            GL.UseProgram(GetHandle());
        }

        internal int GetHandle()
        {
            return handle;
        }

        internal void SetUniformMat4(Matrix4 mat, string location)
        {
            int loc = GL.GetUniformLocation(GetHandle(), location);
            GL.UniformMatrix4(loc, false, ref mat);
        }
    }
}
