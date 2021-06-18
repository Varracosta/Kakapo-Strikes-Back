using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private Image[] _lives;
    private const int MAX_LIVES = 3;
    public int numberOfLives;

    //Caching references
    public Sprite activeHeart;
    public Sprite inactiveHeart;

    void Start()
    {
        numberOfLives = MAX_LIVES;
    }

    void Update()
    {
        DisplayLives(numberOfLives);
    }

    public int GetLives() { return numberOfLives; }
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
    public void Respawn()
    {
        numberOfLives = MAX_LIVES;
    }
    public void AddLife()
    {
        if (numberOfLives < MAX_LIVES)
            numberOfLives++;
        else
            FindObjectOfType<UIManager>().AddToScore(500);
    }
}
