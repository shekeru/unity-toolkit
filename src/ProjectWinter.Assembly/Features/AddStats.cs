﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WinterAssembly.Features
{
    class AddStats : Feature
    {
        // Config
        public override string
            NAME => "MaxStats";
        public override int
            SECTION => 0;
        // Logics
        public override void UpdateLocal()
        {
            // Fucking Hell
            if (SIGNAL)
            {
                var health = Instance.PrivateField<HealthScript>
                    (PlayerHandler.LocalPlayerInstance, "healthScript");
                if(health.CurrentValue < health.maxValue)
                    health.RestoreStat(75, false, false);
                var warmth = Instance.PrivateField<WarmthScript>
                    (PlayerHandler.LocalPlayerInstance, "warmthScript");
                if (warmth.CurrentValue < warmth.maxValue)
                    warmth.RestoreStat(75, false, false);
                var hunger = Instance.PrivateField<HungerScript>
                    (PlayerHandler.LocalPlayerInstance, "hungerScript");
                if (hunger.CurrentValue < hunger.maxValue)
                    hunger.RestoreStat(75, false, false);
                //SIGNAL = !SIGNAL;
                PlayerHandler.LocalPlayerInstance.cooldownDuration = 0f;
                //var inv = PlayerHandler.LocalPlayerInstance
                //    .PlayerInventoryHandlerRef.PlayerInventoryRef;
                var invCtrl = Instance.level.HUDManagerRef.PlayerInventoryControllerRef;
                var cooldown = Instance.PrivateField<ItemUICooldownController>
                    (invCtrl, "uiCooldownController");
                 cooldown.SetFill(1000f);
                //inv.InventoryUpdated = delegate (ItemSlot s){};
    }
        }
    }
}
