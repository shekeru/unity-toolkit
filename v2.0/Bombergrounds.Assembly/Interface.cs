using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using TMPro;

namespace Bombergrounds
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
            // Misc Shit
          
        }
        public void FixedUpdate()
        {
            // Fuck Full Servers
            var local = GameManager.Instance.localPlayer;
            local.username = "cats"; 
            local.playerCollision = false;
            local.canUseMeleeWeapon = true;
            local.disableWeapon = false;
            local.updatePlayer = true;
            local.canMove = true;
            local.bombCount = 3;
            //
            //FUCKLOL(local);
            //local.hitbox.SetActive(false);
            // Client
            local.client_send_redundant_inputs = false;
            //local.client_enable_corrections = false;
            //local.client_correction_smoothing = false;
            // Weak
            ForceValue(local, "died", false);
            ForceValue(local, "stunned", false);
            ForceValue(local, "freezeMovement", false);
            ForceValue(local, "colliding", false);
            // Weh
            Instance.TestingM(local);
        }
        public static void FUCKLOL(PlayerController lcl)
        {
            Dictionary<short, byte> dictionary1 = new Dictionary<short, byte>();
            Dictionary<short, Direction> dictionary2 = new Dictionary<short, Direction>();
            Vector3 position = lcl.transform.position + Vector3.up * lcl.size * .5f; Vector3 vector3;
            foreach (Component component1 in Physics.OverlapSphere(position, lcl.size + 500.5f, (int)lcl.bombCollisionMask, QueryTriggerInteraction.Ignore))
            {
                BombController component2 = component1.GetComponent<BombController>();
                if (!dictionary1.ContainsKey(component2.ID))
                {
                    Vector3 forward = lcl.transform.forward;
                    vector3 = new Vector3(component2.transform.position.x, 0.0f, component2.transform.position.z) - new Vector3(position.x, 0.0f, position.z);
                    if ((double)Vector3.Dot(vector3.normalized, forward) >= 0.0)
                    {
                        vector3 = new Vector3(component2.transform.position.x, 0.0f, component2.transform.position.z) - new Vector3(position.x - forward.x * 0.5f, 0.0f, position.z - forward.z * 0.5f);
                        Vector3 normalized = vector3.normalized;
                        Direction cardinalDirection = MathUtility.GetCardinalDirection(new Vector2(normalized.x, normalized.z));
                        dictionary1.Add(component2.ID, (byte)cardinalDirection);
                    }
                }
            }
            foreach (Component component1 in Physics.OverlapSphere(position, lcl.size + 500.5f, (int)lcl.playerHitboxMask, QueryTriggerInteraction.Ignore))
            {
                PlayerController component2 = component1.transform.parent.parent.GetComponent<PlayerController>();
                if (!dictionary2.ContainsKey(component2.id) && (int)component2.id != (int)lcl.id)
                {
                    vector3 = component2.transform.position - position;
                    if ((double)Vector3.Dot(vector3.normalized, lcl.transform.forward) > 0.0)
                        dictionary2.Add(component2.id, Direction.None);
                }
            }
            NetMessage msg = new NetMessage();
            msg.WriteByte((byte)36);
            msg.WriteByte((byte)dictionary1.Count);
            msg.WriteByte((byte)dictionary2.Count);
            foreach (KeyValuePair<short, byte> keyValuePair in dictionary1)
            {
                msg.WriteInt16(keyValuePair.Key);
                msg.WriteByte(keyValuePair.Value);
            }
            foreach (short key in dictionary2.Keys)
                msg.WriteInt16(key);
            GameClient.Instance.SendMessage(msg);
        }
    }
}
