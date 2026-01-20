namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerA;


public static class ManufacturerAConstants
{
    public const int DeviceId = 0;
    public const int EventCode = 1;
    public const int Latitude = 4;
    public const int Longitude = 5;
    public const int Timestamp = 6;
    public const int Speed = 7;
    public const int Direction = 8;
    public const int Idletime = 9;
    public const int MaxSpeed = 10;

    public const string SpeedUnits = "m/s";
    
};


public enum EventCodeType
{
    Location = 1,
    StartSpeed = 2,
    EndSpeed = 3,
    IdleStart = 4,
    IdleEnd = 5
}