using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//The scripts sits on Level finish flag (finish point). If Player reaches it, the script saves Player progress (passed level), 
//loads Total Score Scene, plays sound
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
