using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private HealthView _healthView;
    private Health _health;

    private void Start()
    {
        _health = new Health(_maxHealth);
        _healthView.Init(_health);
    }

    public void TakeDamage(int value)
    {
        _health.TakeDamage(value);
    }
}