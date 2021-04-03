#if UNITY_IOS

using UnityEngine;
using UnityEngine.XR.ARKit;

namespace ARKitHelpers
{
    public class CustomSessionDelegate : DefaultARKitSessionDelegate
    {
        protected override void OnCoachingOverlayViewWillActivate(ARKitSessionSubsystem sessionSubsystem)
        {
            Debug.Log("OnCoachingOverlayViewWillActivate");
            // Logger.Log(nameof(OnCoachingOverlayViewWillActivate));
            GameManager.Instance.CoachingInProgress();
        }

        protected override void OnCoachingOverlayViewDidDeactivate(ARKitSessionSubsystem sessionSubsystem)
        {
            Debug.Log("OnCoachingOverlayViewDidDeactivate");
            // Logger.Log(nameof(OnCoachingOverlayViewDidDeactivate));
            GameManager.Instance.CoachingCompleted();
        }
    }
}
#endif