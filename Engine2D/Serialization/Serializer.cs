using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using System.Text.Json;
using System.Text.Json.Serialization;
    
namespace Engine2D.Serialization
{
    public static class Serializer
    {


        public static void SerializeScene(Scene scene)
        {
            JObject jsonObject = new JObject();
            
            foreach(List<Component> c in scene.Components.Values)
            {
                foreach(Component comp in c)
                {
                    comp.Serialize(jsonObject);
                }
            }

            File.WriteAllText("save.txt", jsonObject.ToString());
        }

        public static void DeserializeScene(Scene scene)
        {
            scene.Clear();
            string json = File.ReadAllText("save.txt");
            JObject obj = JObject.Parse(json);

            foreach (var x in obj)
            {
                DeserializeComponent(x.Key, (JObject) x.Value, scene);
            }
        }

        private static void DeserializeComponent(string component, JObject obj, Scene scene)
        {
            Entity entity = scene.GetOrCreateEmptyEntity((int)obj["eID"]);
            switch (component)
            {
                case "TransformComponent":
                    entity.AddComponent<TransformComponent>().Deserialize(obj);
                    break;

                case "SpriteComponent":
                    entity.AddComponent<SpriteComponent>().Deserialize(obj);
                    break;

                case "TagComponent":
                    entity.AddComponent<TagComponent>().Deserialize(obj);
                    break;

                case "ScriptComponent":
                    entity.AddComponent<ScriptComponent>().Deserialize(obj);
                    break;
            }
        }


    }
}
