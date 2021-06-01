using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    float shakeDuration;               // How long the camera should shake, in seconds
    float shakeMagnitude;             // How strong the camera shake should be
    float reductionSpeed;            // How long it takes for the camera to stop shaking, in seconds
    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        // Camera's initial position
        initialPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Determines if the camera should shake
        if(shakeDuration > 0)
        {
            // Shakes camera & reduces time left on shake duration
            transform.position = initialPos + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * reductionSpeed;
            if (GetComponent<CameraFollowPlayer>() != null)
                GetComponent<CameraFollowPlayer>().Following = false;
        }
        else
        {
            // Stops the camera from shaking & resets its position
            shakeDuration = 0;
            transform.position = initialPos;
            if (GetComponent<CameraFollowPlayer>() != null)
                GetComponent<CameraFollowPlayer>().Following = true;
        }
    }

    // Shakes the camera
    public void Shake(float duration, float magnitude, float reduction)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        reductionSpeed = reduction;
        initialPos = transform.position;
    }
}
