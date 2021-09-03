using System;

using Framework2D;

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

        public override void Draw()
        {
            base.Draw();
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
