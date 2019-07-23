using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using System.Collections.Generic;
using System;

namespace BeanAssembly
{
    public class NiggyHook : MonoBehaviour
    {
        int ticker = 0; System.Random rng = new System.Random();
        float MIN_VALUE = -350; float MAX_VALUE = 350; int alt = 0;
        // State
        GameManager gameManager; SetUpLocalPlayer localPlayer;
        List<GameObject> current = new List<GameObject>();
        public void Update() {
            gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();
            localPlayer = gameManager.myPlayer.GetComponent<SetUpLocalPlayer>();
            foreach (var player in gameManager.players) {
                UpdatePlayer(player);
            }; UpdateLocal();
        }
        public void OnGUI()
        {
            GUI.contentColor = Color.cyan;
            GUI.Label(new Rect(500, 10, 200, 40), "Niggyhook v0.3~");
            GUI.contentColor = Color.white; // Game Managers
        }
        private void UpdatePlayer(GameObject player)
        {
            var movement = player.GetComponent<Movement>();
                if (movement.isLocalPlayer) return;
            var local = player.GetComponent<SetUpLocalPlayer>();
            // Display Players
            if (!current.Contains(player))
            {
              localPlayer.NewTeamMate(player, local.pname, 
                  local.playerColor); current.Add(player);
             // gameManager.SetUpTeammateHudSingle(player);
            }
        }
        private void UpdateLocal() {
            var movement = gameManager.myPlayerMovement;
            // Enable Rocket Boots
            movement.rocketJumpEnabled = true;
            movement.boostPower = 100f;
            movement.movementSpeed = 14f;
            movement.sprintSpeed = 24f;
            movement.jumpSpeed = 90f;
            // Change Weapon Manager Values
            var equips = movement.GetComponent<WeaponManager>();
            var active = equips.weapons[equips.currentWeapon];
            // Zero Recoil Values
            active.additionalSideKick = 0f;
            active.verticalKick = 0f;
            active.sideKick = 0f;
            // Better Shot Handling
            active.bulletSpeed = 1e6f;
            active.reloadTime = 1e-9f;
            active.currentclip = 500;
            active.reloading = false;
            // Meme Shit
            active.shotgun = true;
            active.recoveryTime = 1e-6f;
            //active.recovering = false;
            active.fullAuto = true;
            //Aids(active);
            var extras = gameManager.myPlayer.GetComponent<Extras>();
            //if (ticker++ % 10 <= 0)
            //{
            //    extras.CallCmdAirStrikePos((float)rng.NextDouble() * (MAX_VALUE - MIN_VALUE) + MIN_VALUE,
            //        (float)rng.NextDouble() * (MAX_VALUE - MIN_VALUE) + MIN_VALUE, (rng.Next(500) > 2 ?4:2),
            //        player.GetComponent<NetworkIdentity>().netId);
            //}
            // Friendly Fire
            localPlayer.teammates = new List<GameObject>(gameManager.players);
            localPlayer.teammateNumber = gameManager.players.Length - 1;
            localPlayer.rTeams = false;
        }
        // Privates
        private void BurstFire(Weapon active)
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

