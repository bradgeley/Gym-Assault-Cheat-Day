using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;

    //Scoring
    [SerializeField] int pointsOnKill;
    ScoreBoard scoreboard;

    private void Start()
    {
        AddBoxCollider();
        scoreboard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        if (!gameObject.GetComponent<Collider>())
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        CreateExplosion();
        Destroy(gameObject);
        AwardPointsToPlayer();
    }

    private void AwardPointsToPlayer()
    {
        scoreboard.ScoreHit(pointsOnKill);
    }

    private void CreateExplosion()
    {
        GameObject FX = Instantiate(deathFX, transform.position, Quaternion.identity);
        FX.transform.parent = parent; //child the explosion game object to a parent that will hold all temporary objects
    }
}
