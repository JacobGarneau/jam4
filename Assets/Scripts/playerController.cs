using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class playerController : MonoBehaviour
{
    private Vector3 playerVelocity;

    public Collider nearbyTask = null;
    public bool doingTask = false;

    public CharacterController characterController;
    public SpriteRenderer sprite;
    public Animator animator;
    public GameObject taskPrompt;
    public GameObject promptBackground;
    public float characterSpeed = 1f;
    public GameObject scoreDisplay;
    public float playerScore = 0;
    public float scoreObjective = 4;
    public AudioSource doorbell;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!doingTask)
        {
            moveCharacter();
        }

        checkInputs();
    }

    void moveCharacter()
    {
        if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * characterSpeed);

        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            animator.SetBool("walking", false);
        } else
        {
            animator.SetBool("walking", true);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            sprite.flipX = true;
        } else if (Input.GetAxis("Horizontal") > 0)
        {
            sprite.flipX = false;
        }
    }

    void checkInputs()
    {
        if (Input.GetKeyUp("e") && nearbyTask != null && !doingTask && !nearbyTask.GetComponent<taskStation>().taskCompleted)
        {
            doingTask = true;
            nearbyTask.GetComponent<taskStation>().taskUI.SetActive(true);
        }
        else if (Input.GetKeyUp("e") && nearbyTask != null && doingTask)
        {
            doingTask = false;
            nearbyTask.GetComponent<taskStation>().taskUI.SetActive(false);
        }
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
        if (other.gameObject.tag == "Layer")
        {
            sprite.sortingOrder = other.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        }
        else if (other.gameObject.tag == "Sfx")
        {
            doorbell.Play();
        }
        else
        {
            taskPrompt.SetActive(true);
            promptBackground.SetActive(true);
            nearbyTask = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Layer")
        {
            taskPrompt.SetActive(false);
            promptBackground.SetActive(false);
            nearbyTask = null;
        }
    }
}
