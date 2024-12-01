using UnityEngine;
using System.Collections;

public class AsteroidTest : MonoBehaviour
{
    [SerializeField] private float _tumble;

    private void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * _tumble;
    }
}