using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{   
    public class ScriptComponent : Component
    {
        public GameScript Script { get; private set; }
        public bool IsDirty { get; set; } = true;
        private string scriptPath;
        public void SetScript(string path)
        {
            this.scriptPath = path;
            Script = (GameScript) Activator.CreateInstance(ScriptManager.GetScript(path).Type);
            IsDirty = true;
        }
        public override void Serialize(JObject obj)
        {
            JObject array = new JObject();
            base.Serialize(obj);
            array["eID"] = Entity.EntityID;
            array["script"] = scriptPath;
            obj["ScriptComponent"] = array;
        }

        public override void Deserialize(JObject obj)
        {
            this.scriptPath = (string) obj["script"];
            Script = (GameScript)Activator.CreateInstance(ScriptManager.GetScript(scriptPath).Type);
        }
    }
}
