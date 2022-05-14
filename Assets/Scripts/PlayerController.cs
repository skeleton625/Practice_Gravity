using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera Rotation Setting"), Space(10)]
    [SerializeField] private Transform DownCenterTransform = null;
    [SerializeField] private Transform UpCenterTransform = null;
    [SerializeField] private float RotateSpeed = 10f;
    [SerializeField] private float CameraRadius = 10f;

    private Rigidbody CameraRigidbody = null;

    private void Start()
    {
        CameraRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var DownPosition = DownCenterTransform.position;
        DownPosition.y = CameraRigidbody.position.y;
        var gravityVector = (CameraRigidbody.position - DownPosition).normalized;

        CameraRigidbody.MovePosition(gravityVector * CameraRadius + UpCenterTransform.position);

        CameraRigidbody.velocity = transform.right * RotateSpeed * Time.deltaTime;

        var rotation = Quaternion.FromToRotation(DownCenterTransform.up, gravityVector) * CameraRigidbody.rotation;
        CameraRigidbody.MoveRotation(Quaternion.Slerp(CameraRigidbody.rotation, rotation, RotateSpeed * Time.deltaTime));

        transform.LookAt(DownCenterTransform);
    }
}
