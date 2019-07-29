using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BeanAssembly
{
    class KeyManager : Dictionary<KeyCode, bool>
    {
        public const KeyCode
            Interface = KeyCode.Insert,
            ShootDev = KeyCode.KeypadDivide,
            KatanaBug = KeyCode.KeypadMinus,
            AirStrike = KeyCode.Keypad9,
            KillAll = KeyCode.Keypad2,
            Crasher = KeyCode.Keypad5,
            Ferrets = KeyCode.Keypad7;
        // Logic
        public KeyManager()
        {
            foreach (var code in GetType().GetFields
                (BindingFlags.Static | BindingFlags.Public))
            this[(KeyCode)code.GetValue(this)] = false;
        }
        public void Update()
        {
            foreach (var code in Keys.ToArray())
                this[code] = Input.GetKeyDown(code);
        }
    }
}
