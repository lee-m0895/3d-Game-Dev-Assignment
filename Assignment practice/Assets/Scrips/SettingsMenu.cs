using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class SettingsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Panel, settingsMenu, mapMenu, inventoryMenu;
    public GameObject Player;
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    void Start()
    {
        Panel.SetActive(false);
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();



        List<string> options = new List<string>();

        int current = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                current = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = current;
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            openSettings();
        }
    }

    public void openSettings()
    {
        if (Panel.activeSelf)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
            Panel.SetActive(false);
            CharacterController controller = Player.GetComponent<CharacterController>();
            Player.GetComponent<FirstPersonController>().enabled = true;
            controller.enabled = true;
            Time.timeScale = 1f;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CharacterController controller = Player.GetComponent<CharacterController>();
            Player.GetComponent<FirstPersonController>().enabled = false;
            controller.enabled = false;
            Panel.SetActive(true);
            Time.timeScale = 0f;
        }
        
    }



    public void showSettings()
    {
        if (mapMenu.activeSelf)
        {
            mapMenu.SetActive(false);
        }
        if (inventoryMenu.activeSelf)
        {
            inventoryMenu.SetActive(false);
        }
        settingsMenu.SetActive(true);
    }


    public void showMap()
    {
        
        if (inventoryMenu.activeSelf)
        {
            inventoryMenu.SetActive(false);
        }
        if (settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
        }

        mapMenu.SetActive(true);
    }


    public void showInventory()
    {
        if (mapMenu.activeSelf)
        {
            mapMenu.SetActive(false);
        }
        if (settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
        }
        inventoryMenu.SetActive(true);
    }


    public void SetRes(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


}
