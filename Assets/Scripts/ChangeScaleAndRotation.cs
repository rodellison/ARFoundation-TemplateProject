
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARSessionOrigin))]
public class ChangeScaleAndRotation : MonoBehaviour
{

    private ARSessionOrigin _sessionOrigin;
    private ARTapToPlaceObject myARTTPO;
    private Vector3 _newScale;
    private Quaternion _newRotation;
    private float _maxScale;

    private void Start()
    {
       _newScale = new Vector3();
       _newRotation = new Quaternion();
       _sessionOrigin = GetComponent<ARSessionOrigin>();
       myARTTPO = GetComponent<ARTapToPlaceObject>();
       _maxScale = GameObject.Find("SliderScale").GetComponent<Slider>().maxValue;

    }

    public void SetScale(float _scaleValue)
    {
         _newScale.x = _maxScale/_scaleValue;
         _newScale.y = _maxScale/_scaleValue;
         _newScale.z = _maxScale/_scaleValue;
         _sessionOrigin.transform.localScale = _newScale;

        if (myARTTPO.IsGOPlaced())
        {
            _sessionOrigin.MakeContentAppearAt(myARTTPO.GOTransform(), myARTTPO.GetPlacementPose().position);
        }
    }
    
    public void SetRotation(float _rotationValue)
    {
        _newRotation = Quaternion.AngleAxis(_rotationValue, Vector3.up);
        _sessionOrigin.transform.localRotation = _newRotation;

    }
}
