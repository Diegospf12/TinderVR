using UnityEngine;
using Oculus;

public class OVRTestScript : MonoBehaviour
{
    public OVRCameraRig cameraRig;

    void Start()
    {
        if (cameraRig != null)
        {
            Debug.Log("OVRCameraRig asignado desde el inspector!");
        }
        else
        {
            Debug.LogError("Asigna el OVRCameraRig en el inspector!");
        }
    }

    void Update()
    {
        if (cameraRig != null)
        {
            var leftHandAnchor = cameraRig.transform.Find("TrackingSpace/LeftHandAnchor");
            var rightHandAnchor = cameraRig.transform.Find("TrackingSpace/RightHandAnchor");

            /*
            if (leftHandAnchor != null && rightHandAnchor != null)
            {
                Debug.Log("Anclas de las manos encontradas!");
            }
            */
        }
    }
}