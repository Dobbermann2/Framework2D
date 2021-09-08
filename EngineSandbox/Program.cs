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

        public override void Initialize()
        {
            Entity player = Scene.CreateEntity();

            player.AddComponent<TagComponent>().Tag = "Player";

            SpriteComponent spriteComp = player.AddComponent<SpriteComponent>();
            spriteComp.Texture = AssetManager.LoadTexture2D("textureA.png");

            player.AddComponent<ScriptComponent>().SetScript("PlayerController.cs");
        }
    }
}
