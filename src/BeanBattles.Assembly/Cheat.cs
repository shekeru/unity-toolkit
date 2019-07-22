using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

namespace BeanAssembly
{
    public class NiggyHook : MonoBehaviour
    {
        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 200, 40), "Niggerhook v0.1~"); int loc = 60;
            var serverManager = GameObject.Find("Server Manager").GetComponent<ServerManager>();
            var gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();
            foreach (var player in serverManager.playersList) {
                GUI.Label(new Rect(45, loc+=40, 200, 40), player.name);
            }
        }
        public void Update()
        {
            
        }
    }
}

