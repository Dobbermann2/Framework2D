using Framework2D.Graphics;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    class SpriteSystem : System
    {
        
        public override void Draw(Component component, BatchRenderer batchRenderer)
        {
            SpriteComponent comp = (SpriteComponent)component;
            batchRenderer.Draw(comp.Texture, comp.Transform.Position, comp.Transform.Scale, comp.Transform.Rotation, Vector2.Zero);
        }



        public override void Update(Component component, float deltaTime)
        {
        }

        public override void OnAdd(Component component)
        {

        }

        public override void OnRemove(Component component)
        {

        }
    }
}
