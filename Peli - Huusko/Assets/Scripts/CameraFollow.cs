using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 minValues, maxValues;

    [SerializeField] private Transform target;


    void Update()
    {
        Vector3 targetPosition = target.position + offset;


        //Tässä haetaan kameran rajat min/max x,y, ja z suunnassa.

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
             Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
               Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z));

        transform.position = Vector3.SmoothDamp(transform.position, boundPosition, ref velocity, smoothTime);
    }

}
