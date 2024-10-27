
using Assets.Scripts.Datas.Save;
using UnityEngine;

public class MusicMenu : MonoBehaviour
{
    public AudioSource mainthemeaudio;
    public static MusicMenu MusicInstance = null;

    public AudioClip audioClipStartMenu;
    public AudioClip audioClipDungeon;

    // Start is called before the first frame update
    void Awake()
    {
        if (MusicInstance==null)
        {
            //DontDestroyOnLoad(transform.gameObject);
            MusicInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        if (!SaveLoad.currentPrefs.loaded)
        {
            SaveLoad.currentPrefs = SaveLoad.LoadPrefs();
        }

        ReloadVolume();
        if (mainthemeaudio.volume > 0.01f)
        {
            mainthemeaudio.Play();
        }

    }
    public void ReloadVolume(float volume)
    {
        mainthemeaudio.volume = volume;
    }
    public void ReloadVolume()
    {
        ReloadVolume((SaveLoad.currentPrefs.musicVolume / 100f) * (SaveLoad.currentPrefs.totalVolume / 100f));
    }

    public void SetMusic(int i)
    {
        if (i == 0)
        {
            mainthemeaudio.clip = audioClipStartMenu;
        }
        if (i == 1)
        {
            mainthemeaudio.clip = audioClipDungeon;
        }
        ReloadVolume();
    }


    public void PlayMusic()
    {
        if (mainthemeaudio.isPlaying) return;
        mainthemeaudio.Play();
    }

    public void StopMusic()
    {
        mainthemeaudio.Stop();
    }
}
