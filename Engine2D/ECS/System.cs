using Framework2D.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    internal abstract class System
    {
        public abstract void Update(Component component, float deltaTime);
        public abstract void Draw(Component component, BatchRenderer batchRenderer);

        public abstract void OnAdd(Component component);
        public abstract void OnRemove(Component component);

        public void PreUpdate(Component component)
        {
            if (!component.Initialized)
            {
                OnAdd(component);
                component.Initialized = true;
            }
        }

    }
}
