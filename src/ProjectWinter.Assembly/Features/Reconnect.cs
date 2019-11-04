using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WinterAssembly.Features
{
    class Reconnect : Feature
    {
        // Config
        public override string
            NAME => "Reconnect";
        public override int
            SECTION => 0;
        // Logics
        public override void UpdateLocal()
        {
            // Fucking Hell
            if (SIGNAL)
            {
                PhotonNetwork.AuthValues.AuthGetParameters = "";
                PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.NintendoSwitch;
                PhotonNetwork.ConnectUsingSettings("wow");
                SIGNAL = !SIGNAL;
            }
        }
    }
}
