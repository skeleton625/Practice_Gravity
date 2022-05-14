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

            // Plane이 받는 Planet의 중력 Vector
            var gravityVector = (body.position - transform.position).normalized;
            body.MovePosition(gravityVector * (planetRadius + planeHeightFromGround[i]));

            // Plane의 Forward 방향으로 Velocity (힘)을 적용
            body.velocity = body.transform.forward * planeMoveSpeed[i] * Time.deltaTime;

            // Planet의 표면을 이동하도록 Plane의 Forward 방향을 회전
            var planeRotation = Quaternion.FromToRotation(body.transform.up, gravityVector) * body.rotation;
            body.MoveRotation(Quaternion.Slerp(body.rotation, planeRotation, speed * Time.deltaTime));
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
