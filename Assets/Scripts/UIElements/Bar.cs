using UnityEngine;
using UnityEngine.UI;

namespace UIElements
{
  public class Bar : MonoBehaviour
  {
    [SerializeField] private Slider _slider;

    public void UpdateValue(int value, int maxValue)
    {
      _slider.value = value;
      _slider.maxValue = maxValue;
    }
  }
} 
