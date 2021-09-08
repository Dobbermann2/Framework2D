using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    public class Component
    {
        public Entity Entity { get; set; }
        public TransformComponent Transform { get { return Entity.Transform; } }

        public bool Initialized { get; set; }

        public Component()
        {
        }


        public virtual void Serialize(JObject obj)
        {


        }
        public virtual void Deserialize(JObject obj)
        {
        }
    }
}
