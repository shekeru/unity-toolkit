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
        bool[] arr;
        public bool OPENGUI = false;
        public override void UpdateGUI()
        {
            if (!OPENGUI)
                return;
            var ui = new Interface("Select Event");
            var events = Enum.GetValues(typeof(GlobalEventData.eGlobalEventType))
                .Cast<GlobalEventData.eGlobalEventType>().ToArray();
            if (arr == null)
                arr = new bool[events.Length];
            foreach (GlobalEventData.eGlobalEventType value in events) {
                ui.Button(value.ToString(), ref arr[(int) value]);
                if (arr[(int)value]) {
                    Instance.game.LevelManagerRef.GlobalEventManagerRef
                        .ForceStartEvent(value);
                    OPENGUI = false; arr = null;
                }

            }
        }
        public override void UpdateLocal()
        {
            // Fucking Hell
            if (SIGNAL)
            {
                Interface.Toggle = false;
                OPENGUI = SIGNAL;
                SIGNAL = !SIGNAL;
            }
        }
    }
}
