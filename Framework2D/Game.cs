using Framework2D.Assets;
using Framework2D.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework2D
{
    public class Game
    {
        Core core;
        public Game(int width, int height, string title)
        {
             core = new Core(this, width, height, title);
        }

        public void Run()
        {
            core.Run();
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(float deltaTime)
        {

        }

        public virtual void Draw(BatchRenderer batchRenderer)
        {

        }
    }
}
