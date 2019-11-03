using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using System;

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
        GameManager game;
        // Init Everything
        public void Start()
        {
            // Init Lists
            features = new List<Feature>();
            keys = new KeyManager();
            // Init Objects
            game = GameManager.m_instance;
        }
        // Disregard Frame Skips for now
        public void Update()
        {
            Interface.Toggle ^= keys[KeyManager.Interface];
                UpdateLocal(); BypassPasswords();
        }
    }
}
