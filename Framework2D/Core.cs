using Framework2D.Graphics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL4;
using Framework2D.Inputs;

namespace Framework2D
{
    class Core : GameWindow
    {
        Game game;
        BatchRenderer batchRenderer;
        public Core(Game game, int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { API = ContextAPI.OpenGL, APIVersion = new Version(4, 6),  Size = new OpenTK.Mathematics.Vector2i(width, height), Title = title })
        {
            VSync = VSyncMode.Off;

            GL.Viewport(0,0,width, height);
            this.game = game;
            this.batchRenderer = new BatchRenderer(width, height);
            game.Initialize();
            Run();
        }
        
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            Input.Update(KeyboardState, MouseState);
            base.OnUpdateFrame(args);
            game.Update((float)args.Time);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            game.Draw(batchRenderer);
            SwapBuffers();
        }
    }
}
