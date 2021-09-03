using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework2D.Graphics
{
    public struct BatchItem
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Scale;
        public float Angle;

        public Vector2 Size
        {
            get
            {
                if(Texture != null)
                return Texture.Size * Scale;
                return Scale;
            }
        }
    }
}
