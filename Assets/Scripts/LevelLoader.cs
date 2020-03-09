using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    const float START_DELAY = 5f;

    void Start()
    {
        Invoke("StartGame", START_DELAY);
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
