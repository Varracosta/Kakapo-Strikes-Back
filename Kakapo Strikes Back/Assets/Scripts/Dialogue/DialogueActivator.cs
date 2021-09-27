using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject dialogueObject;

    private void Start()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(WaitAndShowDialogue(dialogueObject));
        }
    }

    private IEnumerator WaitAndShowDialogue(DialogueObject dialogueObj)
    {
        yield return new WaitForSeconds(2);
        dialogueUI.ShowDialogue(dialogueObj);
    }
}
