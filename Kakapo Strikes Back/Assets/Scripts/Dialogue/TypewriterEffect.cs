using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typeWriterSpeed = 50f;

    //Responsible for driving coroutine, takes a string of text to type, and a text label to type into
    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(WaitAndTypeText(textToType, textLabel));
    }

    //Responsible for typing the text
    private IEnumerator WaitAndTypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;

        float t = 0; //elapsed time
        int charIndex = 0;  //how many characters we want to type on screen at the given frame 

        while(charIndex < textToType.Length)
        {
            t += Time.deltaTime * typeWriterSpeed; //increment over time multiplied by desired type speed
            charIndex = Mathf.FloorToInt(t); //stores the largest int value smaller or equal to timer above
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length); //making sure that charIndex is not longer than textToType length

            textLabel.text = textToType.Substring(0, charIndex); //writing the text itself

            yield return null;
        }

        textLabel.text = textToType;
    }
}
