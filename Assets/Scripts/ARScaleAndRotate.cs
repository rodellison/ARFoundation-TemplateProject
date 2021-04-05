using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARSessionOrigin))]
public class ARScaleAndRotate : MonoBehaviour
{
    ARSessionOrigin m_SessionOrigin;
    private ARPlaceAndMove myARTTPO;
    private Vector3 _newScale;
    private Quaternion _newRotation;
    private float _minScale;
    private float _maxScale;

    private ARRaycastManager _raycastManager;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private float initialDistance;
    private Vector3 initialScale;

    private void Start()
    {
        _newScale = new Vector3();
        _newRotation = new Quaternion();
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
        myARTTPO = GetComponent<ARPlaceAndMove>();
        _minScale = GameObject.Find("SliderScale").GetComponent<Slider>().minValue;
        _maxScale = GameObject.Find("SliderScale").GetComponent<Slider>().maxValue;
    }

    private void Update()
    {
        //IMPORTANT - Perform code below ONLY if TouchCount is 2, as if it's 1, then we're moving
        //which is handled in the ARPlaceAndMove script
        if (Input.touchCount == 2)
        {
            var touch0 = Input.GetTouch(0);
            var touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Ended || touch0.phase == TouchPhase.Canceled ||
                touch1.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Canceled)
                return;

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch0.position, touch1.position);
                initialScale = myARTTPO.GOTransform().localScale;

            }
            else
            {
                var currentDistance = Vector2.Distance(touch0.position, touch1.position);
                if (Mathf.Approximately(currentDistance, 0))
                    return;

                var factor = currentDistance / initialDistance;
                SetScale(factor * initialScale.x);
            }
        }
    }


    /// <summary>
    /// SetScale is a public function that handles setting the Session Origin scale and making the content appear correct
    /// at that scale. It can be called from a UI Slider, or is called as part of two finger pinch scaling from the Update method above
    /// </summary>
    /// <param name="_scaleValue"></param>
    public void SetScale(float _scaleValue)
    {
        var clampedScale = Mathf.Clamp(_maxScale / _scaleValue, _maxScale * _minScale, _maxScale / _minScale);
      //  GameManager.Instance.SetLogText(clampedScale.ToString());

        _newScale.x = clampedScale;
        _newScale.y = clampedScale;
        _newScale.z = clampedScale;
        m_SessionOrigin.transform.localScale = _newScale;


        if (myARTTPO.IsGOPlaced())
        {
            m_SessionOrigin.MakeContentAppearAt(myARTTPO.GOTransform(), myARTTPO.GetPlacementPose().position);
        }
    }

    public void SetRotation(float _rotationValue)
    {
        _newRotation = Quaternion.AngleAxis(_rotationValue, Vector3.up);
        m_SessionOrigin.transform.localRotation = _newRotation;
    }
}