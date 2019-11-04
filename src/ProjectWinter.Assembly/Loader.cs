using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using System;
using Steamworks;

namespace WinterAssembly
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
        // Objects
        public static GameManager game;
        public static LobbyHandler lobby;
        public static LevelManager level;
        // Init Everything
        public void Start()
        {
            // Init Lists
            features = new List<Feature>();
            keys = new KeyManager();
            // Init Objects
            features.Add(new
                Features.ForceReady());
            features.Add(new
                Features.ForceEvent());
            features.Add(new
                Features.AddStats());
            features.Add(new
                Features.BreakSpine());
            features.Add(new
                Features.EmoteAsync());
            features.Add(new
                Features.NewPerson());
            features.Add(new
                Features.ForceTraitors());
            features.Add(new
                Features.ChangeRole());
            features.Add(new
                Features.Reconnect());
            Update();
        }
        // Disregard Frame Skips for now
        public void Update()
        {
            // Update Vars
            game = GameManager.Instance;
            lobby = game.LobbyHandlerRef;
            level = game.LevelManagerRef;
            // Update Locals
            Interface.Toggle ^= keys[KeyManager.Interface];
            keys.Update(); UpdateLocal(); BypassPasswords();
            // Update Game Settings
            ForceValue(game.SteamDLCManagerRef,
                "hasSupernaturalDLCPurchased", true);
            PhotonNetwork.playerName = "Lauren";
            game.playFabId = "cats2";
            // testing
            //GameManager.IsServerCheckRequired = false;
            //game.PlayFabRef.playFabStatus = ConnectionManager.EStatus.ONLINE;
            // Meh
            //var exiles = PrivateField<Dictionary<int, bool>>(level.ExileManagerRef, "isExiled");
            //    exiles[PhotonNetwork.player.ID] = false;
            //level.ExileManagerRef.ForceGlobalExile(false);
            // Fuck Niggers
        }
    }
}
