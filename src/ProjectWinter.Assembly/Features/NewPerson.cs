using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WinterAssembly.Features
{
    class NewPerson : Feature
    {
        // Config
        public override string
            NAME => "SwapClothes";
        public override int
            SECTION => 2;
        // Logics
        System.Random rnd = new System.Random();
        public override void UpdateLocal()
        {
            // Fucking Hell
            if (SIGNAL)
            {
                var players = Instance.PrivateField<Dictionary<int, PlayerData>>
                    (Instance.game.LevelManagerRef, "players").Keys.ToArray();
                var player = Instance.level.GetPlayerHandler(players[rnd.Next(players.Length)], true);
                PlayerHandler.LocalPlayerInstance.photonView.RPC("OnSwapClothes", 
                    PhotonTargets.AllViaServer, (object) PhotonNetwork.player.ID, 
                    (object) player.photonView.CreatorActorNr);
                SIGNAL = !SIGNAL;
            }
        }
    }
}
