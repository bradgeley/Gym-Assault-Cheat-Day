using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float reloadSceneDelay = 3f;
    [SerializeField] GameObject deathFX;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence() //Called by string reference
    {
        GetComponent<PlayerController>().SendMessage("ReceiveDeathMessage");
        deathFX.SetActive(true);
        Invoke("ReloadScene", reloadSceneDelay);
    }

    private void ReloadScene() //Called by string reference
    {
        SceneManager.LoadScene(1);
    }
}
