
using Engine2D.Physics;
using Framework2D.Graphics;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    class RigidBodySystem : System
    {
        public override void Draw(Component component, BatchRenderer batchRenderer)
        {

        }

        public override void Update(Component component, float deltaTime)
        {
            ((RigidBodyComponent)component).RigidBody.UpdatePosition(deltaTime);
            component.Transform.Position = Physics2D.ToVector2(((RigidBodyComponent)component).RigidBody.GetPosition());
        }

        public override void OnAdd(Component component)
        {
            RigidBodyComponent rb = (RigidBodyComponent)component;
            Physics2D.AddRigidBody(rb);
        }

        public override void OnRemove(Component component)
        {

        }
    }
}
