using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using TMPro;

namespace WinterAssembly
{
    class Interface
    {
        int w, h;
        static Color Blue = new
            Color(119 / 256f, 174 / 256f, 230 / 256f, 1f);
        static Color Green = new
            Color(140 / 256f, 240 / 256f, 115 / 256f, 1f);
        static Color Red = new
            Color(242 / 256f, 95 / 256f, 44 / 256f, 1f);
        // Public Statics
        public static bool Toggle = true;
        public const string Name = "hrtWare-0.1.0";
        // Toggles
        public static bool
            instantKill, friendlyFire;
        public static string last_error = "";
        static string[] Headers = {
            "-Stable Features-",
            "-Experimental-",
            "-Misc Triggers-"
        };
        // Create
        public Interface(string text)
        {
            w = Screen.width / 3; h = Screen.height / 3;
            Cursor.lockState = CursorLockMode.None;
            GUI.Box(new Rect(w, h, w, h), text);
            // New Features Method
            for (int i = 0; i < Headers.Length; i++)
            {
                var style = GUI.skin.GetStyle("Label");
                style.alignment = TextAnchor.MiddleCenter;
                GUI.contentColor = Blue;
                GUI.Label(AfterLabel(), Headers[i], style);
                w += 105; h = Screen.height / 3;
            } // Misc Shit
            Button("Friendly Fire",
                ref friendlyFire);
            Button("Instant Kill",
                ref instantKill);
            Button("Close Menu",
                ref Toggle);
            //    Label(0, 0, 400, 20, last_error);
        }
        public Rect AfterButton()
        {
            return new Rect(w + 5, h += 30, 100, 25);
        }
        public Rect AfterLabel()
        {
            return new Rect(w + 5, h += 25, 100, 25);
        }
        public void Button(string text, ref bool value)
        {
            GUI.contentColor = value ? Color.green : Color.red;
            value ^= GUI.Button(AfterButton(), text);
            GUI.contentColor = Color.white;
        }
        public static void Label(int x, int y, int w, int h, string text)
        {
            GUI.contentColor = new Color(140 / 256f, 240 / 256f, 115 / 256f, 1f);
            GUI.Label(new Rect(x, y, w, h), text);
            GUI.contentColor = Color.white;
        }
    }
    partial class Instance : MonoBehaviour
    {
        public void OnGUI()
        {
            // Basic Interface
            Interface.Label(0, 0, 150, 15, Interface.Name);
            if (Interface.Toggle)
                new Interface(Interface.Name);
        }
        public void FixedUpdate()
        {
            // Fuck Full Servers
        }
    }
}
