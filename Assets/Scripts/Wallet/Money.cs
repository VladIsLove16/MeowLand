using System;
[Serializable]
public class Money
{
    public Money()
    {
        SoftMoney = 0;
        HardMoney = 0;
    }
    public Money(int Soft,int Hard =0)
    {
        SoftMoney = Soft;
        HardMoney = Hard;
    }
    public int SoftMoney=0;
    public int HardMoney=0;
}
