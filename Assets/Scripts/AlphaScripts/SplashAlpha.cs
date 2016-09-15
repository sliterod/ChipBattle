using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashAlpha : MonoBehaviour {

    bool isInputCaptureEnabled;
    public string targetScene;

    public GameObject logo;
    public GameObject text;
    public GameObject press;
    public GameObject howToPlay;

    // Use this for initialization
    void Start () {
        int firstTime = PlayerPrefs.GetInt("alphaFirst", 0);
        if (firstTime == 0)
        {
            PlayerPrefs.SetInt("alphaFirst", 1);
            StartCoroutine(Disclaimer());
        }
        else {
            StartCoroutine(Splash());
        }
    }

    void Update()
    {
        if (isInputCaptureEnabled) {
            if (Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.Q))
            {
                isInputCaptureEnabled = false;
                LoadDemoScene();
            }
        }
    }

    /// <summary>
    /// Loads demo scene
    /// </summary>
    void LoadDemoScene() {
        SceneManager.LoadScene(targetScene);
    }

    /// <summary>
    /// Enables input capture
    /// </summary>
    void EnableInputCapture() {
        isInputCaptureEnabled = true;
    }

    IEnumerator Disclaimer() {

        iTweenEvent.GetEvent(logo, "show").Play();
        yield return new WaitForSeconds(1.0f);

        iTweenEvent.GetEvent(text, "show").Play();
        yield return new WaitForSeconds(10.0f);

        iTweenEvent.GetEvent(text, "hide").Play();
        yield return new WaitForSeconds(0.5f);

        howToPlay.SetActive(true);
        yield return new WaitForSeconds(2.0f);

        iTweenEvent.GetEvent(press, "show").Play();
        yield return new WaitForSeconds(0.1f);

        EnableInputCapture();
    }

    IEnumerator Splash() {

        iTweenEvent.GetEvent(logo, "show").Play();
        yield return new WaitForSeconds(0.5f);

        howToPlay.SetActive(true);
        yield return new WaitForSeconds(2.0f);

        iTweenEvent.GetEvent(press, "show").Play();
        yield return new WaitForSeconds(0.1f);

        EnableInputCapture();
    }

}
