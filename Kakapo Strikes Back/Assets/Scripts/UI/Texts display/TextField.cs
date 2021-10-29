using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Text field for Total Score scenes for lvl 3 and lvl 4 where just a big chunk of text is displayed with help of type writer 
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
