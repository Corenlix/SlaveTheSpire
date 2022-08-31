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
            _entity.EntityUpdated += OnEntityUpdate;
        }

        private void OnDisable()
        {
            _entity.EntityUpdated -= OnEntityUpdate;
        }

        private void OnEntityUpdate(Entity entity)
        {
            _nameText.text = _entity.Name;
            _healthText.text = $"{entity.Health}/{entity.MaxHealth}";
            _shieldText.text = $"{entity.Shield}";
        }
    }
}