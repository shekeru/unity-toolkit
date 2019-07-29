using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BeanAssembly.Features
{
    class ForceShotgun : Feature
    {
        Weapon weapon;
        // Config
        public override string
            NAME => "Auto-Shotgun";
        // Defaults
        static bool fullAuto;
        static float recoveryTime;
        static bool shotgun;
        // Logics
        public override void UpdateLocal()
        {
            StoreDefaults(Instance.equips.weapons
                [Instance.equips.currentWeapon], ref weapon);
            // Fucking Hell
            if (SIGNAL) {
                weapon.fullAuto = true;
                weapon.recoveryTime = 1e-6f;
                weapon.shotgun = true;
            } else
                LoadDefaults(weapon);
        }
    }
}
