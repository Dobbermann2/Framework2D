using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    public class TagComponent : Component
    {
        public string Tag { get; set; }

        public override void Serialize(JObject obj)
        {
            JObject array = new JObject();
            base.Serialize(obj);
            array["eID"] = Entity.EntityID;
            array["tag"] = Tag;
        }

        public override void Deserialize(JObject obj)
        {
            Tag = (string)obj["tag"];
        }
    }
}
