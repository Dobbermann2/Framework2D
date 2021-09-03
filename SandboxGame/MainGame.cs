using System;

using Framework2D;
using Framework2D.Graphics;

namespace SandboxGame
{
    class MainGame : Game
    {

        Texture2D tex;

        public MainGame() : base(1280, 720, "Title")
        {
        }

        public override void Initialize()
        {
            tex = new Texture2D("texture.png");

        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(BatchRenderer batchRenderer)
        {
            batchRenderer.Begin();
            batchRenderer.Draw(tex, new OpenTK.Mathematics.Vector2(0, 0));
            batchRenderer.Draw(tex, new OpenTK.Mathematics.Vector2(512, 512));
            batchRenderer.Draw(tex, new OpenTK.Mathematics.Vector2(256, 256));


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
