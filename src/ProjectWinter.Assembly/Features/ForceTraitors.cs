using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WinterAssembly.Features
{
    class ForceTraitors : Feature
    {
        // Config
        public override string
            NAME => "AllTraitors";
        public override int
            SECTION => 2;
        // Logics
        public override void UpdateLocal()
        {
            // Fucking Hell
            if (SIGNAL)
            {
                var players = Instance.PrivateField<Dictionary<int, PlayerData>>
                    (Instance.game.LevelManagerRef, "players").Keys.ToArray();
                foreach(int id in players) {
                    var player = Instance.level.GetPlayerHandler(id, true);
                    PlayerHandler.LocalPlayerInstance.photonView.RPC("OnSwapPlayerClass", 
                        PhotonTargets.AllViaServer, 
                        (object)player.photonView.viewID, 
                        (object)ePlayerClass.TRAITOR);
                }; SIGNAL = !SIGNAL;
            }
        }
    }
}
