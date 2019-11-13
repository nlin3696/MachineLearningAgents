using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform observable;
    public float aheadSpeed;
    public float followDamping;
    public float cameraHeight;

    Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = observable.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (observable == null) return;

        Vector3 targetPosition = observable.position + Vector3.up * cameraHeight + _rigidbody.velocity * aheadSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDamping * Time.deltaTime);
    }
}
