using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
  [SerializeField] private Slider _slider;
  
  public void Init(int maxValue)
  {
    _slider.maxValue = maxValue;
    _slider.value = maxValue;
  }

  public void UpdateValue(int value)
  {
    _slider.value = value;
  }
} 
