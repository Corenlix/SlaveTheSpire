using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace Card.TargetSelectors
{
    public class Attack : CardTargetSelector
    {
        private bool _isActive = false;
        private Camera _camera;
        private Entity _selectedEntity;
        
        public override void StartSelecting()
        {
            SetActive(true);
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!_isActive)
                return;
            
            var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null  && hit.transform.TryGetComponent<Entity>(out var entity))
            {
                if (_selectedEntity != entity)
                {
                    _selectedEntity = entity;
                    entity.transform.DOScale(1.15f, 0.25f);
                }
                if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse))
                {
                    List<Entity> targets = new List<Entity> {entity};
                    SelectTargets(targets);
                }
            }
            else if (_selectedEntity != null)
            {
                _selectedEntity.transform.DOScale(1, 0.25f);
                _selectedEntity = null;
            }
        }

        public override void FinishSelecting()
        {
            if (_selectedEntity != null)
            {
                _selectedEntity.transform.DOScale(1, 0.5f);
                _selectedEntity = null;
            }
            SetActive(false);
        }

        private void SetActive(bool active)
        {
            _isActive = active;
        }
    }
}