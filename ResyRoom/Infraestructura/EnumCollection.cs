using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResyRoom.Infraestructura
{
    public enum ViewState
    {
        Display = 1,
        Edit = 2
    }

    public enum RegisterStudioPage 
    {
        AccountInfo = 1,
        GeneralInfo = 2,
        RoomsInfo = 3,
        EquipmentInfo = 4,
        ScheduleInfo = 5
    }
}