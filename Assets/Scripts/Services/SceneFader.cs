using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// the  script designed to handle scene transitions with a fade effect.
public class SceneFader : MonoBehaviour
{
    [SerializeField] Image img;
    public AnimationCurve curve; // defines the fade-in/fade-out animation.

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 0.8f; // the duration of the fade-in animation
        while (t > 0f)
        {
            t -= Time.deltaTime;
            // calculates the alpha value (a) based on the current t value. This alpha value will be used to fade in the image.
            float a = curve.Evaluate(t);
            // changes the transparency of the image, creating the fade-in effect.
            img.color = new Color(0f, 0f, 0f, a);
            // waits for the next frame before continuing the coroutine.
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f; // represents the starting point of the fade-out animation.
        while (t < 0.8f)
        {
            t += Time.deltaTime;
            // calculates the alpha value (a) based on the current t value. This alpha value will be used to fade out the image.
            float a = curve.Evaluate(t);
            // changes the transparency of the image, creating the fade-out effect.
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        // Once the fade-out animation is complete, the SceneManager.LoadScene method is called to load the target scene.
        SceneManager.LoadScene(scene);
    }
}
