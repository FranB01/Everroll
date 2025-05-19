using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;      // The ball's transform
    public Vector3 offset = new Vector3(0, 4, -5);  // Offset from the ball
    public float camTilt;
    /*
    public float smoothSpeed = 0.125f;  // Smooth following

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Look at the ball without rotating with its orientation
        transform.LookAt(target);
    }
    */

    void Start()
    {
        camTilt = Mathf.Atan2(Math.Abs(offset.y - 1f), Math.Abs(offset.z)) * Mathf.Rad2Deg; // get cam to point to player
    }

    void LateUpdate()
    {
        transform.position = (target.transform.position.z * Vector3.forward) + (target.transform.position.x * Vector3.right) + offset;
    }

}
