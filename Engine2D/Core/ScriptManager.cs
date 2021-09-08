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
            if (scriptAssets.ContainsKey(path))
            {
                return scriptAssets[path];
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
