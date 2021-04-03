#if UNITY_IOS

using UnityEngine;
using UnityEngine.XR.ARKit;

namespace ARKitHelpers
{
    public class CustomSessionDelegate : DefaultARKitSessionDelegate
    {
        protected override void OnConfigurationChanged(ARKitSessionSubsystem sessionSubsystem)
        {
            Debug.Log("OnConfigurationChanged");
        }

        protected override void OnSessionDidFailWithError(ARKitSessionSubsystem sessionSubsystem, NSError error)
        {
            Debug.Log("OnSessionDidFailWithError: " + error.AsARKitErrorCode());
        }

        protected override void OnCoachingOverlayViewWillActivate(ARKitSessionSubsystem sessionSubsystem)
        {
            Debug.Log("OnCoachingOverlayViewWillActivate");
            GameManager.Instance.CoachingInProgress();
        }

        protected override void OnCoachingOverlayViewDidDeactivate(ARKitSessionSubsystem sessionSubsystem)
        {
            Debug.Log("OnCoachingOverlayViewDidDeactivate");
            GameManager.Instance.CoachingCompleted();
        }
    }
}
#endif