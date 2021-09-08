using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework2D.Graphics
{
    public struct BatchItem
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public float Angle { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2[] TexCoords { get; set; }

        public Vector2 Size { get; set; }

        public Matrix4 TransformMatrix
        {
            get { return  Matrix4.CreateScale(new Vector3(Size.X * Scale.X, Size.Y * Scale.Y, 0)) * Matrix4.CreateRotationZ(Angle) * Matrix4.CreateTranslation(new Vector3(Position)); }
        }

    }
}
