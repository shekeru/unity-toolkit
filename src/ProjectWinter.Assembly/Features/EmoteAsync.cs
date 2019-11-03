using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WinterAssembly.Features
{
    class EmoteAsync : Feature
    {
        // Config
        public override string
            NAME => "EmoteAsync";
        public override int
            SECTION => 1;
        // Logics
        System.Random rnd = new System.Random();
        DateTime last = DateTime.Now;
        public override void UpdateLocal()
        {
            // Fucking Hell
            if (SIGNAL)
            {
                var playerChar = PlayerHandler.LocalPlayerInstance.PlayerCharControllerRef;
                var emoteHandler = Instance.PrivateField<EmoteHandler>
                    (PlayerHandler.LocalPlayerInstance, "emoteHandler");
                //emoteHandler.photonView.RPC("DoShowEmote", PhotonTargets.All, new object[]{index});
                DateTime current = DateTime.Now;
                if(((TimeSpan)(current - last)).TotalMilliseconds > 450) {
                    var index = rnd.Next(emoteHandler.defaultEmotes.Length);
                    var emote = emoteHandler.defaultEmotes[index];
                    playerChar.photonView.RPC("PlayEmoteAnim",
                        PhotonTargets.Others, new object[] { emote.animKey });
                }
            }
        }
    }
}
