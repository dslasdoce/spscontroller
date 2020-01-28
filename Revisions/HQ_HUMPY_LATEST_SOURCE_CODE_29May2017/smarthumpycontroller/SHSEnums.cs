using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHumpyController
{

    public class HumpyTablet
    {
        public string mHumpyIP = "";
        public bool mHumpyMasterFlag = false;
        public HumpyTablet(string aIP, bool aMaster)
        {
            mHumpyIP = aIP;
            mHumpyMasterFlag = aMaster;
        }
  
    }

    public enum LightStackColor : ushort { RedOrange = 3, OrangeOnly = 2, GreenOnly = 4, RedOnly = 1, Off = 0, All = 5 };

    public enum SHS_DetailMode
    {
        CheckedInList,
        DetailList   
    }

    public enum SHS_JournalType
    {
        NA,
        INITIAL,
        CHECKIN,
        CHECKOUT,
        SYS,
        RD,
        GPO,
        IN,
        OUT,
        GETDBDATA
    }


    public enum SHS_LogType //IN JL 12-MAY-2016
    {
        NA,
        ACTION,
        ERROR,
        EXCEPTION
    }

    // TaskInfo holds state information for a task that will be
    // executed by a ThreadPool thread.
    public class StacklightInfo
    {
        // State information for the task.  These members
        // can be implemented as read-only properties, read/write
        // properties with validation, and so on, as required.
        public LightStackColor LightColor;

        // Public constructor provides an easy way to supply all
        // the information needed for the task.
        public StacklightInfo(LightStackColor alightColor)
        {
            LightColor = alightColor;
        }
    }

    class SHSEnums
    {
    }

    public enum SHS_CheckInOutStatus
    { 
       CheckIn,CheckOut,NA
    }

    public class SHS_DbReturnColumnNames
    {
        public const string Column1 = "Name";
        public const string Column2 = "CheckedIn";
        public const string Column3 = "PersonUID";
        public const string Column4 = "MISCCard";
        public const string Column5 = "FirstSeenDT";
        public const string Column6 = "LastSeenDT";
        public const string Column7 = "LocId";
        public const string Column8 = "INOUT";
        public const string Column9 = "INOUTIcon";//IN JL16-MAY-16: for icon display purpose
        public const string Column10 = "";
        public const string Column11 = "";
    
    }

    public class SHS_INOUT_Values
    {
        public const string INValue = "IN";
        public const string OUTValue = "OUT";
        public const string NotCheckedInValue = "Not Checked In";
    }

}
