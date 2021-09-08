using Engine2D;
using Engine2D.Serialization;
using Framework2D.Assets;
using Framework2D.Inputs;
using Newtonsoft.Json.Linq;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineSandbox
{
    class PlayerController : GameScript
    {

        float speed = 100f;

        public override void OnCreate()
        {
            
        }

        public override void OnUpdate(float deltaTime)
        {
            Vector2 position = Transform.Position;
            if (Input.IsKeyDown(Key.A))
            {
                position.X -= speed * deltaTime;
            }
            if (Input.IsKeyDown(Key.D))
            {
                position.X += speed * deltaTime;
            }
            if (Input.IsKeyDown(Key.W))
            {
                position.Y -= speed * deltaTime;
            }
            if (Input.IsKeyDown(Key.S))
            {
                position.Y += speed * deltaTime;
            }
            Transform.Position = position;

            if (Input.IsKeyDown(Key.Space))
            {
                Serializer.SerializeScene(Scene);
            }

            if(Input.IsKeyDown( Key.Enter))
            {
                Serializer.DeserializeScene(Scene);
            }

        }
    }
}
