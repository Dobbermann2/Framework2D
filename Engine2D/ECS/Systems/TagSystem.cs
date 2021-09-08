using Framework2D.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    class TagSystem : System
    {

        public override void Draw(Component component, BatchRenderer batchRenderer)
        {

        }

        public override void Update(Component component, float deltaTime)
        {
            Console.WriteLine(((TagComponent) component).Tag);
        }

        public override void OnAdd(Component component)
        {

        }

        public override void OnRemove(Component component)
        {

        }
    }
}
