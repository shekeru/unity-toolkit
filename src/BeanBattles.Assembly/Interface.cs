﻿using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

namespace BeanAssembly
{
    class Interface
    {
        int w, h;
        // Statics
        public static bool Toggle = true;
        public const string Name = "Niggyhook, Version 5.5.3";
        // Toggles
        public static bool
            instantKill, noRecoil, autoFire, 
            rocketBoots, friendlyFire;
        // Create
        public Interface(string text)
        {
            w = Screen.width / 3; h = Screen.height / 3;
            GUI.Box(new Rect(w, h, w, h), text);
            // Experimental
            Button("No Recoil",
                ref noRecoil);
            Button("Auto-Shotgun", 
                ref autoFire);
            Button("Friendly Fire", 
                ref friendlyFire);
            Button("Movement+", 
                ref rocketBoots);
            Button("Instant Kill", 
                ref instantKill);
            Button("Close Menu",
                ref Toggle);
        }
        public Rect NextSlot()
        {
            return new Rect(w+5, h += 30, 100, 25);
        }
        public void Button(string text, ref bool value)
        {
            GUI.contentColor = value ? Color.green : Color.red;
            value ^= GUI.Button(NextSlot(), text);
            GUI.contentColor = Color.white;
        }
        public static void Label(int x, int y, int w, int h, string text)
        {
            GUI.contentColor = new Color(140 / 256f, 240 / 256f, 115 / 256f, 1f);
            GUI.Label(new Rect(x, y, w, h), text);
            GUI.contentColor = Color.white;
        }
    }
    partial class BeanAbuser : MonoBehaviour
    {
        public void OnGUI()
        {
            netManager = GameObject.Find("NetworkManager").GetComponent<CustomNetworkManager>();
            // Basic Interface
            Interface.Label(Screen.width - 155, 0, 160, 35, Interface.Name);
            if (Interface.Toggle)
                new Interface(Interface.Name);
            // Edit MatchDatas
            var matches = (MatchUp.Match[])typeof(CustomNetworkManager).GetField("matches",
                BindingFlags.NonPublic | BindingFlags.Instance).GetValue(netManager);
            //foreach (var listing in matches) {
            //    listing.matchData["maxPlayers"] = 20;
            //    listing.matchData["matchIsFull"] = 0;
            //}
            // Bypass cocksucker passwords
            var match = ((MatchUp.Match)typeof(CustomNetworkManager).GetField("tryingToJoinMatch",
               BindingFlags.NonPublic | BindingFlags.Instance).GetValue(netManager)).matchData;
            netManager.passwordEntryInput.text = match["Match Password"];
            netManager.passwordEntryTitle.text =
                match["externalIP"] + ":" + match["port"];
            // Debug Print
            //int position = 110;
            //foreach (var key in match) GUI.Label(new
            //    Rect(10, position += 22, 400, 25), key.ToString());
        }
    }
}

//netManager.steamFriendManager.banPlayerList = null;
//try
//{
//    var eff = netManager.steamFriendManager;
//    //eff.bMan.playButton.gameObject.SetActive(true);
//    eff.bMan.playButton.GetComponent<Button>().Select();
//    eff.IdNotificationObj.gameObject.SetActive(false);
//}
//catch (Exception e)
//{
//    GUI.Label(new Rect(2, 400, 160, 200), "Error: " + e.Message);
//};  GUI.Label(new Rect(2, 40, 160, 20), "ID Status: " +
//   Cursor.lockState);
// Inspect Match Data
//var menus = netManager.menuMatchPanel.GetComponentsInChildren<MenuMatch>();
//GUI.Label(new Rect(10, 76, 400, 25), "Matches: " + menus.Length.ToString());
//foreach (var menu in menus) {
//    menu.fullText.gameObject.SetActive(false);
//    menu.buttonObj.gameObject.SetActive(true);
//}