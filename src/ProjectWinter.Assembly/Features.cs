using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace WinterAssembly
{
    partial class Instance
    {
        void UpdatePlayer(GameObject player)
        {
        }
        void UpdateLocal()
        {
            foreach (var feature in features)
                try { feature.UpdateLocal(); } catch 
                (Exception e) {
                    Interface.last_error = 
                        e.Message + '\n' + e.StackTrace;
                } // Misc Features
        }
    }
    abstract class Feature
    {
        public virtual bool SIGNAL
            { get; set; } = false;
        public virtual int
            SECTION { get; } = 1;
        public virtual string NAME { get; }
        // Update Other Players
        public virtual void UpdateLocal() {}
        // Update Local Player
        public virtual void UpdatePlayer() {}
        // Store Defaults
        public void StoreDefaults<T>(T src, ref T dest)
        {
            if (src.Equals(dest))
                return; dest = src;
            var fields = GetType().GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            foreach (var field in fields)
                field.SetValue(this, typeof(T).GetField(field.Name,
            BindingFlags.Instance | BindingFlags.Public).GetValue(src));
        }
        // Load Defaults
        public void LoadDefaults<T>(T obj)
        {
            var fields = GetType().GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            foreach (var field in fields)
                typeof(T).GetField(field.Name, BindingFlags.Instance | BindingFlags
                    .Public).SetValue(obj, field.GetValue(this));
        }
    }
}
