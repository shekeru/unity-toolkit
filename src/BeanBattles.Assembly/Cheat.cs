using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using System;

namespace BeanAssembly
{
    public class NiggyHook : MonoBehaviour
    {
        int ticker = 0; System.Random rng = new System.Random();
        float MIN_VALUE = -350; float MAX_VALUE = 350; int alt = 0;
        private void OnGUI()
        {
            int loc = 60; GUI.contentColor = Color.cyan;
            GUI.Label(new Rect(10, 10, 200, 40), "Niggyhook v0.3~");
            GUI.contentColor = Color.yellow; // Game Managers
            var gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();
            foreach (var player in gameManager.players) {
                try
                {
                    var movement = player.GetComponent<Movement>();
                    var local = player.GetComponent<SetUpLocalPlayer>();
                    if (movement.enabled && !movement.isLocalPlayer && !local.isSpectating)
                    { 
                        GUI.Label(new Rect(45, loc += 40, 200, 40),
                            local.pname);
                    }
                    if (movement.isLocalPlayer)
                    {
                        var extras = player.GetComponent<Extras>();
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
                        active.recoveryTime = 1e-4f;
                        //active.recovering = false;
                        active.fullAuto = true;
                        //Aids(active);
                        //if (ticker++ % 10 <= 0)
                        //{
                        //    extras.CallCmdAirStrikePos((float)rng.NextDouble() * (MAX_VALUE - MIN_VALUE) + MIN_VALUE,
                        //        (float)rng.NextDouble() * (MAX_VALUE - MIN_VALUE) + MIN_VALUE, (rng.Next(500) > 2 ?4:2),
                        //        player.GetComponent<NetworkIdentity>().netId);
                        //}
                    }
                } catch { }
            }
        }
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

