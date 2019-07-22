using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

namespace BeanAssembly
{
    public class NiggyHook : MonoBehaviour
    {
        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 200, 40), "Niggyhook v0.2~"); int loc = 60;
            //var serverManager = GameObject.Find("Server Manager").GetComponent<ServerManager>();
            var gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();;
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
                        // Enable Rocket Boots
                        movement.rocketJumpEnabled = true;
                        movement.boostPower = 100f;
                        movement.movementSpeed = 12f;
                        movement.sprintSpeed = 20f;
                        movement.jumpSpeed = 90f;
                        // Change Weapon Manager Values
                        var equips = movement.GetComponent<WeaponManager>();
                        var active = equips.weapons[equips.currentWeapon];
                        // Zero Recoil Values
                        active.additionalSideKick = 0f;
                        active.verticalKick = 0f;
                        active.sideKick = 0f;
                        // Better Shot Handling
                        active.bulletSpeed = 1000f;
                        active.reloadTime = 0.0001f;
                        active.currentclip = 500;
                        active.reloading = false;
                        // Meme Shit
                        active.shotgun = true;
                        active.recoveryTime = 1e-9f;
                        active.recovering = false;
                        active.fullAuto = true;
                        //active.burstAmount = 4;
                        //active.burst = true;
                        var health = player.GetComponent<Health>();
                        health.healing = true;
                    }
                } catch { }
            }
        }
    }
}

