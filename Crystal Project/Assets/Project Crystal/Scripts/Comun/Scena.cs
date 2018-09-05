using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scena : MonoBehaviour
{

    public void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("02_MainMenu");
        }
    }

    public void IrAOmali()
    {
        SceneManager.LoadScene("03_Omali");
    }

    public void IrAOmaliProt()
    {
        SceneManager.LoadScene("03_Omali_Prototipo");
    }

    public void IrAOmaliProt2()
    {
        SceneManager.LoadScene("03_Omali_Prototipo2 1");
    }

    public void IrACamino()
    {
        SceneManager.LoadScene("04_Camino");
    }

    public void IrABatalla()
    {
        SceneManager.LoadScene("00_Batalla");
    }
}
