using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.Buffs
{
    public class BuffView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _stepsText;
        private Buff _buff;
        
        public void Init(Sprite icon, Buff buff)
        {
            UnSubscribe();
            
            _icon.sprite = icon;
            UpdateSteps(buff);
            buff.StepRemainChanged += UpdateSteps;
        }

        public void UpdateSteps(Buff buff)
        {
            _stepsText.text = buff.StepsRemain.ToString();
        }

        private void OnDestroy()
        {
            UnSubscribe();
        }

        private void UnSubscribe()
        {
            if(_buff != null)
                _buff.StepRemainChanged -= UpdateSteps;
        }
    }
}