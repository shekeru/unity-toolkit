namespace BeanAssembly.Features
{
    class RocketBoost : Feature
    {
        Movement movement;
        // Config
        public override string
            NAME => "Rocket Boots";
        // Defaults
        static float boostPower;
        static bool rocketJumpEnabled;
        static float movementSpeed;
        static float sprintSpeed;
        static float jumpSpeed;
        // Logics
        public override void UpdateLocal()
        {
            StoreDefaults(Instance.gameManager
                .myPlayerMovement, ref movement);
            // Fucking Hell
            if (SIGNAL)
            {
                movement.boostPower = 100f;
                movement.rocketJumpEnabled = true;
                movement.movementSpeed = 14f;
                movement.sprintSpeed = 24f;
                movement.jumpSpeed = 90f;
            }
            else
                LoadDefaults(movement);
        }
    }
}