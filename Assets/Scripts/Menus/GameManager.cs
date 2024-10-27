
using Assets.Scripts.Datas.Save;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;

    public static GameManager GetInstance()
    {
        return Instance;
    }
    public bool isDummy = true;

    public int level;
    public bool isPausedMenu = false;
    public bool isPausedPopup = false;
    private SceneManagerGame sceneManagerGame;
    public static bool _debugNoSave;

    [SerializeField]
    private FadeEffectUI fade;

    public bool IsPaused()
    {
       // Debug.Log(isPaused + " " + isPausedPopup);
        return isPausedMenu || isPausedPopup;
    }



    private void Load()
    {
        SaveLoad.Load(_debugNoSave);
        level = SaveLoad.currentSave.currentGame.floor;
        sceneManagerGame = new SceneManagerGame();

    }

    public void Awake()
    {
        _debugNoSave = Application.isEditor;
        if (Instance == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            Instance = this;
            Instance.Load();
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void PrevLevel(SceneGame from)
    {
        StartCoroutine(PrevLevelCo(from));
    }
    private IEnumerator PrevLevelCo(SceneGame from)
    {
        yield return fade.StartFadeIn();
        sceneManagerGame.GoToPreviousSceneAndSaveCurrent(from);
        yield return fade.StartFadeOut();
    }
    public void LoadLevel(SceneGame from, SceneGame to)
    {
        StartCoroutine(LoadLevelCo(from, to));
    }
    private IEnumerator LoadLevelCo(SceneGame from, SceneGame to)
    {
        isDummy = false;
        bool stopmusic = false;
        int musicFrom;
        int musicTo;

        if (to == SceneGame.Dungeon)
        {
            musicTo = 1;
        }
        else {
            musicTo = 0;
        }
        if (to == SceneGame.Dungeon)
        {
            musicFrom = 1;
        }
        else
        {
            musicFrom = 0;
        }
        stopmusic = musicFrom != musicTo;


        if (stopmusic)
        {
            MusicMenu.MusicInstance.StopMusic();
        }

        MusicMenu.MusicInstance.SetMusic(musicTo);
        yield return fade.StartFadeIn();
        sceneManagerGame.GoToSceneAndSavePrevious(from, to);
        yield return fade.StartFadeOut();
        if (stopmusic)
        {
            MusicMenu.MusicInstance.PlayMusic();
        }
    }

}
