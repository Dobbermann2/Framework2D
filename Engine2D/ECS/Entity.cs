using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    public class Entity
    {

        public Scene Context { get; private set; }
        public int EntityID { get; private set; }

        public TransformComponent Transform { get { return GetComponent<TransformComponent>(); } }

        public Entity(Scene context, int id)
        {
            this.Context = context;
            this.EntityID = id;
        }

        public T AddComponent<T>() where T : Component, new()
        {
            return Context.AddComponent<T>(this);
        }

        public T GetComponent<T>() where T : Component
        {
            return Context.GetComponent<T>(this);
        }

        public bool HasComponent<T>() where T : Component
        {
            return Context.HasComponent<T>(this);
        }
    }
}
