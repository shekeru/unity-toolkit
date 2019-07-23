using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

namespace BeanAssembly
{
    public class NiggyHook : MonoBehaviour
    {
        // Hack State
        List<GameObject> players = new List<GameObject>();
        Dictionary<KeyCode, bool> keys = new Dictionary<KeyCode, bool>();
        // Unity Classes
        GameManager gameManager; CustomNetworkManager netManager;
        SetUpLocalPlayer localPlayer; Extras extras;
        public void Start() {
            keys[KeyCode.Keypad9] = false;
        }
        public void Update() {
            // Update Values
            gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();
            localPlayer = gameManager.myPlayer.GetComponent<SetUpLocalPlayer>();
            extras = gameManager.myPlayer.GetComponent<Extras>();
            // Keybind Checks
            try {
                foreach (var kcode in keys.Keys)
                    keys[kcode] = Input.GetKeyDown(kcode);
            } catch {} 
            // Update Players
            foreach (var player in gameManager.players) {
                try { UpdatePlayer(player); } catch {};
            }; UpdateLocal();
            // Clear Entries
            foreach (var player in players)
                if (!gameManager.players.Contains(player))
                    players.Remove(player);
        }
        public void OnGUI()
        {
            GUI.contentColor = Color.cyan;
            GUI.Label(new Rect(10, 10, 200, 40), "Niggyhook, Version 4");
            GUI.contentColor = Color.white; // Game Managers
            netManager = GameObject.Find("NetworkManager").GetComponent<CustomNetworkManager>();
            GUI.Label(new Rect(10, 600, 200, 40),
                netManager.steamInfo.steamDisplayName.text);
            netManager.passwordEntryTitle.text = "--debug";
        }
        private void UpdatePlayer(GameObject player)
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
            if (keys[KeyCode.Keypad9])
            {
                extras.airstrikeDelay = false;
                var coords = movement.transform.position;
                extras.CallCmdAirStrikePos(coords.x, coords.z, 2,
                   player.GetComponent<NetworkIdentity>().netId);
            }
        }
        public void FixedUpdate() {
            var movement = gameManager.myPlayerMovement;
            // Enable Rocket Boots
            movement.rocketJumpEnabled = true;
            movement.boostPower = 100f;
            movement.movementSpeed = 14f;
            movement.sprintSpeed = 24f;
            movement.jumpSpeed = 90f;
        }
        private void UpdateLocal() {
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
            active.currentclip = 500;
            active.reloading = false;
            // Meme Shit
            active.shotgun = true;
            active.recoveryTime = 1e-6f;
            //active.recovering = false;
            active.fullAuto = true;
            // Friendly Fire
            localPlayer.rTeams = false;
            //localPlayer.teammates = new List<GameObject>(gameManager.players);
            //localPlayer.teammateNumber = gameManager.players.Length - 1;
            //localPlayer.NetworkteamNumber = localPlayer.teamNumber = -1;
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

