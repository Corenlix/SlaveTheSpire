using Entities;
using TMPro;
using UnityEngine;

namespace UIElements
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _shieldText;
        
        private void OnEnable()
        {
            _entity.EntityInited += OnEntityInited;
            _entity.EntityHealth.Changed += OnHealthChanged;
        }

        private void OnDisable()
        {
            _entity.EntityInited -= OnEntityInited;
            _entity.EntityHealth.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged(EntityHealth health)
        {
            UpdateArmor();
            UpdateHealth();
        }

        private void OnEntityInited(Entity entity)
        {
            _nameText.text = _entity.Name;
            UpdateHealth();
            UpdateArmor();
        }

        private void UpdateHealth()
        {
            _healthText.text = $"{_entity.EntityHealth.Health}/{_entity.EntityHealth.MaxHealth}";
        }

        private void UpdateArmor()
        {
            _shieldText.text = $"{_entity.EntityHealth.Armor}";
        }
    }
}