using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework2D
{
    class Core : GameWindow
    {
        Game game;
        public Core(Game game, int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { API = ContextAPI.OpenGL, APIVersion = new Version(4, 6),  Size = new OpenTK.Mathematics.Vector2i(width, height), Title = title })
        {
            this.game = game;
            Run();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            game.Update();

            if (KeyboardState.IsAnyKeyDown) Close();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            SwapBuffers();

        }
    }
}
