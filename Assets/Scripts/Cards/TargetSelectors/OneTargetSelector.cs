using System.Collections.Generic;
using BansheeGz.BGSpline.Curve;
using DG.Tweening;
using Entities;
using Entities.Enemies;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Cards.TargetSelectors
{
    public class OneTargetSelector : CardTargetSelector
    {
        [SerializeField] private SpriteRenderer _cursor;
        [SerializeField] private float _smoothless;
        [SerializeField] private BGCurve _bgCurve;
        [SerializeField] private Animator _animator;
        private Camera _camera;
        private Enemy _selectedEnemy;
        private static readonly int SelectedBool = Animator.StringToHash("Selected");

        protected override void OnStartSelecting()
        {
            _camera = Camera.main;
            _cursor.transform.position = _camera.ScreenToWorldPoint(Input.mousePosition).WithZ(0);
            _bgCurve.Points[0].PositionWorld = _cursor.transform.position;
            _bgCurve.Points[1].PositionWorld = (SelectedCard.transform.position).WithZ(0);
            _cursor.gameObject.SetActive(true);
            _bgCurve.gameObject.SetActive(true);
        }

        protected override void OnSelectingUpdate()
        {
            _cursor.transform.position =
                Vector3.Lerp(_cursor.transform.position.WithZ(0), _camera.ScreenToWorldPoint(Input.mousePosition).WithZ(0), Time.deltaTime * _smoothless);
            var direction = _bgCurve.GetComponent<LineRenderer>().GetPosition(1) - _bgCurve.GetComponent<LineRenderer>().GetPosition(0);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _cursor.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            _bgCurve.Points[0].PositionWorld = _cursor.transform.position;
            var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null  && hit.transform.TryGetComponent<Enemy>(out var enemy))
            {
                if (_selectedEnemy != enemy)
                {
                    if(_selectedEnemy)
                        _selectedEnemy.Animator.DeselectState();;
                    
                    _selectedEnemy = enemy;
                    enemy.Animator.SelectState();
                    
                    _animator.SetBool(SelectedBool, true);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    List<Entity> targets = new List<Entity> {enemy};
                    SelectTargets(targets);
                }
            }
            else if (_selectedEnemy != null)
            {
                _animator.SetBool(SelectedBool, false);
                _selectedEnemy.Animator.DeselectState();
                _selectedEnemy = null;
            }
        }

        protected override void OnFinishSelecting()
        {
            _animator.SetBool(SelectedBool, false);
            _cursor.gameObject.SetActive(false);
            _bgCurve.gameObject.SetActive(false);
            if (_selectedEnemy != null)
            {
                _selectedEnemy.Animator.DeselectState();
                _selectedEnemy = null;
            }
        }
    }
}