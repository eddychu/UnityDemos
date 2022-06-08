using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ArcballController : MonoBehaviour
{

    bool ballEnabled = false;

    Vector2 lastMousePosition;


    // these fields are just for viewing the realtime rotation angle of the camera in the inspector
    [SerializeField, Range(-90f, 90f)]
	public float rotationX = 0.0f;

    [SerializeField, Range(0f, 360f)]
    public float rotationY = 0.0f;

    Vector3 target = new Vector3(0.0f, 0.0f, 0.0f);

    float radius = 5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // last is a global vec3 variable
            lastMousePosition = Input.mousePosition;
            
            // This is another global variable
            ballEnabled = true;
        }
        if (Input.GetMouseButtonUp (0)) {
            ballEnabled = false;
        }
        if (ballEnabled) {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 delta = currentMousePosition - lastMousePosition;
            lastMousePosition = currentMousePosition;
            rotationX += delta.y * 0.1f;
            rotationY += delta.x * 0.1f;
            // clamp the rotation to prevent the camera from flipping
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);
            // mod the rotation to prevent the camera from rotating more than 360 degrees
            rotationY = rotationY % 360f;
        }
    }

    void LateUpdate() {
        Vector2 orbitAngles = new Vector2(rotationX, rotationY);
        Quaternion lookRotation = Quaternion.Euler(orbitAngles);
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = target - lookDirection * radius;
        this.transform.SetPositionAndRotation(lookPosition, lookRotation);
    }
}
