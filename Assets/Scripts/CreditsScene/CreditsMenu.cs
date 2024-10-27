using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public void OnClickBack()
    {
        GameManager.GetInstance().LoadLevel(SceneGame.Credits, SceneGame.Start);
    }
}
