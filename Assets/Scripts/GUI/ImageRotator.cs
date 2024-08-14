using UnityEngine;
using UnityEngine.UI;

public class ImageRotator : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    private float _rotateValue = -1;

    private void Update()
    {
        transform.Rotate(0, 0, _rotateValue);
    }
}
