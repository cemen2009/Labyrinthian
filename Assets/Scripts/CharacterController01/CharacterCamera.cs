using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    [SerializeField] float horizontalSensetivity, verticalSensetivity;

    [SerializeField] Transform orientation;

    // input varuables
    float mouseHorizontal, mouseVertical;

    float cameraRotationX, cameraRotationY;

    public bool invertedRotation;

    private void Start()
    {
        // TODO: replace that to gameflow-chaning method
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // TODO: check gameflow state

        // get input data
        mouseHorizontal = Input.GetAxis("Mouse X") * horizontalSensetivity;
        mouseVertical = Input.GetAxis("Mouse Y") * verticalSensetivity;

        // change rotation of object according to axis of it's transform
        cameraRotationY += invertedRotation ? mouseHorizontal : -mouseHorizontal;
        cameraRotationX -= invertedRotation ? mouseVertical : -mouseVertical;
        
        // clamp X rotation so player can't make 360 vertical rotation
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90, 90);

        transform.rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0);
        orientation.rotation = Quaternion.Euler(cameraRotationX, 0, 0);
    }
}
