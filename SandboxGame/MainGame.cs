using System;
using System.Collections.Generic;
using Framework2D;
using Framework2D.Graphics;
using Framework2D.Inputs;
using OpenTK.Mathematics;

namespace SandboxGame
{
    class MainGame : Game
    {

        Texture2D spritesheet;

        SubTexture2D grassTile;
        SubTexture2D trashTile;

        Texture2D texA;

        public MainGame() : base(1280, 720, "Title")
        {

        }

        public override void Initialize()
        {
            spritesheet = new Texture2D("tilemap_packed.png");
            grassTile = new SubTexture2D(spritesheet, new TextureRegion(16, 16, 16, 16));
            trashTile = new SubTexture2D(spritesheet, new TextureRegion(128, 144, 16, 16));

            texA = new Texture2D("textureA.png");
        }

        Vector3 camPos;
        
        public override void Update(float deltaTime)
        {
            if(Input.IsKeyDown(Key.D)) {
                camPos.X += 100 * deltaTime;
            }
            if (Input.IsKeyDown(Key.A))
            {
                camPos.X -= 100 * deltaTime;
            }
            if (Input.IsKeyDown(Key.W))
            {
                camPos.Y -= 100 * deltaTime;
            }
            if (Input.IsKeyDown(Key.S))
            {
                camPos.Y += 100 * deltaTime;
            }
            base.Update(deltaTime);
        }

        public override void Draw(BatchRenderer batchRenderer)
        {
            batchRenderer.Begin(Matrix4.CreateScale(0.3f) * Matrix4.CreateTranslation(-camPos));
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    batchRenderer.Draw(grassTile, new OpenTK.Mathematics.Vector2(x * 256, y * 256), new Vector2(4f), 0, new Vector2(0.5f));

                }
            }
            batchRenderer.End();

            base.Draw(batchRenderer);
        }
    }

    class Entry
    {
        static public void Main(String[] args)
        {
            MainGame m = new MainGame();
        }
    }
}
