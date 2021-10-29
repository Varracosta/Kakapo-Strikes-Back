using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Responsible for moving a turret and a crosshair (Level 4), and shooting from turret. 
public class TurretShooting : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private Transform bulletDirection;


    private PlayerInputHandler inputHandler;

    void Start()
    {
        inputHandler = FindObjectOfType<PlayerInputHandler>();
    }
    void Update()
    {
        if (PauseMenu.isPaused) { return; }

        Point();
        Shoot();
    }

    //Turret will point to mouse/crosshair position
    private void Point()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(inputHandler.MousePosition);
        Vector3 targetDirection = (mouseWorldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    //Instantiates bullets
    private void Shoot()
    {
        if (inputHandler.ShootInput)
        {
            inputHandler.StopShooting();
            GameObject g = Instantiate(bullet, bulletDirection.position, bulletDirection.rotation);
            g.SetActive(true);
        }
    }
}
