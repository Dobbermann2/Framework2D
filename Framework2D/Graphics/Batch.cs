using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL4;

namespace Framework2D.Graphics
{
    public class Batch
    {
        const int MAX_TEXTURES = 5;
        const int MAX_QUADS = 100;
        const int MAX_VERTICES = MAX_QUADS * 4;
        const int MAX_INDICES = MAX_QUADS * 6;

        public Vertex[] Vertices
        {
            get { return vertices; }
        }

        public int[] Indices
        {
            get { return Indices; }
        }

        Vertex[] vertices = new Vertex[MAX_VERTICES];
        int[] indices = new int[MAX_INDICES];

        public int quadCount = 0;

        public int VA;
        int VB;
        int IB;

        //(TextureID, List<BatchItem>)
        Dictionary<int, List<BatchItem>> batchItems;

        public Batch()
        {
            batchItems = new Dictionary<int, List<BatchItem>>();
            GenerateBuffers();
        }

        public bool AddItem(BatchItem item)
        {
            //if ((!batchItems.ContainsKey(item.Texture.Handle) && batchItems.Count >= MAX_TEXTURES) || quadCount >= MAX_QUADS) return false;
            //if(!batchItems.ContainsKey(item.Texture.Handle))
            //{
            //    batchItems.Add(item.Texture.Handle, new List<BatchItem>());
            //}
            if(!batchItems.ContainsKey(0))batchItems.Add(0, new List<BatchItem>());
            batchItems[0].Add(item);
            quadCount++;
            return true;
        }

        //Build index buffer from batch items
        public void Finish()
        {
            int quadVPointer = 0;
            int quadIPointer = 0;

            foreach (List<BatchItem> items in batchItems.Values)
            {
                foreach(BatchItem item in items)
                {
                    //TODO: Implement depth
                    float depth = 0;
                    vertices[quadVPointer + 0] = new Vertex() { position = new Vector3(item.Position.X, item.Position.Y, depth) };
                    vertices[quadVPointer + 1] = new Vertex() { position = new Vector3(item.Position.X + item.Size.X, item.Position.Y, depth) };
                    vertices[quadVPointer + 2] = new Vertex() { position = new Vector3(item.Position.X +item.Size.X, item.Position.Y + item.Size.Y, depth) };
                    vertices[quadVPointer + 3] = new Vertex() { position = new Vector3(item.Position.X, item.Position.Y + item.Size.Y, depth) };

                    indices[quadIPointer + 0] = quadIPointer + 0;
                    indices[quadIPointer + 1] = quadIPointer + 1;
                    indices[quadIPointer + 2] = quadIPointer + 2;
                    indices[quadIPointer + 3] = quadIPointer + 2;
                    indices[quadIPointer + 4] = quadIPointer + 3;
                    indices[quadIPointer + 5] = quadIPointer + 0;

                    quadVPointer += 4;
                    quadIPointer += 6;
                }
            }

            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, vertices.Length * Vertex.Size, vertices);
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, IntPtr.Zero, indices.Length * sizeof(int), indices);
            batchItems.Clear();
        }

        public void BindTextures()
        {
            int c = 0;
            foreach(int tex in batchItems.Keys)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + c);
                GL.BindTexture(TextureTarget.Texture2D, tex);
                c++;
            }
        }

        public void GenerateBuffers()
        {
            GL.CreateVertexArrays(1, out VA);
            GL.BindVertexArray(VA);

            GL.CreateBuffers(1, out VB);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VB);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertex.Size * MAX_VERTICES, IntPtr.Zero, BufferUsageHint.DynamicDraw);

            GL.EnableVertexArrayAttrib(VA, 0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            GL.CreateBuffers(1, out IB);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IB);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * MAX_INDICES, IntPtr.Zero, BufferUsageHint.DynamicDraw);
        }

    }
}
