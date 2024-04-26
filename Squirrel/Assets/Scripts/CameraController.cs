using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;

    [SerializeField] private Transform target;

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.x = Mathf.Clamp(target.position.x, minPosition.x, maxPosition.x);
        targetPosition.y = Mathf.Clamp(target.position.y, minPosition.y, maxPosition.y);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime); 
    }
}
