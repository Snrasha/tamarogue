
using Assets.Scripts.Datas.Save;
using UnityEngine.SceneManagement;
public enum SceneGame
{
    Start,
    Credits,
    Choice,
    Dungeon,
    Settings,
}
public class SceneManagerGame
{
 
    private string GetScene(SceneGame name)
    {
        string realscene;
        switch (name)
        {
            case SceneGame.Choice:
                realscene = "ChoiceMenu";
                break;
            case SceneGame.Credits:
                realscene = "CreditsMenu";
                break;
            case SceneGame.Settings:
                realscene = "SettingsMenu";
                break;
            case SceneGame.Dungeon:
                realscene = "DungeonScene";
                break;
            default:
                realscene = "StartMenu";
                break;

        }

        return realscene;
    }
    public void GoToPreviousSceneAndSaveCurrent(SceneGame current)
    {
        string sc = GetScene(current);
        string prev = SaveLoad.currentPrefs.previousScene;
        if (prev == null)
        {
            prev = GetScene(SceneGame.Start);
        }
        SaveLoad.currentPrefs.previousScene = sc;

        GoToScene(prev);
    }
    public void GoToSceneAndSavePrevious(SceneGame from,SceneGame to)
    {
        SaveLoad.currentPrefs.previousScene = GetScene(from);
        GoToScene(GetScene(to));
    }
    public void GoToScene(string to)
    {
        SceneManager.LoadScene(to);
    }
    public void GoToScene(SceneGame to)
    {
        string sc=GetScene(to);
        GoToScene(sc);
    }


}
