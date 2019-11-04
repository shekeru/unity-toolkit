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
    class Print {
        public Color color = Color.white;
        public int x, y, w, h; public GUIStyle style;
        public Print(int x, int y, int w, int h) {
            this.x = x; this.y = y; this.w = w; this.h = h;
            style = GUI.skin.GetStyle("Label");
        }
        public void SetColor(int r, int g, int b) {
            color = new Color(r / 256f, g / 256f, b / 256f, 1f);
        }
        public void Label(string text)
        {
            GUI.contentColor = color;
            style.alignment = TextAnchor.UpperLeft;
            GUI.Label(new Rect(x, y, w, h), text, style);
            y += h;
        }
    }
    class Interface
    {
        public int x, y, w, h;
        public static Color Blue = new
            Color(119 / 256f, 174 / 256f, 230 / 256f, 1f);
        public static Color Green = new
            Color(140 / 256f, 240 / 256f, 115 / 256f, 1f);
        public static Color Red = new
            Color(242 / 256f, 95 / 256f, 44 / 256f, 1f);
        // Public Statics
        public const string Name = "hrtWare-0.1.3";
        public static bool Toggle = true;
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
            if (text == Interface.Name)
                DefaultMenu();
            else
            {
                x = Screen.width / 3;
                y = Screen.height / 3;
                w = x * 2; h = y * 2;
                //GUI.Box(new Rect(x, y, w, h), text);
            }
        }
        public void DefaultMenu() {
            x = w = Screen.width / 3;
            y = h = Screen.height / 3;
            Cursor.lockState = CursorLockMode.None;
            GUI.Box(new Rect(x, y, w, h), Interface.Name);
            // New Features Methods
            for (int i = 0; i < Headers.Length; i++)
                {
                    var style = GUI.skin.GetStyle("Label");
                    style.alignment = TextAnchor.MiddleCenter;
                    GUI.contentColor = Blue;
                    GUI.Label(AfterLabel(), Headers[i], style);
                    foreach (var feature in Instance.features)
                    {
                        if (feature.SECTION != i) continue;
                        GUI.contentColor = feature.SIGNAL ? Green : Red;
                        feature.SIGNAL ^= GUI.Button(AfterButton(), feature.NAME);
                        GUI.contentColor = Color.white;
                    }
                    x += 105; y = Screen.height / 3;
                } // Misc Shit
                Button("Close Menu",
                    ref Toggle);
            }
        public Rect AfterButton()
        {
            return new Rect(x + 5, y += 30, 100, 25);
        }
        public Rect AfterLabel()
        {
            return new Rect(x + 5, y += 25, 100, 25);
        }
        public void Button(string text, ref bool value)
        {
            GUI.contentColor = value ? Color.green : Color.red;
            value ^= GUI.Button(AfterButton(), text);
            GUI.contentColor = Color.white;
        }
    }
    partial class Instance : MonoBehaviour
    {
        public void OnGUI()
        {
            // Basic Interface
            var header = new Print(2, 0, 1500, 20);
            header.color = Interface.Blue;
            header.style.alignment = 
                TextAnchor.UpperLeft;
            header.Label(Interface.Name);
            // Overlay
            if (Interface.Toggle)
                new Interface(Interface.Name);
            UpdateGUI();
            // Testing Features
            header.y += 250; header.x = 2;
            PhotonNetwork.AuthValues.UserId = "cats";
            header.Label(PhotonNetwork.AuthValues.UserId);
            header.Label(PhotonNetwork.AuthValues.AuthGetParameters);
            if (game.ConnectionManagerRef.gameStatus != ConnectionManager.EStatus.ONLINE)
                game.ConnectionManagerRef.ChangeGameStatus(ConnectionManager.EStatus.ONLINE);
            try
            {
                var players = PrivateField<Dictionary<int, PlayerData>>(game.LevelManagerRef, "players");
                foreach (var entry in players)
                {
                    var player = level.GetPlayerHandler(entry.Key, true);
                    header.color = Color.magenta;
                    // Display List
                    header.Label("[" + entry.Key.ToString() + ", "
                        + player.PlayerClass.ToString() + "]: "
                        + player.PlayerName.ToString().StripNoParse() + " -- "
                        + player.CurrentHealth.ToString());
                };
            }
            catch {

            }
        }
        public void FixedUpdate()
        {
            // Fuck Full Servers
        }
    }
}
