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
    
    [Test]
    public void should_be_60()
    {
        var arriveTime = new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc);
        var leaveTime = new DateTime(2024, 1, 9, 1, 0, 0, 0, DateTimeKind.Utc);
        
        var fee = _parkingService.CalculateFee(arriveTime, leaveTime);
        Assert.AreEqual(60, fee);
    }

    [Test]
    public void should_be_max_150()
    {
         var arriveTime = new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc);
        var leaveTime = new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc);
        
        var fee = _parkingService.CalculateFee(arriveTime, leaveTime);
        Assert.AreEqual(150, fee);
    }

}

public class ParkingService
{
    public decimal CalculateFee(DateTime arriveTime, DateTime leaveTime)
    {
        var timeSpan = leaveTime - arriveTime;
        var spans = Math.Floor(timeSpan.TotalMinutes/30);
        return decimal.Min(new decimal(spans* 30), 150m); ;
    }
}