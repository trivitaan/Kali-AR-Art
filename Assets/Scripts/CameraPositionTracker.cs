using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class CameraPositionTracker : MonoBehaviour
{
    public Text cameraPositionText;
    public Text cameraRotationText;

    private void Awake() {    
        Vector3 cameraPosition = Camera.main.transform.position;
        Quaternion cameraRotation = Camera.main.transform.rotation;
        // Update the UI Text components
        cameraPositionText.text = "Camera Position: " + cameraPosition.ToString("F2");
        cameraRotationText.text = "Camera Rotation: " + cameraRotation.eulerAngles.ToString("F2");
    }
    
    private void Update()
    {
       
    }
}
