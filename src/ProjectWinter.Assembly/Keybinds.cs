using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace WinterAssembly
{
    class KeyManager : Dictionary<KeyCode, bool>
    {
        public const KeyCode
            Interface = KeyCode.Insert;
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
