using Entities;
using UnityEngine;
using Utilities;

namespace Cards.TargetSelectors
{
    public class NoTargetSelector : CardTargetSelector
    {
        [SerializeField] private float _smoothless = 60;
        private Camera _camera;
        
        protected override void OnStartSelecting()
        {
            _camera = Camera.main;
        }
        
        protected override void OnSelectingUpdate()
        {
            SelectedCard.transform.position =
                Vector3.Lerp(SelectedCard.transform.position.WithZ(0), _camera.ScreenToWorldPoint(Input.mousePosition).WithZ(0), Time.deltaTime * _smoothless);
            if (Input.GetMouseButtonDown(0))
            {
                SelectTargets(null);
            }
        }

        protected override void OnFinishSelecting()
        {
        }
    }
}