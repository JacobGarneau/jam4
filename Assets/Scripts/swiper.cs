using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class swiper : MonoBehaviour
{
    public float swiperSpeed = 1f;
    public GameObject swiperBox;
    public GameObject player;

    private float directionalSpeed = 1f;
    private float swiperBoxWidth;
    private string colliderTag = null;
    playerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        swiperBoxWidth = swiperBox.GetComponent<RectTransform>().rect.width / 2;
        playerScript = player.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (transform.localPosition.x < -swiperBoxWidth)
        {
            directionalSpeed = swiperSpeed;
        }

        if (transform.localPosition.x > swiperBoxWidth)
        {
            directionalSpeed = -swiperSpeed;
        }

        

        transform.position += new Vector3(directionalSpeed, 0, 0);

        handleInputs();
    }

    void handleInputs()
    {
        if (Input.GetKeyDown("q"))
        {
            if (colliderTag == "Success")
            {
                playerScript.playerScore++;

                playerScript.doingTask = false;
                playerScript.nearbyTask.GetComponent<taskStation>().taskUI.SetActive(false);
            }
            else if (colliderTag == "Failure")
            {
                playerScript.playerScore--;
            }
            
            playerScript.scoreDisplay.gameObject.GetComponent<TMP_Text>().text = playerScript.playerScore.ToString();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        colliderTag = collision.gameObject.tag;
    }

    private void OnTriggerExit(Collider collision)
    {
        colliderTag = null;
    }
}
