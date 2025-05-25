using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private string spawnTag;

    void Awake()
    {
        // Singleton persistente
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName, string spawnTag = "SpawnPoint")
    {
        this.spawnTag = spawnTag;
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Esperar un frame para que todo esté inicializado
        StartCoroutine(MovePlayerToSpawn());
    }

    private System.Collections.IEnumerator MovePlayerToSpawn()
    {
        yield return null;

        if (string.IsNullOrEmpty(spawnTag))
        {
            yield break;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject spawn = GameObject.FindGameObjectWithTag(spawnTag);

        if (player != null && spawn != null)
        {
          player.transform.position = spawn.transform.position;
          player.transform.rotation = spawn.transform.rotation;
        }
        else
        {
          Debug.LogWarning("⚠️ No se encontró el jugador o el punto de spawn con tag: " + spawnTag);
        }

        spawnTag = null;
    }
}
