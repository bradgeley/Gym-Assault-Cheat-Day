using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] int playerHits = 10;
    [SerializeField] float reloadSceneDelay = 3f;
    [SerializeField] float deathDelay = 0.25f;
    [SerializeField] Transform parent;
    [SerializeField] GameObject deathFX;

    //Lives
    [SerializeField] int lives = 5;
    [SerializeField] LivesCounter livesCounter;
    private float timeOfLastCollision = 0;
    private float minTimeBetweenCollisions = 0.25f;

    private void Start()
    {
        livesCounter.setLives(lives);
    }
    private void OnTriggerEnter(Collider other)
    {
        float currentTime = Time.time;
        if (currentTime - timeOfLastCollision > minTimeBetweenCollisions)
        {
            DecrementLivesCounter();
            timeOfLastCollision = currentTime;
        }

        if (lives <= 0)
        {
            StartDeathSequence();
        }
    }

    private void DecrementLivesCounter()
    {
        lives -= 1;
        livesCounter.setLives(lives);
    }

    private void StartDeathSequence() //Called by string reference
    {
        GetComponent<PlayerController>().SendMessage("ReceiveDeathMessage");
        RemovePlayerFromGame();
        CreateExplosion();
        Invoke("ReloadScene", reloadSceneDelay);
    }

    private void RemovePlayerFromGame() //specific to the way player is setup.. not the best way to do it
    {
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        BoxCollider[] boxes = gameObject.GetComponents<BoxCollider>();
        foreach (BoxCollider box in boxes)
        {
            box.enabled = false;
        }
    }

    private void ReloadScene() //Called by string reference
    {
        SceneManager.LoadScene(1);
    }

    private void CreateExplosion() //Find way to not duplicate from enemy
    {
        GameObject FX = Instantiate(deathFX, transform.position, Quaternion.identity);
        FX.transform.parent = parent; //child the explosion game object to a parent that will hold all temporary objects
    }
}
