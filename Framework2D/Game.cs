using Framework2D.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework2D
{
    public class Game
    {
        private Core core;

        public Game(int width, int height, string title)
        {
            core = new Core(this, width, height, title);
            //Initialize framework here
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw(BatchRenderer batchRenderer)
        {

        }
    }
}
