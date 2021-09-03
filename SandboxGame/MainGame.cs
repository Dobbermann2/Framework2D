using System;

using Framework2D;
using Framework2D.Graphics;

namespace SandboxGame
{
    class MainGame : Game
    {
        public MainGame() : base(1280, 720, "Title")
        {

        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(BatchRenderer batchRenderer)
        {
            batchRenderer.Begin();
            batchRenderer.Draw(null, OpenTK.Mathematics.Vector2.One*2);
            batchRenderer.Draw(null, OpenTK.Mathematics.Vector2.One * 0);

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
