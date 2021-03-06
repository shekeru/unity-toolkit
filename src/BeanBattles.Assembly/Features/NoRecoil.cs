﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BeanAssembly.Features
{
    class NoRecoil : Feature
    {
        Weapon weapon;
        // Config
        public override string 
            NAME => "No Recoil";
        public override int
            SECTION => 0;
        // Defaults
        static float verticalKick;
        static float additionalSideKick;
        static float sideKick;
        // Logics
        public override void UpdateLocal()
        {
            StoreDefaults(Instance.equips.weapons
                [Instance.equips.currentWeapon], ref weapon);
            // Fucking Hell
            if (SIGNAL) {
                weapon.verticalKick = 0f;
                weapon.additionalSideKick = 0f;
                weapon.sideKick = 0f;
            } else
                LoadDefaults(weapon);
        }
    }
}
