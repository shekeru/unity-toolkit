using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using System;

namespace BeanAssembly
{
    partial class BeanAbuser : MonoBehaviour
    {
        // State Variables
        List<GameObject> players; KeyManager keys;
        CustomNetworkManager netManager; GameManager gameManager;
        SetUpLocalPlayer localPlayer; Extras extras;
        public void Start() {
            players = new List<GameObject>();
            keys = new KeyManager();
        }
        // Can Skip
        public void Update() {
            // Update Values
            var server = GameObject.Find("Server Manager").GetComponent<ServerManager>();
            gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();
            localPlayer = gameManager.myPlayer.GetComponent<SetUpLocalPlayer>();
            extras = gameManager.myPlayer.GetComponent<Extras>(); keys.Update();
            Interface.Toggle ^= keys[KeyManager.Interface];
            // Update Players
            foreach (var player in gameManager.players) {
                try { UpdatePlayer(player); } catch {};
            }; UpdateLocal();
            // Reset Entries
            foreach (var player in players)
                if (!gameManager.players.Contains(player))
                    players.Remove(player);
            // Fun AirStrikes
            if (keys[KeyManager.AirStrike])
                localPlayer.Chat("[Server] Airstrike Inbound", true, false);
            if (keys[KeyManager.Crasher]) {
                localPlayer.Chat("[Hacker] May Allah Bless You", true, false);
                var size = gameManager.mapSize / 20;
                for (int y = -size; y < size; y++)
                    for (int x = -size; x < size; x++)
                        extras.CallCmdAirStrikePos(x*10, y*10, 1, localPlayer.netId);
            }
            if (keys[KeyManager.Ferrets])
                localPlayer.Chat("[Ferrets are Cool]\n" + String.Concat(Enumerable.
                    Repeat("ௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌ", 200).ToArray()), true, false);
        }      
    }
    enum Raids:long
    {
        NONE = 0,
        GRENADE = 1,
        AIRSTRIKE = 2,
        UNKNOWN = 3,
        SMOKE = 4,
    };
    // Server Sided Shit
    //extras.CallCmdHeal(localPlayer.netId, 100);
    //var health = localPlayer.GetComponent<Health>();
    //extras.CallCmdHeal(health.netId, 100);
    //health.NetworkcurrentHealth = health.currentHealth = 100;
    //health.Networkdead = health.dead = false;
    //health.NetworkhealthPack = health.healthPack = true;
    //health.Networkhealing = health.healing = true;
    //health.invincible = true;
    //health.graceTime = 1e8f;
    //health.CallRpcBloodHitSpawn(localPlayer.transform.position, 100, true);
}
// Privates
//void BurstFire(Weapon active)
//{
//    active.spreadActive = true;
//    active.spreadFactor = 12f;
//    active.fullAuto = false;
//    active.burstAmount = 1234567890;
//    active.fireRate = 1e-14f;
//    active.hasToCock = false;
//    active.cockTime = 1e-6f;
//    active.burst = true;
//}

