using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeBehaviour : MonoBehaviour
{
    public TextMeshProUGUI textVolume;
    public Slider VolumeSlider;
    private int volumeNumber;

    public void Awake()
    {

        VolumeSlider.onValueChanged.AddListener(OnVolumeChanged);

    }
    public void OnVolumeChanged(float val)
    {
        int volume = (int)val;
        this.volumeNumber = volume;
        textVolume.text = (volume-1) + "%";
    }
    public void SetVolume(int volume)
    {

        if (volume == 0)
        {
            volume = 50;
        }
        VolumeSlider.wholeNumbers = true;
        VolumeSlider.minValue = 1;
        VolumeSlider.maxValue = 101;
        this.volumeNumber = volume+1;
        VolumeSlider.value = volumeNumber;
        textVolume.text = (volumeNumber-1) + "%";
    }

    public int GetVolume()
    {
        return volumeNumber-1;
    }
}
