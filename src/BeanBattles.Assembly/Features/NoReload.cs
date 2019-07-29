namespace BeanAssembly.Features
{
    class NoReload : Feature
    {
        Weapon weapon;
        // Config
        public override string
            NAME => "No Reload";
        // Defaults
        // Logics
        public override void UpdateLocal()
        {
            StoreDefaults(Instance.equips.weapons
                [Instance.equips.currentWeapon], ref weapon);
            // Fucking Hell
            if (SIGNAL)
            {
                weapon.reloading = false;
                weapon.currentclip = 20;
            }
            else
                LoadDefaults(weapon);
        }
    }
}
