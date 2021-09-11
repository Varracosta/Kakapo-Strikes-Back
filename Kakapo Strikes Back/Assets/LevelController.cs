using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private AudioClip levelCompleteSFX;

    private string levelName;
    private void Start()
    {
        levelName = SceneManager.GetActiveScene().name;        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Kakapo"))
        {
            AudioSource.PlayClipAtPoint(levelCompleteSFX, Camera.main.transform.position, 0.5f);
            PlayerPrefs.SetString("LevelPassed", levelName);
            SceneLoader.instance.LoadTotalScoreScene();
        }
    }
}
