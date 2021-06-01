using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    public GameObject playerCamera;
    public bool Following { get; set; }


    private void Start()
    {
        Following = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCamera != null && Following)
            transform.position = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y, -10);
    }
}
