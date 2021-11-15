using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUISystem : MonoBehaviour
{
    public GameObject m_settingsPanel;
    public GameObject m_mainPauseMenu;
    public GameObject m_pauseMenu;
    public static bool m_inMenu { get; private set; }

    public void Update()
    {
        if(Input.GetKeyDown(Keybindings.m_pauseMenuToggle))
        {
            m_pauseMenu.SetActive(!m_pauseMenu.activeSelf);
            PauseUISystem.m_inMenu = (!m_pauseMenu.activeSelf);
        }
    }


    public void QuitToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void ToggleSettings(int state)
    {
        if (state == 1)
        {
            m_mainPauseMenu.SetActive(false);
            m_settingsPanel.SetActive(true);
        }
        else if (state == 0)
        {
            m_mainPauseMenu.SetActive(true);
            m_settingsPanel.SetActive(false);
        }
    }


}
