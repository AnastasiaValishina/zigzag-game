using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float smoothing = 0.5f;

    Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 targetPosition = player.transform.TransformPoint(new Vector3 (0, 0, 0));
        targetPosition = new Vector3(targetPosition.x, 0, targetPosition.y);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing * Time.deltaTime);
    }
}
