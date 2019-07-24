using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BeanAssembly
{
    class KeyManager : Dictionary<KeyCode, bool>
    {
        public const KeyCode
            ShootDev = KeyCode.KeypadDivide,
            KatanaBug = KeyCode.KeypadMinus,
            AirStrike = KeyCode.Keypad9,
            KillAll = KeyCode.Keypad2,
            Crasher = KeyCode.Keypad5,
            Ferrets = KeyCode.Keypad7;
        // Logic
        public KeyManager()
        {
            GetType().GetFields(BindingFlags.Static | BindingFlags.Public);
            this[KatanaBug] = false;
            this[AirStrike] = false;
            this[KillAll] = false;
            this[Crasher] = false;
            this[Ferrets] = false;
        }
        public void Update()
        {
            foreach (var code in Keys.ToArray())
                this[code] = Input.GetKeyDown(code);
        }
    }
}
