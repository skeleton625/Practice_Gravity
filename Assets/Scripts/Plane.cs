using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Plane : MonoBehaviour
{
    [Header("Graivity Center Transform"), Space(10)]
    [SerializeField] private Planet TargetPlanet = null;
    [SerializeField] private float Movespeed = 0f;
    [SerializeField] private float HeightFromGround = 0f;

    private void Start()
    {
        TargetPlanet.EnrollThePlane(GetComponent<Rigidbody>(), Movespeed, HeightFromGround);
    }
}
