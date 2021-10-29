using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

//Displays text from DialogueObj in dialogue box, opens and closes the later. 
public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;

    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();

    }

    public void ShowDialogue(DialogueObject dialogueObj)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObj));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObj)
    {
        foreach (string dialogue in dialogueObj.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
            yield return new WaitForSeconds(2);
        }
        CloseDialogueBox();
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
