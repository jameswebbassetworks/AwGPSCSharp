namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerB;


public static class ManufacturerBConstants
{
    public const int EventCode = 0;
    public const int DeviceId = 1;
    public const int LatitudeLongitude = 2;
    public const int Timestamp = 3;
    public const int Speed = 4;
    public const int Direction = 5;
    public const int Idletime = 12;

    public const string SpeedUnits = "m/s";
};


public enum EventCodeType
{
    Location = 50,
    StartSpeed = 62,
    EndSpeed = 63,
    IdleStart = 97,
    IdleEnd = 98
}