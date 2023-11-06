using System;

public class HumanCounter
{
    public event Action<int> OnHumanCountChanged; 
    private int _humanCount;

    public void Add()
    {
        _humanCount++;
        OnHumanCountChanged?.Invoke(_humanCount);
    }

    public void Remove()
    {
        _humanCount--;
        OnHumanCountChanged?.Invoke(_humanCount);
    }
}