﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace BeanAssembly
{
    partial class Instance
    {
        void UpdatePlayer(GameObject player)
        {
            var movement = player.GetComponent<Movement>();
            var local = player.GetComponent<SetUpLocalPlayer>();
                if (movement.isLocalPlayer) return;
            // Display Players
            if (!players.Contains(player) && local.pname != "player" &&
                gameManager.myPlayerMovement.enabled && movement.enabled
                && !localPlayer.isSpectating && !local.isSpectating)
            {
                localPlayer.NewTeamMate(player, local.pname,
                    local.playerColor);  players.Add(player);
            }
            // And fuck to you too
            if (keys[KeyManager.AirStrike])
            {
                var coords = movement.transform.position;
                extras.CallCmdAirStrikePos(coords.x, coords.z, 2, local.netId);
                extras.CallCmdAirStrikePos(coords.x, coords.z, 4, local.netId);
            }
            if (keys[KeyManager.KillAll]) {
                extras.CallCmdVehicleHit(local.netId, movement.transform.position, 100);
                extras.CallCmdShankHit(local.netId, movement.transform.position, 100);
            }
            if (keys[KeyManager.KatanaBug])
            {
                extras.CallCmdPlayAnimation(local.netId, 4, true, true);
                extras.CallCmdPlayAnimation(local.netId, 2, true, false);
                extras.CallCmdPlayAnimation(local.netId, 4, true, true);
                extras.CallCmdPlayAnimation(local.netId, 2, true, false);
            }
            if (keys[KeyManager.ShootDev])
            {
                var equips = localPlayer.GetComponent<WeaponManager>();
                var hit = ((RaycastHit)typeof(WeaponManager).GetField("hit",
                  BindingFlags.NonPublic | BindingFlags.Instance).GetValue(equips));
                equips.CallCmdDealDamage(local.netId,
                    equips.transform.forward, hit.point);
            }
            if(Interface.instantKill) {
                var equips = localPlayer.GetComponent<WeaponManager>();
                var hit = ((RaycastHit)typeof(WeaponManager).GetField("hit",
                  BindingFlags.NonPublic | BindingFlags.Instance).GetValue(equips));
                var target = hit.transform.GetComponent<SetUpLocalPlayer>();
                if (target.netId == local.netId) for(var i = 0; i < 10; i++)
                    equips.CallCmdDealDamage(local.netId, equips.transform.forward, hit.point);
            }
        }
        public void FixedUpdate()
        {
            var movement = gameManager.myPlayerMovement;
            // Enable Rocket Boots
            if (Interface.rocketBoots) {
                movement.rocketJumpEnabled = true;
                movement.boostPower = 100f;
                movement.movementSpeed = 14f;
                movement.sprintSpeed = 24f;
                movement.jumpSpeed = 90f;
            }
        }
        void UpdateLocal()
        {
            // Change Weapon Manager Values
            var active = equips.weapons[equips.currentWeapon];
            // Better Shot Handling
            active.bulletSpeed = 1e6f;
            active.reloadTime = 1e-9f;
            active.reloading = false;
            active.currentclip = 95;
            // Friendly Fire
            localPlayer.rTeams = 
                Interface.friendlyFire;
            foreach (var feature in features)
                try { feature.UpdateLocal(); } catch 
                (Exception e) {
                    Interface.last_error = 
                        e.Message + '\n' + e.StackTrace;
                }
        }
    }
    abstract class Feature
    {
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
