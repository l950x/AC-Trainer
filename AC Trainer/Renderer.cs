using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ClickableTransparentOverlay;
using ImGuiNET;
using Vortice.Mathematics;

namespace AC_Trainer
{

    internal class Renderer : Overlay
    {
        private bool prevKeyState = false;
        public Vector2 screenSize = new Vector2(1536, 864);

        public bool getAmmo = false;
        public bool getHealth = false;
        public bool rapidFire = false;
        public bool fly = false;
        public bool noReload = false;


        protected override void Render()
        {
            ImGui.PushStyleColor(ImGuiCol.WindowBg, new Vector4(0.161f, 0f, 0.141f, 0.5f));
            ImGui.PushStyleColor(ImGuiCol.Header, new Vector4(0.227f, 0f, 0.196f, 1.0f));
            ImGui.PushStyleColor(ImGuiCol.TitleBgActive, new Vector4(0.227f, 0f, 0.196f, 1.0f));
            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
            ImGui.PushStyleColor(ImGuiCol.FrameBg, new Vector4(0.33f, 0f, 0.298f, 1f));
            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 10.0f);
            ImGui.SetNextWindowSize(new Vector2(300, 150), ImGuiCond.Always);
            ImGui.Begin("Assault Cube Trainer v1.0", ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.AlwaysAutoResize);
            ImGui.Checkbox("Infinite Ammo", ref getAmmo);
            ImGui.Checkbox("God Mode", ref getHealth);
            ImGui.Checkbox("Rapid Fire", ref rapidFire);
            ImGui.Checkbox("Fly", ref fly);
            ImGui.Checkbox("No Reload", ref noReload);
            ImGui.End();

            DrawOverlay(screenSize);

            //prevKeyState = keyState;

            ImGui.PopStyleColor(5);
            ImGui.PopStyleVar(1);
        }

        void DrawOverlay(Vector2 screenSize)
        {
            ImGui.SetNextWindowSize(screenSize);
            ImGui.SetNextWindowPos(new Vector2(0, 0));
            ImGui.Begin("overlay", ImGuiWindowFlags.NoDecoration
                | ImGuiWindowFlags.NoBackground
                | ImGuiWindowFlags.NoBringToFrontOnFocus
                | ImGuiWindowFlags.NoMove
                | ImGuiWindowFlags.NoInputs
                | ImGuiWindowFlags.NoCollapse
                | ImGuiWindowFlags.NoScrollbar
                | ImGuiWindowFlags.NoScrollWithMouse
                );
        }

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);
    }
}
