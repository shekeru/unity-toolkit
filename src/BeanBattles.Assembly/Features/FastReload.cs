namespace BeanAssembly.Features
{
    class FastReload : Feature
    {
        Weapon weapon;
        // Config
        public override string
            NAME => "Fast Reload";
        // Defaults
        static float reloadTime;
        // Logics
        public override void UpdateLocal()
        {
            StoreDefaults(Instance.equips.weapons
                [Instance.equips.currentWeapon], ref weapon);
            // Fucking Hell
            if (SIGNAL)
            {
                weapon.reloadTime = 1e-9f;
            }
            else
                LoadDefaults(weapon);
        }
    }
}
