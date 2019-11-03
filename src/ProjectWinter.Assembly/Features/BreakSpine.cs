using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WinterAssembly.Features
{
    class BreakSpine : Feature
    {
        // Config
        public override string
            NAME => "BreakSpine";
        public override int
            SECTION => 2;
        // Logics
        public override void UpdateLocal()
        {
            // Fucking Hell
            if (SIGNAL)
            {
                PlayerHandler.LocalPlayerInstance.PlayerAnimControllerRef
                    .transform.rotation = UnityEngine.Random.rotation;
            }
        }
    }
}
