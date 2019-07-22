using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

namespace BeanAssembly
{
    public class NiggyHook : MonoBehaviour
    {
        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 200, 40), "Niggerhook v0.2~"); int loc = 60;
            //var serverManager = GameObject.Find("Server Manager").GetComponent<ServerManager>();
            var gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();;
            foreach (var player in gameManager.players) {
                try {
                    var movement = player.GetComponent<Movement>();
                    var local = player.GetComponent<SetUpLocalPlayer>();
                    if (movement.enabled && !local.isSpectating)
                        GUI.Label(new Rect(45, loc += 40, 200, 40), 
                            local.pname);
                    if (movement.isLocalPlayer)
                    {
                        // Enable Rocket Boots
                        movement.rocketJumpEnabled = true;
                        movement.boostPower = 100f;
                    }           
                } catch { }
            }
        }
    }
}

