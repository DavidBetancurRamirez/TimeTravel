using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelManager : MonoBehaviour
{
    public GameObject pastEnvironment;
    public GameObject presentEnvironment;
    public GameObject futureEnvironment;

    public void ShowPast()
    {
        pastEnvironment.SetActive(true);
        presentEnvironment.SetActive(false);
        futureEnvironment.SetActive(false);
    }

    public void ShowPresent()
    {
        pastEnvironment.SetActive(false);
        presentEnvironment.SetActive(true);
        futureEnvironment.SetActive(false);
    }

    public void ShowFuture()
    {
        pastEnvironment.SetActive(false);
        presentEnvironment.SetActive(false);
        futureEnvironment.SetActive(true);
    }
}
