using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    #region Planet Gravity Setting SerializeField
    [Header("Planet Gravity Force"), Space(10)]
    [SerializeField] private float GravityForce = 0f;

    private float planetRadius = 0f;

    private List<float> planeMoveSpeed = null;
    private List<float> planeHeightFromGround = null;
    private List<Rigidbody> enrolledPlanes = null;
    #endregion

    #region Planet Initialize Functions
    private void Awake()
    {
        planeMoveSpeed = new List<float>();
        planeHeightFromGround = new List<float>();

        enrolledPlanes = new List<Rigidbody>();
        planetRadius = transform.localScale.x / 2;
    }
    #endregion

    #region Planet Gravity and Move Update Functions
    private void FixedUpdate()
    {
        for (int i = 0; i < enrolledPlanes.Count; ++i)
        {
            var body = enrolledPlanes[i];
            var speed = planeMoveSpeed[i];

            // Plane�� �޴� Planet�� �߷� Vector
            var gravityVector = (body.position - transform.position).normalized;
            body.MovePosition(gravityVector * (planetRadius + planeHeightFromGround[i]) + transform.position);

            // Plane�� Forward �������� Velocity (��)�� ����
            body.velocity = body.transform.forward * planeMoveSpeed[i] * Time.deltaTime;

            // Planet�� ǥ���� �̵��ϵ��� Plane�� Forward ������ ȸ��
            var planeRotation = Quaternion.FromToRotation(body.transform.up, gravityVector) * body.rotation;
            body.MoveRotation(planeRotation);
        }
    }
    #endregion

    #region Planet Enroll Plane on Planet Functions
    public void EnrollThePlane(Rigidbody planeRigidbody, float speed, float height)
    {
        planeMoveSpeed.Add(speed);
        planeHeightFromGround.Add(height);

        enrolledPlanes.Add(planeRigidbody);
    }
    #endregion
}
