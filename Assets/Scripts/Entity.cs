using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Entity : MonoBehaviour
{
    [SerializeField] private Health _health;
    
    public void DealDamage(int value)
    {
        _health.DealDamage(value);
    }
}