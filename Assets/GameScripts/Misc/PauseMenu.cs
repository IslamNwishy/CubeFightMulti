using UnityEngine;
using Photon.Pun;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    private bool isActive = true;
    public GameObject PausePanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isActive)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {

        PausePanel.SetActive(false);
        isPaused = false;

    }

    void Pause()
    {
        PausePanel.SetActive(true);
        isPaused = true;

    }

    public void MainMenu()
    {
        isActive = false;
        StartCoroutine("MenuDelay");
    }

    IEnumerator MenuDelay()
    {
        PhotonNetwork.Disconnect();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }
}
