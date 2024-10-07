using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    [SerializeField] float horizontalSensetivity, verticalSensetivity;

    [SerializeField] Transform orientation;

    // input varuables
    float mouseHorizontal, mouseVertical;

    Vector3 cameraRotation = Vector3.zero;
    //float cameraRotationX, cameraRotationY;

    public bool invertedVerticalRotation;

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
        mouseHorizontal = Input.GetAxis("Mouse X") * horizontalSensetivity * Time.deltaTime;
        mouseVertical = Input.GetAxis("Mouse Y") * verticalSensetivity * Time.deltaTime;

        // change rotation of object according to axis of it's transform
        cameraRotation.y +=  mouseHorizontal;
        cameraRotation.x -= invertedVerticalRotation ? -mouseVertical : mouseVertical;
        
        // clamp X rotation so player can't make 360 vertical rotation
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -90, 90);

        transform.rotation = Quaternion.Euler(cameraRotation);
        orientation.rotation = Quaternion.Euler(cameraRotation.x, 0, 0);
    }
}
