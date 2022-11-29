using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class swiper : MonoBehaviour
{
    public float swiperSpeed = 1f;
    public GameObject swiperBox;
    public GameObject player;
    public GameObject progressBar;
    public string key;

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
        moveSwiper();

        handleInputs();
    }

    void moveSwiper()
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
    }

    void handleInputs()
    {
        if (Input.GetKeyDown(key))
        {
            if (colliderTag == "Success")
            {
                playerScript.playerScore++;
                playerScript.doingTask = false;
                playerScript.nearbyTask.GetComponent<taskStation>().taskCompleted = true;

                if (playerScript.playerScore >= playerScript.scoreObjective)
                {
                    SceneManager.LoadScene("WinScene");
                } else
                {
                    float completionPercent = playerScript.playerScore / playerScript.scoreObjective;
                    playerScript.scoreDisplay.gameObject.GetComponent<TMP_Text>().text = (Mathf.Round(completionPercent * 100)).ToString() + " %";
                    progressBar.GetComponent<Image>().fillAmount = completionPercent;
                }
            }

            playerScript.nearbyTask.GetComponent<taskStation>().taskUI.SetActive(false);
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
