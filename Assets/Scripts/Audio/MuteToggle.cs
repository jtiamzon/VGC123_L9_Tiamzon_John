using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteToggle : MonoBehaviour
{
    Toggle muteToggle;
    
    // Start is called before the first frame update
    void Start()
    {
        muteToggle = GetComponent<Toggle>();
        if(AudioListener.volume == 0)
        {
            muteToggle.isOn = false;
        }
    }

    public void ToggleAudioOnValueChange(bool audioIn)
    {
        if (audioIn)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }
}
