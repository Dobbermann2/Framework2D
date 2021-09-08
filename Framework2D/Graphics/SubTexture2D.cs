using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework2D.Graphics
{
    public class SubTexture2D
    {
        TextureRegion bounds;
        public Texture2D Texture { get; set; }

        public Vector2[] TexCoords
        {
            get;
            private set;
        }

        public Vector2 Size
        {
            get; set;
        }

        
        public SubTexture2D(Texture2D texture, TextureRegion textureRegion)
        {
            this.bounds = textureRegion;
            this.Texture = texture;
            Vector2[] texCoords = GenerateTexCoords();
            TexCoords = texCoords;

            Size = (TexCoords[2] - TexCoords[0]) * Texture.Size;
        }

        private Vector2[] GenerateTexCoords()
        {
            float pixelStepX = 1f / Texture.Size.X;
            float pixelStepY = 1f / Texture.Size.Y;
            return new Vector2[4]
            {
                new Vector2(    pixelStepX*(bounds.X),                  pixelStepY*(bounds.Y)),
                new Vector2(    pixelStepX*(bounds.X+bounds.Width),     pixelStepY*(bounds.Y)),
                new Vector2(    pixelStepX*(bounds.X+bounds.Width),     pixelStepY*(bounds.Y+bounds.Height)),
                new Vector2(    pixelStepX*(bounds.X),                  pixelStepY*(bounds.Y+bounds.Height)),
            };
        }
    }

    public struct TextureRegion
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public TextureRegion(Vector2 topLeft, Vector2 bottomRight)
        {
            this.X = (int) (topLeft.X);
            this.Y = (int) (topLeft.Y);
            this.Width = (int) (bottomRight.X - topLeft.X);
            this.Height = (int)(bottomRight.Y - topLeft.Y);
        }

        public TextureRegion(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
    }
}
