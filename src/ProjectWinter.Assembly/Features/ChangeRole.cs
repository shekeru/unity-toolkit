using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WinterAssembly.Features
{
    class ChangeRole : Feature
    {
        // Config
        public override string
            NAME => "BeTraitor";
        public override int
            SECTION => 1;
        // Logics
        public override void UpdateLocal()
        {
            // Fucking Hell
            if (SIGNAL)
            {
                //PlayerHandler.LocalPlayerInstance.photonView.RPC("OnSwapPlayerClass", 
                //    PhotonTargets.AllViaServer, 
                //    (object)PlayerHandler.LocalPlayerInstance.photonView.viewID, 
                //    (object)ePlayerClass.TRAITOR);
                PlayerHandler.LocalPlayerInstance.MakeMeTraitor();
            SIGNAL = !SIGNAL;
            }
        }
    }
}
