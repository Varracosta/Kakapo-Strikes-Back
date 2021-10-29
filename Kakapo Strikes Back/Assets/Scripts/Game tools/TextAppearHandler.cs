using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Displays text in Total Score Scene according to timer. Activates continue button after all texts were displayed
public class TextAppearHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private GameObject button;
    [SerializeField] private AudioClip thudSFX;
    private const float MAX_TIME = 1.5f;
    private float startTime = 0f;
    private int counter = 0;
    private bool isFinished;

    void Start()
    {
        startTime = MAX_TIME;
        isFinished = false;
    }

    void Update()
    {
        DisplayInfo();
        DisplayButton();
    }

    private void DisplayInfo()
    {
        startTime -= 1f * Time.deltaTime;

        if(startTime <= 0)
        {
            if (counter == texts.Length)
            {
                isFinished = true;
                return;
            }
            else
            {
                texts[counter].gameObject.SetActive(true);
                AudioSource.PlayClipAtPoint(thudSFX, Camera.main.transform.position);
                counter++;
                startTime = MAX_TIME;
            }
        }
    }
    private void DisplayButton()
    {
        if (isFinished)
            button.SetActive(true);
    }
}
