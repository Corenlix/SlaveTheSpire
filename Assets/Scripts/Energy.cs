using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy
{
    public event Action OnUpdateEnergy; 
    public int CurrentEnergy => _currentEnergy;
    public int MaxEnergy => _maxEnergy;
    
    private int _currentEnergy;
    private int _maxEnergy;
    
    public void SubtractEnergy(int value)
    {
        if(_currentEnergy <= 0) return;
        _currentEnergy -= value;
        
        OnUpdateEnergy?.Invoke();
    }

    public void AddEnergy(int value)
    {
        if(_currentEnergy > _maxEnergy) return;
        _currentEnergy += value;
        
        OnUpdateEnergy?.Invoke();
    }

    public Energy(int currentEnergy, int maxEnergy)
    {
        _currentEnergy = currentEnergy;
        _maxEnergy = maxEnergy;
    }
}
