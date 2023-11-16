using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject ButtonPushSound;

    public GameObject YesButton;
    public GameObject NoButton;
    public GameObject QuitButton;
    public GameObject AreYouSureButton;
    public GameObject CreditsText;
    public GameObject Controls;
    public GameObject Cheats;
    public GameObject KeyArt;
    public GameObject MainTitle;

    public string CreditsScene = "Credits";
    public string MainScene = "SampleScene";

    public void StartGame()
    {
        Instantiate(ButtonPushSound, transform.position, Quaternion.identity);
        SceneManager.LoadScene(MainScene);
    }
    public void TerminateGame()
    {
        Instantiate(ButtonPushSound, transform.position, Quaternion.identity);
        Application.Quit();
    }

    public void CancelConfirm()
    {
        Instantiate(ButtonPushSound, transform.position, Quaternion.identity);
        YesButton.SetActive(true);
        NoButton.SetActive(true);
        AreYouSureButton.SetActive(true);
        QuitButton.SetActive(false);
    }

    public void Unsure()
    {
        Instantiate(ButtonPushSound, transform.position, Quaternion.identity);
        YesButton.SetActive(false);
        NoButton.SetActive(false);
        AreYouSureButton.SetActive(false);
        QuitButton.SetActive(true);
    }

    public void Credits()
    {
        Instantiate(ButtonPushSound, transform.position, Quaternion.identity);
        if (CreditsText.activeSelf == false)
        {
            CreditsText.SetActive(true);
            KeyArt.SetActive(false);
            Controls.SetActive(false);
            Cheats.SetActive(false);
            MainTitle.SetActive(false);
        }
        else
        {
            CreditsText.SetActive(false);
            KeyArt.SetActive(true);
            MainTitle.SetActive(true);
        }
    }

    public void ControlsButton()
    {
        Instantiate(ButtonPushSound, transform.position, Quaternion.identity);
        if (Controls.activeSelf == false)
        {
            CreditsText.SetActive(false);
            KeyArt.SetActive(false);
            Controls.SetActive(true);
            Cheats.SetActive(false);
            MainTitle.SetActive(false);
        }
        else
        {
            Controls.SetActive(false);
            KeyArt.SetActive(true);
            MainTitle.SetActive(true);
        }
    }

    public void CheatsButton()
    {
        Instantiate(ButtonPushSound, transform.position, Quaternion.identity);
        if (Cheats.activeSelf == false)
        {
            CreditsText.SetActive(false);
            KeyArt.SetActive(false);
            Controls.SetActive(false);
            Cheats.SetActive(true);
            MainTitle.SetActive(false);
        }
        else
        {
            Cheats.SetActive(false);
            KeyArt.SetActive(true);
            MainTitle.SetActive(true);
        }
    }

    public void LoadCredits()
    {
        Instantiate(ButtonPushSound, transform.position, Quaternion.identity);
        SceneManager.LoadScene(CreditsScene);
    }




}
