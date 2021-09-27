using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextField : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;
    [SerializeField] private DialogueObject textToShow;
    [SerializeField] private GameObject button;
    private TypewriterEffect typeWriter;

    void Start()
    {
        typeWriter = GetComponent<TypewriterEffect>();

        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(WaitAndShowText(textToShow));
        }
    }
    
    private IEnumerator WaitAndShowText(DialogueObject textToShow)
    {
        foreach (string text in textToShow.Dialogue)
        {
            yield return typeWriter.Run(text, textField);
        }
        yield return new WaitForSeconds(1); 
        button.SetActive(true);
    }
}
