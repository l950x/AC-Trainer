using System;
using AC_Trainer;
using System.Numerics;
using System.Reflection.PortableExecutable;
using Swed32;
using System.Net.Sockets;
using System.Runtime.InteropServices;


class Program
{


    static void Main()
    {
        Renderer renderer = new Renderer();
        Thread rendererThread = new Thread(new ThreadStart(renderer.Start().Wait));

        Swed swed = new Swed("ac_client");
        IntPtr moduleBase = swed.GetModuleBase("ac_client.exe");
        int AmmoAddressOffset = 0x190844;
        int localPlayerOffset = 0x0017E0A8;
        int healthOffset = 0xEC;
        int YOffset = 0x30;
        //int cameraYOffset = 0x38;
        //int healthSubInstruction = 0x84499;
        //string healthBytes = "89 82 EC 00 00 00";
        IntPtr localPlayerAddress = swed.ReadPointer(moduleBase, localPlayerOffset);
        IntPtr ammoAddress = swed.ReadPointer(moduleBase, AmmoAddressOffset, 0x0);


        while (true)
        {

            /*
            if (GetAsyncKeyState(0x01) < 0) {
                float cameraY = swed.ReadFloat(localPlayerAddress, cameraYOffset);
                swed.WriteFloat(localPlayerAddress, cameraYOffset, cameraY);
                Thread.Sleep(10);
            }
            */

            if (renderer.getAmmo)
            {
                setAmmo(ammoAddress, swed, 1000);
            }
            else
            {
                int ammoValue = swed.ReadInt(ammoAddress, 0x140);
                if (ammoValue > 50)
                {
                    setAmmo(ammoAddress, swed, 30);
                }
            }

            if (renderer.getHealth)
            {
                setHealth(localPlayerAddress, healthOffset, swed, 100);
            }

            if (renderer.fly)
            {
                float Y = swed.ReadFloat(localPlayerAddress, YOffset);
                toggleFly(localPlayerAddress, swed, Y, YOffset);
            }

        }
    }

    static public void setAmmo(IntPtr ammoAddress, Swed swed, int value)
    {
        swed.WriteInt(ammoAddress, 0x140, value);
    }

    static public void setHealth(IntPtr localPlayerAddress, int healthOffset, Swed swed, int value)
    {
        swed.WriteInt(localPlayerAddress, healthOffset, value);
    }

    static DateTime lastUpdateTime = DateTime.Now;

    static public void toggleFly(IntPtr localPlayerAddress, Swed swed, float Y, int YOffset)
    {
        if (GetAsyncKeyState(0x20) < 0)
        {
            if (Y <= 9)
            {
                float newPosition = Y + 0.0005f;
                swed.WriteFloat(localPlayerAddress, YOffset, newPosition);
                lastUpdateTime = DateTime.Now;
            }
            else
            {
                Y = 2;
            }

        }

        if (Y > 5 && (DateTime.Now - lastUpdateTime).TotalSeconds > 1)
        {
            Y = 1;
            swed.WriteFloat(localPlayerAddress, YOffset, Y);
        }

    }


    [DllImport("user32.dll")]
    static extern short GetAsyncKeyState(int vKey);

}
