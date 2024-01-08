namespace tdd_dojo_parking_service;

[TestFixture]
public class ParkingServiceTests
{
    private ParkingService _parkingService;

    [SetUp]
    public void SetUp()
    {
        _parkingService = new ParkingService();
    }

    [Test]
    public void should_be_free()
    {
        var arriveTime = new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc);
        var leaveTime = new DateTime(2024, 1, 9, 0, 15, 0, 0, DateTimeKind.Utc);
        
        var fee = _parkingService.CalculateFee(arriveTime, leaveTime);
        Assert.AreEqual(0, fee);
    }

    [Test]
    public void should_be_30()
    {
        var arriveTime = new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc);
        var leaveTime = new DateTime(2024, 1, 9, 0, 30, 0, 0, DateTimeKind.Utc);
        
        var fee = _parkingService.CalculateFee(arriveTime, leaveTime);
        Assert.AreEqual(30, fee);
    }
}

public class ParkingService
{
    public decimal CalculateFee(DateTime arriveTime, DateTime leaveTime)
    {
        if (leaveTime - arriveTime >= TimeSpan.FromMinutes(30))
        {
            return 30m;
        }

        return 0m;
    }
}