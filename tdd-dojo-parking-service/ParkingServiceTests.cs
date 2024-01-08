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

        var fee = _parkingService.CalculateFee(arriveTime, leaveTime, false);
        Assert.AreEqual(0, fee);
    }

    [Test]
    public void should_be_30()
    {
        var arriveTime = new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc);
        var leaveTime = new DateTime(2024, 1, 9, 0, 30, 0, 0, DateTimeKind.Utc);

        var fee = _parkingService.CalculateFee(arriveTime, leaveTime, false);
        Assert.AreEqual(30, fee);
    }

    [Test]
    public void should_be_60()
    {
        var arriveTime = new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc);
        var leaveTime = new DateTime(2024, 1, 9, 1, 0, 0, 0, DateTimeKind.Utc);

        var fee = _parkingService.CalculateFee(arriveTime, leaveTime, false);
        Assert.AreEqual(60, fee);
    }

    [Test]
    public void should_be_max_150()
    {
        var arriveTime = new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc);
        var leaveTime = new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc);

        var fee = _parkingService.CalculateFee(arriveTime, leaveTime, false);
        Assert.AreEqual(150, fee);
    }

    [Test]
    public void should_be_50_when_on_holiday()
    {
        var arriveTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        var leaveTime = new DateTime(2024, 1, 1, 0, 15, 0, 0, DateTimeKind.Utc);

        var fee = _parkingService.CalculateFee(arriveTime, leaveTime, true);
        Assert.AreEqual(0, fee);
    }

    [Test]
    public void should_be_100_when_on_holiday()
    {
        var arriveTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        var leaveTime = new DateTime(2024, 1, 1, 0, 30, 0, 0, DateTimeKind.Utc);

        var fee = _parkingService.CalculateFee(arriveTime, leaveTime, true);
        Assert.AreEqual(50, fee);
    }

    [Test]
    public void should_be_2400_when_on_holiday()
    {
        var arriveTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        var leaveTime = new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc);

        var fee = _parkingService.CalculateFee(arriveTime, leaveTime, true);
        Assert.AreEqual(2400, fee); 
    }
}

public class ParkingService
{
    public decimal CalculateFee(DateTime arriveTime, DateTime leaveTime, bool isHoliday)
    {
        var spans = new decimal(Math.Floor((leaveTime - arriveTime).TotalMinutes/30));

        return isHoliday
            ? spans * 50
            : decimal.Min(spans * 30, 150m);
    }
}