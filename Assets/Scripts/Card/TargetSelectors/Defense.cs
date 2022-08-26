using DG.Tweening;
using UnityEngine;

namespace Card.TargetSelectors
{
    public class Defense : CardTargetSelector
    {
        private bool _isActive;
        
        public override void StartSelecting()
        {
            SetActive(true);
        }

        private void Update()
        {
            if (!_isActive)
                return;
            
            transform.DOMove(Input.mousePosition, Time.deltaTime * 5f);
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.transform.TryGetComponent<DefenseTrigger>(out var trigger))
                {
                    SelectTargets(null);
                }
            }
        }

        public override void FinishSelecting()
        {
            SetActive(false);
        }

        private void SetActive(bool active)
        {
            _isActive = active;
        }
    }
}