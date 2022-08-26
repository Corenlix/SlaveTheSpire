using TMPro;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    private Health _entityHealth;
    [SerializeField] private Bar _bar;
    [SerializeField] private TextMeshProUGUI _healthLabelInfo;

    public void Init(Health health)
    {
        _entityHealth = health;
        _bar.Init(_entityHealth.MaxHealth);
        _entityHealth.OnHealthChanged += UpdateView;
    }

    private void OnDestroy()
    {
        _entityHealth.OnHealthChanged -= UpdateView;
    }

    private void UpdateView(int value)
    {
        _healthLabelInfo.text = $"{value}/{_entityHealth.MaxHealth}";
        _bar.UpdateValue(value);
    }
}
