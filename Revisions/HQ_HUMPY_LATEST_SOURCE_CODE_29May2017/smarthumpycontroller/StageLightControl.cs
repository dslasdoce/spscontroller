using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHumpyController
{


    public enum StackLightControlStage_Boxing
    {
        NA,
        Startup,
        ReadingStarted,
        ReadingStopped,
        Check_25minsWarning,
        Check_30minsLimit,
        InvalidBagCheck,
        ValidTag
    }

    public enum StackLightControlStage_Palletising
    {
        NA,
        Startup,
        ReadingStarted,
        ReadingStopped,
        InvalidBagCheck,
        ValidTag
    }
}
