using System;
using System.Collections.Generic;
using System.Linq;
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
                extras.CallCmdAirStrikePos(coords.x, coords.z, 2,
                   player.GetComponent<NetworkIdentity>().netId);
                extras.CallCmdAirStrikePos(coords.x, coords.z, 4,
                   player.GetComponent<NetworkIdentity>().netId);
            }
        }
        void FixedUpdate()
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
            active.currentclip = 99;
            // Meme Shit
            active.shotgun = true;
            active.recoveryTime = 1e-6f;
            active.fullAuto = true;
            // Friendly Fire
            localPlayer.rTeams = false;
        }
    }
}
