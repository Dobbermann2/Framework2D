using System;
using System.Collections.Generic;
using System.Text;

namespace Engine2D
{
    public class ScriptComponent : Component
    {
        public GameScript Script { get; set; }
        public bool IsDirty { get; set; } = true;

        public void SetScript(GameScript script)
        {
            Script = script;
            IsDirty = true;
        }
    }
}
