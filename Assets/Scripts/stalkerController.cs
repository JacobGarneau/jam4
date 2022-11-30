using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stalkerController : MonoBehaviour
{
    public GameObject target;
    public float stalkerSpeed = 1f;
    public NavMeshAgent navigator;
    public SpriteRenderer sprite;

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
        transform.localRotation = Quaternion.Euler(new Vector3(90, transform.localRotation.y, transform.localRotation.z));

        Debug.Log(transform.forward);

    }
}
