using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Portal : MonoBehaviour
{
  [Header("Escena a cargar")]
  public string sceneToLoad;

  [Header("Nombre del portal")]
  public string portalDisplayName;

  [Header("Texto del portal (TextMeshPro 3D)")]
  public TextMeshPro portalLabel;

  private void Start()
  {
    if (portalLabel != null)
    {
      portalLabel.text = portalDisplayName;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
        // SceneManager.LoadScene(sceneToLoad);
        SceneLoader.Instance.LoadScene(sceneToLoad, "SpawnPoint");
    }
  }
}
