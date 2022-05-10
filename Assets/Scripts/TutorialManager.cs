using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialTips;
    public GameObject player;

    private int index; // Punto del tutorial en el que me encuentro
    
    private PlayerController playerController;
    private float jumpForceValue;

    private float tol = 0.000001f;

    private void Start()
    {
        index = 0;
        ShowTutorial(index);
        
        playerController = FindObjectOfType<PlayerController>();
        jumpForceValue = playerController.jumpForce;
        playerController.jumpForce = 0;
    }

    void Update()
    {
        if (index == 0)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                StartCoroutine(ShowNext());
            }
        }else if (index == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space) && player.transform.position.x > tutorialTips[1].transform.position.x)
            {
                playerController.jumpForce = jumpForceValue;
                StartCoroutine(ShowNext());
            }
        }else if (index == 2)
        {

        }
        
    }

    private void ShowTutorial(int idx)
    {
        for (int i = 0; i < tutorialTips.Length; i++)
        {
            if (i == idx)
            {
                tutorialTips[i].SetActive(true);
                StartCoroutine(FadeIn(i));
                StartCoroutine(Letters(i));
            }
            else
            {
                tutorialTips[i].SetActive(false);
            }
        }
    }

    private IEnumerator ShowNext()
    {
        index++;
        yield return StartCoroutine(FadeOut(index - 1));
        ShowTutorial(index);
    }

    private IEnumerator FadeIn(int idx)
    {
        GameObject child = tutorialTips[idx].transform.GetChild(0).gameObject;
        TextMeshProUGUI textAlpha = child.GetComponent<TextMeshProUGUI>();
        textAlpha.alpha = 0;
        
        for (float i = 0; i < 1; i += 0.1f)
        {
            textAlpha.alpha = i;
            yield return new WaitForSeconds(0.2f);
        }
        textAlpha.alpha = 1;
    }
    
    private IEnumerator FadeOut(int idx)
    {

        GameObject child = tutorialTips[idx].transform.GetChild(0).gameObject;
        TextMeshProUGUI textAlpha = child.GetComponent<TextMeshProUGUI>();
        textAlpha.alpha = 1;
        
        for (float i = 1; i > 0; i -= 0.1f)
        {
            textAlpha.alpha = i;
            yield return new WaitForSeconds(0.1f);
        }
        textAlpha.alpha = 0;
    }

    private IEnumerator Letters(int idx)
    {
        GameObject child = tutorialTips[idx].transform.GetChild(0).gameObject;
        TextMeshProUGUI message = child.GetComponent<TextMeshProUGUI>();
        string originalMessage = message.text;

        message.text = "";

        foreach (char c in originalMessage)
        {
            message.text = $"{message.text}{c}";
            yield return new WaitForSeconds(0.1f);
        }
    }
}
