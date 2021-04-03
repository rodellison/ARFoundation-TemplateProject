using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleRotationCanvasControls : MonoBehaviour
{

    [SerializeField] private Slider ScaleSlider;
    [SerializeField] private Slider RotationSlider;   
    // Start is called before the first frame update
    public void Reset()
    {
        ScaleSlider.value = 0.5f;
        RotationSlider.value = 0f;
    }

}
