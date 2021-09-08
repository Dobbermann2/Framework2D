using Framework2D.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    class ScriptSystem : System
    {
        public override void Draw(Component component, BatchRenderer batchRenderer)
        {

        }

        public override void Update(Component component, float deltaTime)
        {
            ScriptComponent scriptComp = (ScriptComponent)component;
            if (scriptComp.Script == null) return;

            if (scriptComp.IsDirty)
            {
                scriptComp.Script.Transform = component.Transform;
                scriptComp.Script.Entity = component.Entity;
                scriptComp.Script.Scene = component.Entity.Context;
                scriptComp.Script.OnCreate();
                scriptComp.IsDirty = false;
            }
            scriptComp.Script.OnUpdate(deltaTime);
        }

        public override void OnAdd(Component component)
        {

        }

        public override void OnRemove(Component component)
        {

        }
    }
}
