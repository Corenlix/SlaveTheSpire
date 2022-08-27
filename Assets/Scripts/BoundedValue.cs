using System;

public class BoundedValue
{
    public event Action ValueChanged; 
    public int CurrentValue { get; private set; }
    public int MaxValue { get; private set; }

    public void Subtract(int value)
    {
        if (CurrentValue < value)
            throw new ArgumentOutOfRangeException();
        
        CurrentValue -= value;
        ValueChanged?.Invoke();
    }

    public void Add(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException();
            
        CurrentValue = Math.Clamp(CurrentValue + value, 0, MaxValue);
        ValueChanged?.Invoke();
    }

    public BoundedValue(int maxValue) : this(maxValue, maxValue)
    {
    }
    
    public BoundedValue(int currentValue, int maxValue)
    {
        if(currentValue > maxValue || currentValue < 0)
            throw new ArgumentOutOfRangeException();
        
        CurrentValue = currentValue;
        MaxValue = maxValue;
    }
}
