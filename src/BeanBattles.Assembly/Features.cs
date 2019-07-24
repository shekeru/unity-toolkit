using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace BeanAssembly
{
    partial class BeanAbuser
    {
        void UpdatePlayer(GameObject player)
        {
            var movement = player.GetComponent<Movement>();
            if (movement.isLocalPlayer) return;
            var local = player.GetComponent<SetUpLocalPlayer>();
            // Display Players
            if (!players.Contains(player) && local.pname != "player" &&
                gameManager.myPlayerMovement.enabled && movement.enabled
                && !localPlayer.isSpectating && !local.isSpectating)
            {
                localPlayer.NewTeamMate(player, local.pname,
                    local.playerColor); players.Add(player);
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
        }
        public void FixedUpdate()
        {
            var movement = gameManager.myPlayerMovement;
            // Enable Rocket Boots
            movement.rocketJumpEnabled = true;
            movement.boostPower = 100f;
            movement.movementSpeed = 14f;
            movement.sprintSpeed = 24f;
            movement.jumpSpeed = 90f;
        }
        void UpdateLocal()
        {
            // Change Weapon Manager Values
            var equips = localPlayer.GetComponent<WeaponManager>();
            var active = equips.weapons[equips.currentWeapon];
            // Zero Recoil Values
            active.additionalSideKick = 0f;
            active.verticalKick = 0f;
            active.sideKick = 0f;
            // Better Shot Handling
            active.bulletSpeed = 1e6f;
            active.reloadTime = 1e-9f;
            active.reloading = false;
            active.currentclip = 95;
            // Meme Shit
            active.fullAuto = true;
            active.recoveryTime = 1e-6f;
            active.shotgun = true;
            // Friendly Fire
            localPlayer.rTeams = false;
        }
    }
}
