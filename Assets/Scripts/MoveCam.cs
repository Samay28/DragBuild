using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform CamPos;
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = CamPos.position;
        transform.rotation = CamPos.rotation;
    }
}
