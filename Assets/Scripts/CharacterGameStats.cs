using System;

public class CharacterGameStats
{
    public enum StatType { HP, Damage, AttackSpeed, CritChance, MoveSpeed }

    public Stat<int> Hp = new Stat<int>(10, 0, false);
    public Stat<int> Damage = new Stat<int>(10, 0, false);
    public Stat<float> AttackSpeed = new Stat<float>(1, 0, true);
    public Stat<float> CritChance = new Stat<float>(1, 0, false);
    public Stat<float> MoveSpeed = new Stat<float>(3, 0, true);

}

public class Stat<T> where T : struct, IComparable, IConvertible, IFormattable
{
    public T baseValue;
    public T bonusValue;
    public bool isPercentValue;

    public Stat(T baseValue, T bonusValue, bool isPercentValue)
    {
        this.baseValue = baseValue;
        this.bonusValue = bonusValue;
        this.isPercentValue = isPercentValue;
    }

    public T GetCalculatedValue()
    {
        if (isPercentValue)
        {
            if (typeof(T) == typeof(float) || typeof(T) == typeof(double))
            {
                float baseVal = Convert.ToSingle(baseValue);
                float bonusVal = Convert.ToSingle(bonusValue);
                return (T)(object)(baseVal * (1f + bonusVal / 100f));  
            }
        }
        else
        {
            if (typeof(T) == typeof(int))
            {
                return (T)(object)(Convert.ToInt32(baseValue) + Convert.ToInt32(bonusValue)); // cast to int
            }

            if (typeof(T) == typeof(float))
            {
                return (T)(object)(Convert.ToSingle(baseValue) + Convert.ToSingle(bonusValue)); // cast to float
            }
        }

        throw new InvalidOperationException($"Unsupported type {typeof(T)} for calculation.");
    }
}
