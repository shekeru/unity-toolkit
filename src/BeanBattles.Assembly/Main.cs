using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using System;

namespace BeanAssembly
{
    enum Keys
    {
        AirStrike = KeyCode.Keypad9,
        ForceAuto = KeyCode.Keypad6,
        Speedup = KeyCode.Keypad5,
        Shotgun = KeyCode.Keypad4,
        ForceBoots = KeyCode.Keypad2,
    };
    partial class NiggyHook : MonoBehaviour
    {
        // Hack State
        List<GameObject> players = new List<GameObject>();
        Dictionary<Keys, bool> keys = new Dictionary<Keys, bool>();
        // Unity Classes
        CustomNetworkManager netManager; GameManager gameManager;
        SetUpLocalPlayer localPlayer; Extras extras;
        public void Start() {
            keys[Keys.AirStrike] = false;
            keys[Keys.Shotgun] = false;
            keys[Keys.Speedup] = false;
            keys[Keys.ForceAuto] = false;
        }
        public void Update() {
            // Update Values
            gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();
            localPlayer = gameManager.myPlayer.GetComponent<SetUpLocalPlayer>();
            extras = gameManager.myPlayer.GetComponent<Extras>();
            // Keybind Checks
            try {
                foreach (var kcode in keys.Keys)
                    keys[kcode] = Input.GetKeyDown((KeyCode)kcode);
            } catch {} 
            // Update Players
            foreach (var player in gameManager.players) {
                try { UpdatePlayer(player); } catch {};
            }; UpdateLocal();
            // Clear Entries
            foreach (var player in players)
                if (!gameManager.players.Contains(player))
                    players.Remove(player);
            // Fun AirStrike
            if(keys[Keys.AirStrike])
                localPlayer.Chat("[Server] Fuck You.", true, false);
        }
        public void OnGUI()
        {
            GUI.contentColor = new Color(0, 116, 217, 1);
            GUI.Label(new Rect(10, 4, 200, 40), "Niggyhook, Version 5.1");
            netManager = GameObject.Find("NetworkManager").GetComponent<CustomNetworkManager>();
            GUI.Label(new Rect(10, 600, 200, 40), netManager.steamInfo.steamDisplayName.text);
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

