using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterAssembly.Features
{
    class ForceEvent : Feature
    {
        // Config
        public override string
            NAME => "ForceEvent";
        public override int
            SECTION => 1;
        // Logics
        public override void UpdateLocal()
        {
            // Fucking Hell
            if (SIGNAL)
            {
                Instance.game.LevelManagerRef.GlobalEventManagerRef
                    .ForceStartEvent(GlobalEventData.eGlobalEventType.LOOT_DROP);
                SIGNAL = !SIGNAL;
            }
        }
    }
}
