

using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D.Physics
{
    public static class Physics2D
    {
        private static ChipmunkSharp.cpSpace world;

        public static void Init(float gravity = 9.81f)
        {
            world = new ChipmunkSharp.cpSpace();
            world.SetGravity(new ChipmunkSharp.cpVect(0, gravity));
        }

        public static void Step(float deltaTime)
        {
            world.Step(deltaTime);
        }

        public static void AddRigidBody(RigidBodyComponent rb)
        {
            if (rb.RigidBody != null) return;
            ChipmunkSharp.cpBody body = new ChipmunkSharp.cpBody(rb.Mass, ChipmunkSharp.cp.PHYSICS_INFINITY);

            body.SetBodyType((ChipmunkSharp.cpBodyType)rb.Type);

            world.AddBody(body);
            
            rb.RigidBody = body;

            body.SetPosition(ToVect(rb.Transform.Position));
        }

        public static ChipmunkSharp.cpPolyShape AddBoxCollider(RigidBodyComponent rb, int width, int height, float radius)
        {
            if(rb.RigidBody == null) { return null; }
            ChipmunkSharp.cpPolyShape shape = ChipmunkSharp.cpPolyShape.BoxShape(rb.RigidBody, width, height, radius);
            shape.Active();
            shape.SetCollisionType(1);
            shape.SetSensor(false);
            world.AddShape(shape);
            return shape;
        }

        public static Vector2 ToVector2(ChipmunkSharp.cpVect vec)
        {
            return new Vector2(vec.x, vec.y);
        }

        public static ChipmunkSharp.cpVect ToVect(Vector2 vector)
        {
            return new ChipmunkSharp.cpVect(vector.X, vector.Y);
        }
    }

}
