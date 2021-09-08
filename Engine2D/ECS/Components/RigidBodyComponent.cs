using Engine2D.Physics;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
namespace Engine2D
{
    public class RigidBodyComponent : Component
    {

        public float Mass { get; set; } = 1f;

        public BodyType Type { get; set; } = BodyType.DYNAMIC;
        public ChipmunkSharp.cpBody RigidBody { get; set; }

        public RigidBodyComponent()
        {

        }

        public void AddForce(Vector2 force)
        {
            
        }
    }

    public enum BodyType
    {
        DYNAMIC = ChipmunkSharp.cpBodyType.DYNAMIC,
        STATIC = ChipmunkSharp.cpBodyType.STATIC,
        KINEMATIC = ChipmunkSharp.cpBodyType.KINEMATIC,
    }
}
