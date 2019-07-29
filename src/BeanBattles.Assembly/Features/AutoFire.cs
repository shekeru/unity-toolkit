namespace BeanAssembly.Features
{
    class AutoFire : Feature
    {
        Weapon weapon;
        // Config
        public override string
            NAME => "Force Auto";
        // Defaults
        static bool fullAuto;
        static float recoveryTime;
        // Logics
        public override void UpdateLocal()
        {
            StoreDefaults(Instance.equips.weapons
                [Instance.equips.currentWeapon], ref weapon);
            // Fucking Hell
            if (SIGNAL)
            {
                weapon.fullAuto = true;
                weapon.recoveryTime = 1e-6f;
            }
            else
                LoadDefaults(weapon);
        }
    }
}
