
class MoneySaveLoader
{
    public void SetupData(Money data, Money service)
    {
        service.SoftMoney = data.SoftMoney;
        service.HardMoney = data.HardMoney;

    }
}
