using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    public class BoxCollider2DComponent : Component
    {

        public ChipmunkSharp.cpPolyShape Shape { get; set; }
        public ChipmunkSharp.cpTransform ShapeTransform { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public float Radius { get; set; }


    }
}
