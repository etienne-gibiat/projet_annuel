using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManagerScript : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Dropdown dropResolution;
    [SerializeField]
    private Toggle toggleFullscreen;

    // Start is called before the first frame update
    void Start()
    {
        List<string> resolutionList = new List<string>();
        Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution resolution in resolutions)
        {
            string resolutionText = resolution.width + " x " + resolution.height;
            resolutionList.Add(resolutionText);
        }
        dropResolution.AddOptions(resolutionList);
    }

    public void ChangeResolution()
    {
        int dropValue = dropResolution.value;
        Screen.SetResolution(Screen.resolutions[dropValue].width, Screen.resolutions[dropValue].height, toggleFullscreen.isOn);
    }

    public void ChangeFullscreen()
    {
        Screen.fullScreen = toggleFullscreen.isOn; 
    }

}
