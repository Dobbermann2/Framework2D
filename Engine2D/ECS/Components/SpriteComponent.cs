using Framework2D.Assets;
using Framework2D.Graphics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    public class SpriteComponent : Component
    {
        public Texture2D Texture { get; set; }


        public override void Serialize(JObject obj)
        {
            JObject array = new JObject();
            base.Serialize(obj);
            array["eID"] = Entity.EntityID;
            array["texS"] = Texture.Source;
            obj["SpriteComponent"] = array;
        }

        public override void Deserialize(JObject obj)
        {
            Texture = AssetManager.LoadTexture2D((string)obj["texS"]);
        }
    }
}
