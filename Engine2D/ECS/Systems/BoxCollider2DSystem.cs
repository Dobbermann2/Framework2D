
using Engine2D.Physics;
using Framework2D.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    class BoxCollider2DSystem : System
    {
        public override void Draw(Component component, BatchRenderer batchRenderer)
        {

        }

        public override void Update(Component component, float deltaTime)
        {
            ((BoxCollider2DComponent)component).Shape.Update(((BoxCollider2DComponent)component).ShapeTransform);
        }

        public override void OnAdd(Component component)
        {
            if (!component.Entity.HasComponent<RigidBodyComponent>()) return;
            RigidBodyComponent rb = component.Entity.GetComponent<RigidBodyComponent>();
            BoxCollider2DComponent box = (BoxCollider2DComponent)component;
            box.Shape = Physics2D.AddBoxCollider(rb, box.Width, box.Height, box.Radius);
            box.ShapeTransform = new ChipmunkSharp.cpTransform();
        }

        public override void OnRemove(Component component)
        {

        }
    }
}
