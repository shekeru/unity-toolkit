using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using System;
using Steamworks;

namespace Bombergrounds
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

        // Init Everything
        public void Start()
        {
            // Init Lists
            features = new List<Feature>();
            keys = new KeyManager();
            // Init Objects
            Update();
        }
        // Disregard Frame Skips for now
        public void Update()
        {
            // Update Vars
         
            // Update Locals
            //Interface.Toggle ^= keys[KeyManager.Interface];
            //keys.Update(); UpdateLocal(); //BypassPasswords();

            // Update Game Settings
            GlobalVariables.Instance.UserId = "cats";
            GlobalVariables.Instance.UserRole = "Admin";
            GlobalVariables.Instance.Username = "cats";

        }
    }
}
