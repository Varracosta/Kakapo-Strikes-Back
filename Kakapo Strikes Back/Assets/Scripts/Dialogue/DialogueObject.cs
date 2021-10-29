using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable Object for any dialogues. Contains text fields (string array) for text input
[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    public string[] Dialogue => dialogue;
}
