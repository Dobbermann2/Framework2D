using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Framework2D.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 position;
        public Vector2 texCoord;
        public int texSlot;

        public static int Size
        {
            get { return Marshal.SizeOf(typeof(Vertex)); }
        }
    }
}
