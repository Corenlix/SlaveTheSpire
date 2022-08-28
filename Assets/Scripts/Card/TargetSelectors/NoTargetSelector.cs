﻿using Entities;
using UnityEngine;

namespace Card.TargetSelectors
{
    public class NoTargetSelector : CardTargetSelector
    {
        [SerializeField] private float _smoothless = 60;
        
        protected override void OnStartSelecting()
        {
        }
        
        protected override void OnSelectingUpdate()
        {
            SelectedCardHolder.transform.position =
                Vector3.Slerp(SelectedCardHolder.transform.position, Input.mousePosition, Time.deltaTime * _smoothless);
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.transform.TryGetComponent<Player>(out var player))
                {
                    SelectTargets(null);
                }
            }
        }

        protected override void OnFinishSelecting()
        {
        }
    }
}