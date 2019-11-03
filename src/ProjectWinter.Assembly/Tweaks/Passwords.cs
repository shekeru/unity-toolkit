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
        void BypassPasswords()
        {
            // Fuck Passwords Lol
            var lobbies = GameObject.FindObjectsOfType<ChooseLobbyRoomSlotUI>();
            for (var i = 0; i <= lobbies.Length; i++)
            {
                try
                {
                    // Fuck You Too Cocksuckers
                    var popup = (LobbyPasswordPopup)typeof(ChooseLobbyRoomSlotUI).GetField
                        ("passwordPopup", BindingFlags.NonPublic | BindingFlags.Instance).
                    GetValue(lobbies[i]);
                    var input = (TMP_InputField)typeof(LobbyPasswordPopup).GetField
                        ("passwordBox", BindingFlags.NonPublic | BindingFlags.Instance).
                    GetValue(popup);
                    // Swap Values
                    input.inputType = TMP_InputField.InputType.Standard;
                    input.text = popup.password;
                }
                catch { }
            }
        }
    }
}
