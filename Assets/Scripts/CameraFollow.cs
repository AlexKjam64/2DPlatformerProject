using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(1f, 1.5f, -10f);
    private float smoothTime = 0.10f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
