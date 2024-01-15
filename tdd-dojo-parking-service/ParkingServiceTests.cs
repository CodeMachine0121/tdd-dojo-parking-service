using System.Globalization;

namespace tdd_dojo_parking_service;

[TestFixture]
public class ParkingServiceTests
{
    private DateTime _arriveTime;
    private decimal _fee;
    private DateTime _leaveTime;
    private ParkingService _parkingService;

    [SetUp]
    public void SetUp()
    {
        _parkingService = new ParkingService();
    }

    [Test]
    public void should_be_free()
    {
        SetArriveTime("2024-01-09 00:00:00");
        SetLeaveTime("2024-01-09 00:01:00");
        WhenCalculate();
        FeeShouldBe(0);
    }

    [Test]
    public void should_be_30()
    {
        SetArriveTime("2024-01-09 00:00:00");
        SetLeaveTime("2024-01-09 00:30:00");
        WhenCalculate();
        FeeShouldBe(30);
    }

    [Test]
    public void should_be_60()
    {
        SetArriveTime("2024-01-09 00:00:00");
        SetLeaveTime("2024-01-09 01:00:00");
        WhenCalculate();
        FeeShouldBe(60);
    }

    [Test]
    public void should_be_max_150()
    {
        SetArriveTime("2024-01-09 00:00:00");
        SetLeaveTime("2024-01-10 00:00:00");
        WhenCalculate();
        FeeShouldBe(150);
    }

    private void FeeShouldBe(int expected)
    {
        Assert.AreEqual(expected, _fee);
    }

    private void WhenCalculate()
    {
        _fee = _parkingService.CalculateFee(_arriveTime, _leaveTime);
    }

    private void SetLeaveTime(string dateTime)
    {
        _leaveTime = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
    }

    private void SetArriveTime(string time)
    {
        _arriveTime = DateTime.ParseExact(time, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
    }
}

public class ParkingService
{
    public decimal CalculateFee(DateTime arriveTime, DateTime leaveTime)
    {
        var spans = new decimal(Math.Floor((leaveTime - arriveTime).TotalMinutes / 30));
        var fee = spans * 30;
        return fee > 150 ? 150 : fee;
    }
}