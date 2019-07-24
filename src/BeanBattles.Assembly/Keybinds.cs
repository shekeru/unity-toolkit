using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BeanAssembly
{
    class KeyManager : Dictionary<KeyCode, bool>
    {
        public const KeyCode
            AirStrike = KeyCode.Keypad9,
            Crasher = KeyCode.Keypad5;
        // Logic
        public KeyManager()
        {
            this[AirStrike] = false;
            this[Crasher] = false;
        }
        public void Update()
        {
            foreach (var code in Keys.ToArray())
                this[code] = Input.GetKeyDown(code);
        }
    }
}
