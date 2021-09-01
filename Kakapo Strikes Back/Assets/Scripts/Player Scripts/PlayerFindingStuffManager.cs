using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFindingStuffManager : MonoBehaviour
{
    #region Main Info
    [SerializeField] private Transform searchPoint;
    [SerializeField] private LayerMask whatIsCreature;
    [SerializeField] private LayerMask whatIsCone;
    public float searchRadius;
    #endregion

    void Start()
    {
        searchRadius = 5f;
    }

    void Update()
    {
        SearchCreatures();
        SearchCones();
    }

    private void SearchCreatures()
    {
        Collider2D[] creatures = Physics2D.OverlapCircleAll(searchPoint.position, searchRadius, whatIsCreature);
        GameScoreStats.instance.AddToCreaturesList(creatures);
    }

    private void SearchCones()
    {
        Collider2D[] cones = Physics2D.OverlapCircleAll(searchPoint.position, searchRadius, whatIsCone);
        GameScoreStats.instance.AddToConesList(cones);
    }
}
