
using UnityEngine;

namespace UIElements
{
    public abstract class ViewObserver : MonoBehaviour
    {
        private BoundedValue _observableValue;
        
        public void Init(BoundedValue watchingValue)
        {
            if (_observableValue != null)
                _observableValue.ValueChanged -= OnValueUpdate;
        
            _observableValue = watchingValue;
            _observableValue.ValueChanged += OnValueUpdate;
            UpdateView(_observableValue.CurrentValue, _observableValue.MaxValue);
        }

        private void OnDestroy()
        {
            if(_observableValue != null)
                _observableValue.ValueChanged -= OnValueUpdate;
        }

        private void OnValueUpdate()
        {
            UpdateView(_observableValue.CurrentValue, _observableValue.MaxValue);
        }

        protected abstract void UpdateView(int currentValue, int maxValue);
    }
}