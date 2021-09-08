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
        public override void OnCreate()
        {
            
        }

        public override void OnUpdate(float deltaTime)
        {
            Vector2 position = Transform.Position;
            position.X += 100 * deltaTime;
            Transform.Position = position;
        }
    }
}
