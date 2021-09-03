using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL4;

namespace Framework2D.Graphics
{
    public class Texture2D
    {

        int handle;

        public Texture2D(string source)
        {
            //Load texture
            int width = 1;
            int height = 1;
            GenerateTexture(data, width, height);
        }

        private void GenerateTexture()
        {
            GL.GenTextures(1, out handle);

            GL.BindTexture(TextureTarget.Texture2D, handle);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, )
        }
    }
}
