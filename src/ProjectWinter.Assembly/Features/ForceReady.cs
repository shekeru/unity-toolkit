using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterAssembly.Features
{
    class ForceReady : Feature
    {
        // Config
        public override string
            NAME => "ForceReady";
        public override int
            SECTION => 0;
        // Logics
        public override void UpdateLocal()
        {
            // Fucking Hell
            if (SIGNAL)
            {
                Instance.lobby.photonView.RPC("ForceReady", 
                    PhotonTargets.AllViaServer, 
                Array.Empty<object>());
                SIGNAL = !SIGNAL;
            }
        }
    }
}
