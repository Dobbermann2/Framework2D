using Framework2D.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework2D
{
    public class Game
    {
        public Game(int width, int height, string title)
        {
            Core core = new Core(this, width, height, title);
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
