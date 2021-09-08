using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    public abstract class GameScript
    {
        public Scene Scene { get; set; }
        public TransformComponent Transform { get; set; }
        public Entity Entity { get; set; }

        public abstract void OnCreate();
        public abstract void OnUpdate(float deltaTime);
        //public abstract void OnRender();

    }
}
