namespace BeanAssembly.Features
{
    class Airstrike : Feature
    {
        Extras extras;
        // Config
        public override string
            NAME => "Airstrike(All)";
        public override int
            SECTION => 2;
        // Logics
        public override void UpdatePlayer()
        {
            StoreDefaults(Instance.extras, ref extras);
            // Fucking Hell
            if (SIGNAL)
            {
               
            }
            else
                LoadDefaults(extras);
        }
    }
}