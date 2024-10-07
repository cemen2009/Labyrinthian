using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class exist only because i don't want to make troubles for myself by putting camera into character object
public class CameraPositionSetter : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
