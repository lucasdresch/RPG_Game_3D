using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour{
    public Transform CameraTarget;
    public Vector3 Offset;
    public float ZoomSpeed = 4f;
    public float MinZoom = 4f;
    public float MaxZoom = 4f;

    public float Pitch = 4f;
    
    public float CurrentZoom = 4f;

    
    void Start(){
        CameraTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    void Update(){
        CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        CurrentZoom = Mathf.Clamp(CurrentZoom, MinZoom, MaxZoom);

    }
    void LateUpdate(){
        transform.position = CameraTarget.position - Offset * CurrentZoom;
        transform.LookAt(CameraTarget.position + Vector3.up * Pitch);
    }


}