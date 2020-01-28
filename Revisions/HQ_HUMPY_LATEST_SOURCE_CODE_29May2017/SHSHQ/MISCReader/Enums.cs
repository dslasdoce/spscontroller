using System;
using System.Collections.Generic;
using System.Text;

namespace MISCReaderTCPIP
{
    public enum JournalType
    {
        /*-----------Prescription Action Journal Type--------*/
        PRESCRIPTION_NORMAL,

        PRESCRIPTION_INSERT,

        PRESCRIPTION_ERROR,

        /*-----------Dispense Action Journal Type--------*/
        DISPENSE_DETECTED,

        DISPENSE_STATUS_CHANGED,

        DISPENSE_ERROR,

        /*-----------Collect Action Journal Type--------*/
        COLLECT_DETECTED,

        COLLECT_STATUS_CHANGED,

        COLLECT_ERROR
    }


    public enum MedicineStatus
    {
        /// <summary>
        /// Default as Null or Blank when record inserted into the database
        /// </summary>
        NULL,

        /// <summary>
        /// When it scanned, the status changed to DISPENSED.
        /// </summary>
        DISPENSED,

        /// <summary>
        /// When it collected by the patient in the end, the status changed to COLLECTED.
        /// </summary>
        COLLECTED
    }

    /// <summary>
    /// The type of commands that you can sent to the server.(Note : These are just some comman types.You should do the desired actions when a command received to the client yourself.)
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// Reader-Defined Command
        /// The host sends this command to get the reader's information including readers' address, firmware version,
        /// reader type, supported protocol and inventoryscantime value(default is 0x1e for 3s)
        /// </summary>
        GetReaderInfor ,
        /// <summary>
        ///  Reader-Defined Command
        /// The host sends this command to turn off the RF output of the reader
        /// </summary>
        CloseRF ,
        /// <summary>
        ///  Reader-Defined Command
        /// The host sends this command to turn on the RF output of the reader and establish the 
        /// inductive field. The RF is open when the reader is powered on.
        /// </summary>
        OpenRF ,
        /// <summary>
        ///  Reader-Defined Command
        /// The host sends this command to change the address of the reader.
        /// </summary>
        WriteCom_adr ,
        /// <summary>
        ///  Reader-Defined Command
        /// The host sends this command to change the value of InventoryScanTime of the reader.
        /// The value is stored in the reader’s inner EEPROM and is nonvolatile after reader powered off.. 
        /// The default value is 0x1E (corresponding to 30*100ms=3s). 
        /// The value range is 0x03~0xFF(corresponding to 3*100ms~255*100ms). 
        /// When the host tries to set value 0x00~0x02 to InventoryScanTime, the reader will set it to 0x03 automatically. 
        /// In various environments, the actual inventory scan time may be 0~75ms longer than the InventoryScanTime defined.
        /// </summary>
        WriteInventoryScanTime ,
        /// <summary>
        /// Reader-Defined Command
        ///Change reader to ISO14443A model. 
        ///Before executing ISO14443A command, the reader should be changed to ISO14443A model. 
        ///Otherwise, the reader returns a status value 0x1F (protocol error).
        /// </summary>
        ChangeToISO14443A ,
        /// <summary>
        ///  Reader-Defined Command
        /// Change reader to ISO15693 model. When powered up, the reader enter this model as default.
        /// </summary>
        ChangeToISO15693,
        /// <summary>
        ///  Reader-Defined Command
        ///  The host can set LED’s action mode such as on/off duration and flash times.
        /// </summary>
        SetLED ,
        /// <summary>
        ///  Reader-Defined Command
        ///  The host can set Beep’s action mode such as on/off duration and beeping times.
        /// </summary>
        SetBeep ,
        /// <summary>
        ///  Reader-Defined Command
        ///  Change reader to ISO14443B model, before execute ISO14443B command, 
        ///  the reader should be changed to ISO14443B model. 
        ///  Otherwise, the reader returns a status value 0x1F (protocol error).
        /// </summary>
        ChangeToISO14443B,
        /// <summary>
        ///  Reader-Defined Command
        /// </summary>
        Message ,
        /// <summary>
        /// This command will sent to all clients when an specific client is had been logged in to the server.The metadata of this command is in this format : "ClientIP:ClientNetworkName"
        /// </summary>
        ClientLoginInform ,
        /// <summary>
        /// This command will sent to all clients when an specific client is had been logged off from the server.You can get the disconnected client information from SenderIP and SenderName properties of command event args.
        /// </summary>
        ClientLogOffInform ,
        /// <summary>
        /// To ask from the server pass the name that you want check it's existance as meta data of command.The server will replay to you a command with the same type and MetaData of 'True' or 'False' that specifies the Network name is exists on the server or not.
        /// </summary>
        IsNameExists,
        /// <summary>
        /// To get a list of current connected clients to the server,Send this type of command to it.The server will replay to you one same command for each client with the metadata in this format : "ClientIP:ClientNetworkName".
        /// </summary>
        SendClientList,
        /// <summary>
        /// This is a free command that you can sent to the server.
        /// </summary>
        FreeCommand,

        /// <summary>
        /// ISO15693 Command state 0x00
        /// All Ready state tags will respond. Only one tag's UID will be returned and that tag will be turned into Quiet state.
        /// </summary>
        InventoryWithoutAFI
    }
}
