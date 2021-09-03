using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Framework2D.Graphics
{
    public class BatchRenderer
    {

        public Shader defaultShader;

        private Matrix4 projectionMatrix;

        public Matrix4 ProjectionMatrix
        {
            get { return projectionMatrix; }
        }

        public Batch batch;

        bool begin = false;
        bool end = true;

        public BatchRenderer(int width, int height)
        {
            defaultShader = new Shader("vUnlit.glsl", "fUnlit.glsl");
            projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, 0.0f, 100f);
        }


        public void Begin()
        {
            if (begin) throw new Exception("End the batch first");
            begin = true;
            end = false;
        }


        public void Draw(Texture2D texture, Vector2 position)
        {
            if (batch == null) batch = new Batch();
            //Add to batch
            batch.AddItem(new BatchItem
            {
                Texture = texture,
                Scale = Vector2.One,
                Position = position
            }) ;
        }

        public void End()
        {
            if (!begin) throw new Exception("Begin batch first");
            //Render batch

            GL.Clear(ClearBufferMask.ColorBufferBit);
            //foreach(Batch batch in batch)
            batch.Finish();
            defaultShader.Use();
            defaultShader.SetUniformMat4(ProjectionMatrix, "projection");
            batch.BindTextures(defaultShader.GetHandle());

            GL.BindVertexArray(batch.VA);
            GL.DrawElements(BeginMode.Triangles, batch.quadCount*6, DrawElementsType.UnsignedInt, 0);

            begin = false;
            end = true;

            batch = new Batch();

        }
    }
}
