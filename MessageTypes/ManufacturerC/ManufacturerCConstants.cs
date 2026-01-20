namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerC;


public class ManufacturerCConstants
{
    public const int VIN = 0;
    public const int DeviceId = 1;
    public const int Date = 2;
    public const int Time = 3;
    public const int Latitude = 10;
    public const int Longitude = 11;
    public const int EventCode = 12;
    public const int Speed = 25;
    public const int Direction = 26;
    public const int MaxSpeed = 27;
    public const int Idletime = 30;
    
    public const string VinLocationIdle = "19XFB2F52DE079283";
    public const string VinSpeeding = "1FT7W2BT2EEB76476";

    public const string SpeedUnits = "km/h";


    public enum EventCodeType
    {
        Location = 15,
        StartSpeed = 51,
        EndSpeed = 52,
        IdleStart = 26,
        IdleEnd = 28
    }
};