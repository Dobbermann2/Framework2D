using Engine2D.Physics;
using Framework2D;
using Framework2D.Graphics;
using System;

namespace Engine2D
{
    public class EngineGame : Game
    {
        public Scene Scene { get; private set;}

        public EngineGame(int width, int height, string title) : base(width, height, title)
        {
            Scene = new Scene();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(BatchRenderer batchRenderer)
        {
            batchRenderer.Begin();
            Scene.Draw(batchRenderer);
            base.Draw(batchRenderer);
            batchRenderer.End();
        }

        public override void Update(float deltaTime)
        {
            Physics2D.Step(deltaTime);
            Scene.Update(deltaTime);
            base.Update(deltaTime);
        }
    }
}
