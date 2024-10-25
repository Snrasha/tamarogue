using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffectUI : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 0.5f;

    [SerializeField]
    private Image fade;

    public IEnumerator StartFadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);

            fade.color = new Color(0, 0, 0, strength);// elapsedTime / duration);
          //  Debug.Log(fade.color);
            yield return null;
        }
        fade.color = new Color(0, 0, 0, 1f);
    }
    public IEnumerator StartFadeOut()
    {
        float elapsedTime = 0f;
        //  Debug.Log("SHAKING");
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(1-elapsedTime / duration);

            fade.color = new Color(0, 0, 0, strength);//1 - (elapsedTime / duration));
            yield return null;
        }
        fade.color = new Color(0, 0, 0, 0f);
    }
}
