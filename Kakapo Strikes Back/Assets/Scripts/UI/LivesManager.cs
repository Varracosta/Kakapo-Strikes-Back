using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private Image[] _lives;
    private const int MAX_LIVES = 3;
    public int NumberOfLives { get; private set; }

    //Caching references
    public Sprite activeHeart;
    public Sprite inactiveHeart;

    void Start()
    {
        NumberOfLives = MAX_LIVES;
    }

    void Update()
    {
        DisplayLives(NumberOfLives);

        if(NumberOfLives == 0)
        {
            FindObjectOfType<SceneLoader>().GameOver();
        }
    }

    public void DisplayLives(int numberOfLives)
    {
        //creating basic "frame" of hearts with empty heart sprites 
        foreach (Image heart in this._lives)
        {
            heart.sprite = inactiveHeart;
        }

        //"filling" empty hearts with active hearts depending on number of lives
        for (int i = 0; i < numberOfLives; i++)
        {
            _lives[i].sprite = activeHeart;
        }
    }
    public void DecreaseLives(int damage)
    {
        NumberOfLives -= damage;
    }
    public void Respawn()
    {
        NumberOfLives = MAX_LIVES;
    }
    public void AddLife()
    {
        if (NumberOfLives < MAX_LIVES)
            NumberOfLives++;
        else
            FindObjectOfType<UIManager>().AddToScore(500);
    }
}
