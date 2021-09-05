using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL4;
using System.Linq;

namespace Framework2D.Graphics
{
    public class Batch
    {
        const int MAX_TEXTURES = 10;
        const int MAX_QUADS = 10000;
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

        List<TextureSlot> batchItems;

        public Batch()
        {
            batchItems = new List<TextureSlot>();
            GenerateBuffers();
        }

        public bool AddItem(BatchItem item)
        {
            if (quadCount >= MAX_QUADS)
                return false;

            for(int i = 0; i < batchItems.Count; i++)
            {
                if(batchItems[i].textureID == item.Texture.Handle)
                {
                    batchItems[i].batchItems.Add(item);
                    quadCount++;
                    return true;
                }
            }

            if (batchItems.Count >= MAX_TEXTURES) return false;
            batchItems.Add(new TextureSlot(item.Texture.Handle));
            batchItems[batchItems.Count - 1].batchItems.Add(item);
            quadCount++;
            return true;
        }

        public void Finish()
        {
            int quadVPointer = 0;
            int quadIPointer = 0;
            int texSlotIndex = 0;
            foreach (TextureSlot texSlot in batchItems)
            {
                foreach(BatchItem item in texSlot.batchItems)
                {
                    Vector3[] quadVertices = CreateOriginOffset(item.Origin);
                    Matrix4 transformMatrix = item.TransformMatrix;
                    float depth = 0;
                    vertices[quadVPointer + 0] = new Vertex() { 
                        position = Vector3.TransformPosition(quadVertices[0], transformMatrix),
                        texCoord = item.TexCoords[0],
                        texSlot = texSlotIndex
                    };

                    vertices[quadVPointer + 1] = new Vertex() {
                        position = Vector3.TransformPosition(quadVertices[1], transformMatrix),
                        texCoord = item.TexCoords[1],
                        texSlot = texSlotIndex
                    };

                    vertices[quadVPointer + 2] = new Vertex() {
                        position = Vector3.TransformPosition(quadVertices[2], transformMatrix),
                        texCoord = item.TexCoords[2],
                        texSlot = texSlotIndex
                    };
                    vertices[quadVPointer + 3] = new Vertex() {
                        position = Vector3.TransformPosition(quadVertices[3], transformMatrix),
                        texCoord = item.TexCoords[3],
                        texSlot = texSlotIndex
                    };
            

                    indices[quadIPointer + 0] = quadVPointer + 0;
                    indices[quadIPointer + 1] = quadVPointer + 1;
                    indices[quadIPointer + 2] = quadVPointer + 2;
                    indices[quadIPointer + 3] = quadVPointer + 2;
                    indices[quadIPointer + 4] = quadVPointer + 3;
                    indices[quadIPointer + 5] = quadVPointer + 0;

                    quadVPointer += 4;
                    quadIPointer += 6;
                }
                texSlotIndex++;
            }
            GL.BindBuffer(BufferTarget.ArrayBuffer, VB);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, quadCount * 4 * Vertex.Size, vertices);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IB);
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, IntPtr.Zero, quadCount*6* sizeof(int), indices);
        }

        public void Reset()
        {
            batchItems.Clear();
            quadCount = 0;
        }

        public void BindTextures(int shaderHandle)
        {
            int c = 0;
            foreach(TextureSlot tex in batchItems)
            {
                int texID = tex.textureID;
                GL.ActiveTexture(TextureUnit.Texture0 + c);
                GL.BindTexture(TextureTarget.Texture2D, texID);
                GL.Uniform1(GL.GetUniformLocation(shaderHandle, "textures[" + c + "]"), c);
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
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float)+ 1 * sizeof(int), 0);

            GL.EnableVertexArrayAttrib(VA, 1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float)+ 1 * sizeof(int), 3*sizeof(float));

            GL.EnableVertexArrayAttrib(VA, 2);
            GL.VertexAttribPointer(2, 1, VertexAttribPointerType.Float, false, 5 * sizeof(float)+ 1 * sizeof(float), 5*sizeof(float));

            GL.CreateBuffers(1, out IB);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IB);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * MAX_INDICES, IntPtr.Zero, BufferUsageHint.DynamicDraw);
        }

        private Vector3[] CreateOriginOffset(Vector2 origin)
        {
            return new Vector3[4]
            {
                new Vector3(-origin.X, -origin.Y, 0.0f),
                new Vector3(1f-origin.X, -origin.Y, 0.0f),
                new Vector3(1f-origin.X, 1f-origin.Y, 0.0f),
                new Vector3(-origin.X, 1f-origin.Y, 0.0f),
            };
        }
    }

    public struct TextureSlot
    {
        public int textureID;
        public List<BatchItem> batchItems;

        public TextureSlot(int textureID)
        {
            this.textureID = textureID;
            batchItems = new List<BatchItem>();
        }
    }
}
