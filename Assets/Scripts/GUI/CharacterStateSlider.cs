using UnityEngine;
using UnityEngine.UI;

public class CharacterStateSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
   
    private float _maxValue;

    public void SetMaxValue(float maxValue)
    {
        _maxValue = maxValue;
        _slider.maxValue = _maxValue;
    }

    public void StartShow()
    {
        _slider.gameObject.SetActive(true);
    }
    
    public void Show(float value)
    {
        _slider.value = value;
    }

    public void StopShow()
    {
        _slider.gameObject.SetActive(false);
        _slider.value = _maxValue;
    }
}
