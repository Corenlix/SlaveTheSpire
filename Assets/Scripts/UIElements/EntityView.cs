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
            _entity.EntityHealed += OnEntityHealed;
            _entity.EntityDamaged += OnEntityDamaged;
            _entity.EntityGotArmor += OnEntityGotArmor;
        }

        private void OnDisable()
        {
            _entity.EntityInited -= OnEntityInited;
            _entity.EntityHealed -= OnEntityHealed;
            _entity.EntityDamaged -= OnEntityDamaged;
            _entity.EntityGotArmor -= OnEntityGotArmor;
        }

        private void OnEntityInited(Entity entity)
        {
            _nameText.text = _entity.Name;
            UpdateHealth();
            UpdateArmor();
        }

        private void UpdateHealth()
        {
            _healthText.text = $"{_entity.Health}/{_entity.MaxHealth}";
        }

        private void UpdateArmor()
        {
            _shieldText.text = $"{_entity.Armor}";
        }

        private void OnEntityHealed(Entity entity, int amount)
        {
            UpdateHealth();
        }

        private void OnEntityDamaged(Entity arg1, int arg2)
        {
            UpdateArmor();
            UpdateHealth();
        }

        private void OnEntityGotArmor(Entity obj)
        {
            UpdateArmor();
        }
    }
}