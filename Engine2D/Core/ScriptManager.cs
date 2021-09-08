using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    public static class ScriptManager
    {
        public static Dictionary<string, ScriptAsset> scriptAssets = new Dictionary<string, ScriptAsset>();


        public static ScriptAsset GetScript(string path)
        {
            ScriptAsset script = null;
            if (scriptAssets.TryGetValue(path, out script))
            {
                return script;
            }
            else
            {
                ScriptAsset newScript = new ScriptAsset(path);
                if(newScript.Compile())
                {
                    scriptAssets.Add(newScript.SourcePath, newScript);
                    return newScript;
                }
                
            }
            return null;

        }
        
    }
}
