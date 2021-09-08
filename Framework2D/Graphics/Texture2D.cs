using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Framework2D.Graphics
{
    public class Texture2D
    {
        internal int Handle
        {
            get { return handle; }
        }

        public Vector2 Size
        {
            get { return size; }
        }

        int handle;

        Vector2 size;

        public string Source { get; set; }

        public Texture2D(string source)
        {
            //Load texture
            this.Source = source;
            Bitmap bitmap = new Bitmap(source);

            int width = bitmap.Width;
            int height = bitmap.Height;
            size = new Vector2(width, height);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GenerateTexture(data.Scan0, width, height);

            bitmap.UnlockBits(data);
        }

        private void GenerateTexture(IntPtr data, int width, int height)
        {
            GL.GenTextures(1, out handle);

            GL.BindTexture(TextureTarget.Texture2D, handle);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.BindTexture(TextureTarget.Texture2D, 0);

        }
    }
}
