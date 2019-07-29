using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using System;

namespace BeanAssembly
{
    // Loading Component
    public class Loader
    {
        // Unity Loader
        static UnityEngine.GameObject gameObject;
        public static void Load()
        {
            gameObject = new UnityEngine.GameObject();
                gameObject.AddComponent<Instance>();
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
        }
        public static void Unload()
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }
    // Basic Functions
    partial class Instance : MonoBehaviour
    {
        // Internal
        public static KeyManager keys;
        public static List<Feature> features;
        public static List<GameObject> players;
        // Managers
        public static GameManager gameManager;
        public static CustomNetworkManager netManager;
        public static ServerManager serverManager;
        // Game Player
        public static WeaponManager equips;
        public static SetUpLocalPlayer localPlayer;
        public static Extras extras;
        // Init Everything
        public void Start()
        {
            // Init Lists
            features = new List<Feature>();
            players = new List<GameObject>();
            keys = new KeyManager();
            // Add Features
            features.Add(new
                Features.NoRecoil());
            features.Add(new
                Features.ShotgunMode());
            features.Add(new
                Features.FastReload());
            features.Add(new
                Features.AutoFire());
            features.Add(new
                Features.NoReload());
            features.Add(new
                Features.RocketBoost());
        }
        // Disregard Frame Skips for now
        public void Update()
        {
            // Update Values
            serverManager = GameObject.Find("Server Manager").GetComponent<ServerManager>();
            gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();
            localPlayer = gameManager.myPlayer.GetComponent<SetUpLocalPlayer>();
            extras = gameManager.myPlayer.GetComponent<Extras>(); keys.Update();
            equips = localPlayer.GetComponent<WeaponManager>();
            // Change Names
            localPlayer.pname = localPlayer.playerNameText.text 
                = netManager.playerName;
            // Begin Updates
            foreach (var player in gameManager.players) {
                try { UpdatePlayer(player); } catch { };
            };
            Interface.Toggle ^= keys[KeyManager.Interface]; UpdateLocal();
            // Reset Entries
            foreach (var player in players)
                if (!gameManager.players.Contains(player))
                    players.Remove(player);
            // Fun AirStrikes
            if (keys[KeyManager.AirStrike])
                localPlayer.Chat("[Server] Airstrike Inbound", true, false);
            if (keys[KeyManager.Crasher])
            {
                localPlayer.Chat("[Hacker] May Allah Bless You", true, false);
                var size = gameManager.mapSize / 20;
                for (int y = -size; y < size; y++)
                    for (int x = -size; x < size; x++)
                        extras.CallCmdAirStrikePos(x * 10, y * 10, 1, localPlayer.netId);
            }
            if (keys[KeyManager.Ferrets])
                localPlayer.Chat("[Ferrets are Cool]\n" + String.Concat(Enumerable.
                    Repeat("ௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌௌ", 200).ToArray()), true, false);
        }
    }
}


