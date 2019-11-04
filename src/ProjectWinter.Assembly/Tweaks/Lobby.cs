using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace WinterAssembly
{
    partial class Instance
    {
        public void BypassPasswords()
        {
            // Fuck Passwords Lol
            var lobbies = GameObject.FindObjectsOfType<ChooseLobbyRoomSlotUI>();
            for (var i = 0; i <= lobbies.Length; i++)
            {
                try
                {
                    // Fuck You Too Cocksuckers
                    lobbies[i].kickedFromGame = false;
                    var popup = PrivateField<LobbyPasswordPopup>(lobbies[i], "passwordPopup");
                    var input = PrivateField<TMP_InputField>(popup, "passwordBox");
                    // Swap Values
                    input.inputType = TMP_InputField.InputType.Standard;
                    input.text = popup.password;
                    // Force Join
                }
                catch { }
            }
        }
        // Load Defaults
        public static Field PrivateField<Field>(object src, string name)
        {
            return (Field)src.GetType().GetField(name, BindingFlags.NonPublic
                | BindingFlags.Instance).GetValue(src);
        }
        // Load Defaults
        public static void ForceValue<Type>(object src, string name, Type value)
        {
            src.GetType().GetField(name, BindingFlags.NonPublic
                | BindingFlags.Instance).SetValue(src, value);
        }
    }
}
