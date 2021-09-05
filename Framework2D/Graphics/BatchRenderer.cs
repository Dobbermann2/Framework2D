using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Framework2D.Graphics
{
    public class BatchRenderer
    {

        private Shader defaultShader;

        private Matrix4 projectionMatrix;
        private Matrix4 viewMatrix;

        private List<Batch> batches;

        private bool begin = false;
        private bool end = true;

        public int drawCallCount = 0;

        public BatchRenderer(int width, int height)
        {
            defaultShader = new Shader("vUnlit.glsl", "fUnlit.glsl");
            projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, 0.0f, 100f);
            batches = new List<Batch>();
        }

        public void Begin(Matrix4 viewMatrix)
        {
            this.viewMatrix = viewMatrix;
            Begin();
        }

        public void Begin()
        {
            if (begin) throw new Exception("End the batch first");
            begin = true;
            end = false;
            drawCallCount = 0;
        }

        /*
         * 
         *      Texture2D
         * 
         */
        public void Draw(Texture2D texture, Vector2 position, Vector2 scale, float angle, Vector2 origin)
        {
            BatchItem item = new BatchItem
            {
                Texture = texture,
                Position = position,
                Scale = scale,
                Angle = angle,
                Origin = origin,
                TexCoords = new Vector2[]
                {
                    new Vector2(0.0f, 0.0f),
                    new Vector2(1f, 0.0f),
                    new Vector2(1f, 1f),
                    new Vector2(0.0f, 1f),
                }
            };
            SubmitBatchItem(item);
        }

        public void Draw(Texture2D texture, Vector2 position)
        {
            Draw(texture, position, Vector2.One, 0.0f, Vector2.Zero);
        }

        public void Draw(Texture2D texture, Vector2 position, Vector2 origin)
        {
            Draw(texture, position, Vector2.One, 0.0f, origin);
        }

        /*
         * 
         *      SubTexture2D
         * 
         */
        public void Draw(SubTexture2D texture, Vector2 position, Vector2 scale, float angle, Vector2 origin)
        {
            BatchItem item = new BatchItem
            {
                Texture = texture.Texture,
                Position = position,
                Scale = scale,
                Angle = angle,
                Origin = origin,
                TexCoords = texture.TexCoords
            };

            SubmitBatchItem(item);
        }


        private void SubmitBatchItem(BatchItem batchItem)
        {
            if (batches.Count == 0) batches.Add(new Batch());

            for (int i = 0; i < batches.Count; i++)
            {
                if (batches[i].AddItem(batchItem))
                {
                    return;
                }
            }

            batches.Add(new Batch());
            batches[batches.Count - 1].AddItem(batchItem);
        }

        public void End()
        {
            if (!begin) throw new Exception("Begin batch first");
            GL.Clear(ClearBufferMask.ColorBufferBit);
            foreach(Batch batch in batches)
            {
                batch.Finish();
                defaultShader.Use();

                defaultShader.SetUniformMat4(projectionMatrix, "projection");
                defaultShader.SetUniformMat4(viewMatrix, "view");

                batch.BindTextures(defaultShader.GetHandle());

                GL.BindVertexArray(batch.VA);
                GL.DrawElements(BeginMode.Triangles, batch.quadCount * 6, DrawElementsType.UnsignedInt, 0);
                drawCallCount++;
                GL.BindVertexArray(0);

            }
            begin = false;
            end = true;
            viewMatrix = Matrix4.Identity;
            foreach(Batch b in batches)
            {
                b.Reset();
            }
        }
    }
}
