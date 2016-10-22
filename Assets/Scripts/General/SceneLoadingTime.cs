using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoadingTime : MonoBehaviour {

    public string targetScene;
    public float targetTime;

	// Use this for initialization
	void Start () {

        if (targetTime <= 0.0f) targetTime = 2.0f;

        ChooseLoadingTarget();
	}

    /// <summary>
    /// Chooses how the next scene will be loaded
    /// </summary>
    void ChooseLoadingTarget() {
        switch (targetScene) {
            case "":
                targetScene = "middle_load";
                StartCoroutine(LoadTargetScene());
                break;

            case "exit":
                StartCoroutine(ExitGame());
                break;

            default:
                StartCoroutine(LoadTargetScene());
                break;
        }

        
    }

    /// <summary>
    /// Loads new scene
    /// </summary>
    /// <returns>The new scene</returns>
    IEnumerator LoadTargetScene() {
        yield return new WaitForSeconds(targetTime);

        SceneManager.LoadScene(targetScene);
    }

    /// <summary>
    /// Starts a timer. After finishing it, the application quits automatically
    /// </summary>
    /// <returns>Quits the game</returns>
    IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(targetTime);
        
    #if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_STANDALONE || UNITY_ANDROID || UNITY_IPHONE
          Application.Quit();
    #endif  

    }
}
