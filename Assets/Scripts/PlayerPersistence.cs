using UnityEngine;

public class PlayerPersistence : MonoBehaviour
{
  private static bool playerExists = false;

  void Awake()
  {
    if (playerExists)
    {
      Destroy(gameObject);
    }
    else
    {
      playerExists = true;
      DontDestroyOnLoad(gameObject);
    }
  }
}
