using Framework2D.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework2D.Assets
{
    public static class AssetManager
    {
        private static Dictionary<string, Texture2D> cachedTextures = new Dictionary<string, Texture2D>();


        public static Texture2D LoadTexture2D(string source)
        {
            Texture2D tex;
            if(!cachedTextures.TryGetValue(source, out tex))
            {
                tex = new Texture2D(source);
                cachedTextures.Add(source, tex);
            }
            return tex;
        }


    }
}
