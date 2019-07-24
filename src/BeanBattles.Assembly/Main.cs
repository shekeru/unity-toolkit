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
            // Update Players
            foreach (var player in gameManager.players) {
                try { UpdatePlayer(player); } catch {};
            }; UpdateLocal();
            // Clear Entries
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
        // Deprecated
        public void OnGUI()
        {
            netManager = GameObject.Find("NetworkManager").GetComponent<CustomNetworkManager>();
            GUI.contentColor = new Color(140/256f, 240/256f, 115/256f, 1f); GUI.Label(new 
                Rect(Screen.width - 155, 0, 160, 35), "Niggyhook, Version 5.4.2");
            GUI.Label(new Rect(2, Screen.height - 18, 160, 20), "Map Size: " +
                gameManager?.mapSize.ToString());
            // Inspect Match Data
            //var menus = netManager.menuMatchPanel.GetComponentsInChildren<MenuMatch>();
            //GUI.Label(new Rect(10, 76, 400, 25), "Matches: " + menus.Length.ToString());
            //foreach (var menu in menus) {
            //    menu.fullText.gameObject.SetActive(false);
            //    menu.buttonObj.gameObject.SetActive(true);
            //}
            // Edit MatchDatas
            var matches = (MatchUp.Match[]) typeof(CustomNetworkManager).GetField("matches",
                BindingFlags.NonPublic | BindingFlags.Instance).GetValue(netManager);
            //foreach (var listing in matches) {
            //    listing.matchData["maxPlayers"] = 20;
            //    listing.matchData["matchIsFull"] = 0;
            //}
            // Bypass cocksucker passwords
            var match = ((MatchUp.Match)typeof(CustomNetworkManager).GetField("tryingToJoinMatch",
               BindingFlags.NonPublic | BindingFlags.Instance).GetValue(netManager)).matchData;
            netManager.passwordEntryInput.text = match["Match Password"];
            netManager.passwordEntryTitle.text =
                match["externalIP"] + ":" + match["port"];
            // Debug Print
            //int position = 110;
            //foreach (var key in match) GUI.Label(new
            //    Rect(10, position += 22, 400, 25), key.ToString());
        }
        // Privates
        void BurstFire(Weapon active)
        {
            active.spreadActive = true;
            active.spreadFactor = 12f;
            active.fullAuto = false;
            active.burstAmount = 1234567890;
            active.fireRate = 1e-14f;
            active.hasToCock = false;
            active.cockTime = 1e-6f;
            active.burst = true;
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
}

