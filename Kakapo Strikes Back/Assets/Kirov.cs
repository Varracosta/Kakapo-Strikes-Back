using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Kirov : MonoBehaviour
{
    [Header("Kirov Movement info")]
    [SerializeField] private Transform stopPoint;
    [SerializeField] private Transform bombingPoint;
    [SerializeField] private float speed = 2f;
    private Transform[] stopPositions;
    
    [Header("Kirov Audio")]
    [SerializeField] private AudioClip hellMarch;
    [SerializeField] private AudioClip kirovFlying;
    [SerializeField] private AudioClip kirovReporting;
    [SerializeField] private AudioClip airshipReady;
    [SerializeField] private AudioClip aknowledged;
    [SerializeField] private AudioClip targetAcquired;
    [SerializeField] private AudioClip bombardiersToStations;
    [SerializeField] private AudioClip bombingBayReady;
    [SerializeField] private AudioClip closingOnTarget;

    [Header("Kirov Bombs")]
    [SerializeField] private AudioClip bombBlow;
    [SerializeField] private AudioClip bombWhistle;
    [SerializeField] private GameObject bomb;

    private Animator animator;
    private AudioSource audioSource;

    public Event customEvent;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
     > When smth happens, it triggers Kirov to appear on a scene
        - Transform.position - make Kirov moving
        - Sounds:
            - *Hell march starts playing*
            - *flying sound*
            - "Kirov reporting"
            - "Airship is ready"  - when Kirov arrives and stops
    
     > Kakapo "gives" Kirov target    
            - "Aknowledged"
            - *starts moving towards the Possum*
            - "target acquired"
            - "bombardiers to your stations"

     > Kirov stops above the Possum 
            - "Closing on target"
            - "Bombing bays are ready"

     > Bombs start to fall 
            - *bombs are whistling*
            - *bombs blow*
     
     */

    private void Moving()
    {
        float movementThisFrame = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, stopPoint.transform.position, movementThisFrame);

        if (transform.position == stopPoint.transform.position)
        {

        }
    }
    private void Bombing()
    {
        StartCoroutine(WaitAndStopBombing());
    }
    private IEnumerator WaitAndStopBombing()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("Attack", false);
    }
}
