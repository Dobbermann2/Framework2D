using System;
using Engine2D;
using Framework2D.Assets;

namespace EngineSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game();
            g.Run();
        }
    }

    class Game : EngineGame
    {
        public Game() : base(1280, 720, "Title")
        {

        }
        Entity e;

        public override void Initialize()
        {
            e = Scene.CreateEntity();
            SpriteComponent spriteComp = e.AddComponent<SpriteComponent>();
            spriteComp.Texture = AssetManager.LoadTexture2D("textureA.png");

            e.AddComponent<ScriptComponent>().Script = new PlayerController();
        }
    }
}
