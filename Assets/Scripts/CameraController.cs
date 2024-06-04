using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float normalSpeed = 10.0f;  // Normal camera movement speed
    public float slowedSpeed = 2.0f;  // Slowed down camera movement speed
    private bool isPlaneDetected = false;

    void Update()
    {
        float currentSpeed = isPlaneDetected ? slowedSpeed : normalSpeed;

        float moveHorizontal = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;

        transform.Translate(new Vector3(moveHorizontal, 0, moveVertical));
    }

    public void SetPlaneDetected(bool detected)
    {
        isPlaneDetected = detected;
    }
}
