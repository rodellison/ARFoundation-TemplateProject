using System;
using System.Collections;
using ARKitHelpers;
using Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    public GameEvent EnableControls;
    public GameEvent DisableControls;
    
    [SerializeField] private ARSession myARSession;

    public Text LogText;

    public void SetLogText(string textToDisplay)
    {
        LogText.text = textToDisplay;
    }
    
    public void CoachingInProgress()
    {
        DisableControls.Raise();
        myARSession.Reset();

#if UNITY_IOS
        myARSession.GetComponent<ARKitCoachingOverlay>().ActivateCoaching(true);
#endif
    }

    public void CoachingCompleted()
    {
        StartCoroutine(EnablePlacementIndicator());
#if UNITY_IOS
        myARSession.GetComponent<ARKitCoachingOverlay>().DisableCoaching(true);
#endif
    }

    IEnumerator EnablePlacementIndicator()
    {
        yield return new WaitForSeconds(0.5f);
        EnableControls.Raise();

    }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // If a second version is created, delete it immediately
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        // Make the singleton persist between scenes
        DontDestroyOnLoad(gameObject);

        Input.backButtonLeavesApp = true;
        Application.targetFrameRate = 60;

    }

    // Update is called once per frame
    void Update()
    {
        //Use the Back button to exit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
            ShutDownAndroid();
#elif UNITY_IOS
            Application.Quit();
#endif
        }
    }

    void ShutDownAndroid()
    {
        Application.Quit();
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }
}