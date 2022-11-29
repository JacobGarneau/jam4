using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stalkerController : MonoBehaviour
{
    public GameObject target;
    public float stalkerSpeed = 1f;
    public NavMeshAgent navigator;

    private Transform targetTransform;
    private Vector3 distanceFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = target.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        navigator.SetDestination(targetTransform.position);
    }
}
