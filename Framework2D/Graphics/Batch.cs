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
        List<TextureSlot> batchItems;

        public Batch()
        {
            batchItems = new List<TextureSlot>();
            GenerateBuffers();
        }

        public bool AddItem(BatchItem item)
        {
            if (quadCount >= MAX_QUADS) return false;
            //TextureSlot[] slot = batchItems.Where(x => x.textureID == item.Texture.Handle).ToArray();
            //bool exists = slot.Length > 0;



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

            //if ((!exists && batchItems.Count >= MAX_TEXTURES) || quadCount >= MAX_QUADS) return false;
            //if (!exists)
            //{
            //    batchItems.Add(new TextureSlot(item.Texture.Handle));
            //}
            //batchItems[slot[0].textureID].Add(item);
            //quadCount++;
        }

        //Build index buffer from batch items
        public void Finish()
        {
            int quadVPointer = 0;
            int quadIPointer = 0;

            foreach (TextureSlot texSlot in batchItems)
            {
                foreach(BatchItem item in texSlot.batchItems)
                {
                    //TODO: Implement depth
                    float depth = 0;
                    vertices[quadVPointer + 0] = new Vertex() { 
                        position = new Vector3(item.Position.X, item.Position.Y, depth),
                        texCoord = new Vector2(0.0f, 0.0f),
                        texSlot = texSlot.textureID};

                    vertices[quadVPointer + 1] = new Vertex() { 
                        position = new Vector3(item.Position.X + item.Size.X, item.Position.Y, depth),                         
                        texCoord = new Vector2(1f, 0.0f),
                        texSlot = texSlot.textureID
                    };

                    vertices[quadVPointer + 2] = new Vertex() {
                        position = new Vector3(item.Position.X + item.Size.X, item.Position.Y + item.Size.Y, depth),
                        texCoord = new Vector2(1f, 1f),
                        texSlot = texSlot.textureID 
                    };
                    vertices[quadVPointer + 3] = new Vertex() { 
                        position = new Vector3(item.Position.X, item.Position.Y + item.Size.Y, depth),
                        texCoord = new Vector2(0.0f, 1.0f),
                        texSlot = texSlot.textureID
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
            }

            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, vertices.Length * Vertex.Size, vertices);
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, IntPtr.Zero, indices.Length * sizeof(int), indices);
            batchItems.Clear();
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
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            GL.EnableVertexArrayAttrib(VA, 1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);


            GL.EnableVertexArrayAttrib(VA, 2);
            GL.VertexAttribPointer(2, 1, VertexAttribPointerType.Int, false, 1 * sizeof(int), 0);
            GL.CreateBuffers(1, out IB);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IB);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * MAX_INDICES, IntPtr.Zero, BufferUsageHint.DynamicDraw);
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
