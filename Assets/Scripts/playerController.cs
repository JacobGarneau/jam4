using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    private Vector3 playerVelocity;
    private Collider nearbyTask = null;
    private bool doingTask = false;

    public CharacterController characterController;
    public GameObject taskPrompt;
    public float characterSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!doingTask)
        {
            moveCharacter();
        }

        if (Input.GetKeyUp("e") && nearbyTask != null && !doingTask)
        {
            doingTask = true;
            nearbyTask.GetComponent<taskStation>().taskUI.SetActive(true);
        } else if (Input.GetKeyUp("e") && nearbyTask != null && doingTask)
        {
            doingTask = false;
            nearbyTask.GetComponent<taskStation>().taskUI.SetActive(false);
        }
    }

    void moveCharacter()
    {
        if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * characterSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");

        if (collision.gameObject.tag == "Stalker")
        {
            Debug.Log("stalker");
            SceneManager.LoadScene("LoseScene");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        taskPrompt.SetActive(true);
        nearbyTask = other;
    }

    void OnTriggerExit(Collider other)
    {
        taskPrompt.SetActive(false);
        nearbyTask = null;
    }
}
