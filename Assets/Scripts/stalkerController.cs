using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class stalkerController : MonoBehaviour
{
    public GameObject target;
    public float stalkerSpeed = 1f;
    public NavMeshAgent navigator;
    public SpriteRenderer sprite;
    public GameObject spawnpoints;
    public GameObject shadowEffect;
    public float maxDistance = 200f;
    public float minSpawnDistance = 20f;

    private Transform targetTransform;
    private float distanceFromTarget;
    private Transform spawnpointsTransform;
    private Image shadowEffectImage;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = target.GetComponent<Transform>();

        spawnpointsTransform = spawnpoints.GetComponent<Transform>();

        shadowEffectImage = shadowEffect.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "Stalker")
        {
            distanceFromTarget = Vector3.Distance(targetTransform.position, transform.position);

            if (distanceFromTarget > maxDistance)
            {
                Vector3 newSpawnPosition = spawnpointsTransform.GetChild(0).transform.position;
                float newSpawnPositionDistance = Vector3.Distance(spawnpointsTransform.GetChild(0).transform.position, targetTransform.position);

                for (int i = 0; i < spawnpointsTransform.childCount; i++)
                {
                    float spawnPositionDistance = Vector3.Distance(spawnpointsTransform.GetChild(i).transform.position, targetTransform.position);
                    if (spawnPositionDistance < newSpawnPositionDistance && spawnPositionDistance >= minSpawnDistance)
                    {
                        newSpawnPositionDistance = spawnPositionDistance;
                        newSpawnPosition = spawnpointsTransform.GetChild(i).transform.position;
                    }
                }

                transform.position = new Vector3(newSpawnPosition.x, transform.position.y, newSpawnPosition.z);
            }

            byte opacity = (byte)(Mathf.Clamp(255 - (distanceFromTarget - 15) * 10, 0, 255));
            shadowEffectImage.color = new Color32(0, 0, 0, opacity);
        }
        

        navigator.SetDestination(targetTransform.position);
        transform.localRotation = Quaternion.Euler(new Vector3(90, transform.localRotation.y, transform.localRotation.z));

        Vector3 normalizedMovement = navigator.desiredVelocity.normalized;
        Vector3 rightVector = Vector3.Project(normalizedMovement, transform.right);

        if (rightVector.x < 0) {
            sprite.flipX = true;
        }
        else if(rightVector.x > 0)
        {
            sprite.flipX = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Layer")
        {
            sprite.sortingOrder = other.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        }
    }
}
