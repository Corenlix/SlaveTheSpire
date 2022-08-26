using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Card.TargetSelectors
{
    public class Attack : CardTargetSelector
    {
        [SerializeField] private Image _cursor;
        [SerializeField] private float _smoothless;
        [SerializeField] private Color _toColor;
        private Color _defaultColor;
        private Camera _camera;
        private Entity _selectedEntity;

        private void Awake()
        {
            _defaultColor = _cursor.color;
        }
        
        protected override void OnStartSelecting()
        {
            _camera = Camera.main;
            _cursor.gameObject.SetActive(true);
            _cursor.transform.position = Input.mousePosition;
        }

        protected override void OnSelectingUpdate()
        {
            _cursor.transform.position =
                Vector3.Slerp(_cursor.transform.position, Input.mousePosition, Time.deltaTime * _smoothless);
            var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null  && hit.transform.TryGetComponent<Entity>(out var entity))
            {
                if (_selectedEntity != entity)
                {
                    _selectedEntity = entity;
                    entity.transform.DOScale(1.15f, 0.1f);
                    _cursor.DOColor(_toColor, 0.25f);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    List<Entity> targets = new List<Entity> {entity};
                    SelectTargets(targets);
                }
            }
            else if (_selectedEntity != null)
            {
                _cursor.DOColor(_defaultColor, 0.1f);
                _selectedEntity.transform.DOScale(1, 0.25f);
                _selectedEntity = null;
            }
        }

        protected override void OnFinishSelecting()
        {
            _cursor.gameObject.SetActive(false);
            if (_selectedEntity != null)
            {
                _cursor.material.color = _defaultColor;
                _selectedEntity.transform.DOScale(1, 0.5f);
                _selectedEntity = null;
            }
        }
    }
}