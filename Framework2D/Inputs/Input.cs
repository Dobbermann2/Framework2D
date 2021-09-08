using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework2D.Inputs
{
    public static class Input
    {

        static KeyboardState keyboardState;
        static MouseState mouseState;

        public static Vector2 MousePosition
        {
            get { return mouseState.Position; }
        }

        internal static void Update(KeyboardState newKeyboardState, MouseState newMouseState)
        {
            keyboardState = newKeyboardState;
            mouseState = newMouseState;
        }

        public static bool IsKeyDown(Key key)
        {
            return keyboardState.IsKeyDown((Keys) key);
        }

        public static bool IsKeyReleased(Key key)
        {
            return keyboardState.IsKeyReleased((Keys)key);
        }

        public static bool IsKeyPressed(Key key)
        {
            return keyboardState.IsKeyPressed((Keys)key);
        }

        public static bool IsMouseButtonDown(MouseButton button)
        {
            return mouseState.IsButtonDown((OpenTK.Windowing.GraphicsLibraryFramework.MouseButton)button);
        }

        public static bool IsMouseButtonReleased(MouseButton button)
        {
            return mouseState.WasButtonDown((OpenTK.Windowing.GraphicsLibraryFramework.MouseButton)button);
        }

        public static bool IsMouseButtonPressed(MouseButton button)
        {
            return IsMouseButtonReleased(button) && !(IsMouseButtonDown(button));
        }

    }
    public enum MouseButton
    {
        LMB = (int)OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Left,
        RMB = (int)OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Right,
        Middle = (int)OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Middle,

    }

    public enum Key
    {
        A = (int)Keys.A,
        B = (int)Keys.B,
        C = (int)Keys.C,
        D = (int)Keys.D,
        E = (int)Keys.E,
        F = (int)Keys.F,
        G = (int)Keys.G,
        H = (int)Keys.H,
        I = (int)Keys.I,
        J = (int)Keys.J,
        K = (int)Keys.K,
        L = (int)Keys.L,
        M = (int)Keys.M,
        N = (int)Keys.N,
        O = (int)Keys.O,
        P = (int)Keys.P,
        Q = (int)Keys.Q,
        R = (int)Keys.R,
        S = (int)Keys.S,
        T = (int)Keys.T,
        U = (int)Keys.U,
        V = (int)Keys.V,
        W = (int)Keys.W,
        X = (int)Keys.X,
        Y = (int)Keys.Y,
        Z = (int)Keys.Z,

        D1 = (int)Keys.D1,
        D2 = (int)Keys.D2,
        D3 = (int)Keys.D3,
        D4 = (int)Keys.D4,
        D5 = (int)Keys.D5,
        D6 = (int)Keys.D6,
        D7 = (int)Keys.D7,
        D8 = (int)Keys.D8,
        D9 = (int)Keys.D9,
        D0 = (int)Keys.D0,

        Equal = (int)Keys.Equal,
        Minus = (int)Keys.Minus,

        F1 = (int)Keys.F1,
        F2 = (int)Keys.F2,
        F3 = (int)Keys.F3,
        F4 = (int)Keys.F4,
        F5 = (int)Keys.F5,
        F6 = (int)Keys.F6,
        F7 = (int)Keys.F7,
        F8 = (int)Keys.F8,
        F9 = (int)Keys.F9,
        F10 = (int)Keys.F10,
        F11 = (int)Keys.F11,
        F12 = (int)Keys.F12,

        Space = (int)Keys.Space,
        LShift = (int)Keys.LeftShift,
        RShift = (int)Keys.RightShift,
        LAlt = (int)Keys.LeftAlt,
        RAlt = (int)Keys.RightAlt,
        Tab = (int)Keys.Tab,
        Up = (int)Keys.Up,
        Down = (int)Keys.Down,
        Left= (int)Keys.Left,
        Right= (int)Keys.Right,

        Enter = (int)Keys.Enter

    }
}
