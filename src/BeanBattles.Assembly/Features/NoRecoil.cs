using System;
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
        public static bool Toggle;
        public static string Title
            = "No Recoil";
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
            if (Toggle) {
                weapon.verticalKick = 0f;
                weapon.additionalSideKick = 0f;
                weapon.sideKick = 0f;
            } else
                LoadDefaults(weapon);
        }
    }
}
