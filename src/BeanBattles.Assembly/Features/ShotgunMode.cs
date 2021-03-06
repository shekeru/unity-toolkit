﻿namespace BeanAssembly.Features
{
    class ShotgunMode : Feature
    {
        Weapon weapon;
        // Config
        public override string
            NAME => "Shotgun Mode";
        public override int
            SECTION => 0;
        // Defaults
        static bool shotgun;
        // Logics
        public override void UpdateLocal()
        {
            StoreDefaults(Instance.equips.weapons
                [Instance.equips.currentWeapon], ref weapon);
            // Fucking Hell
            if (SIGNAL) {
                weapon.shotgun = true;
            } else
                LoadDefaults(weapon);
        }
    }
}
