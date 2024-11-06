using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    [SerializeField] public Animator transition;

    public void LoadScene(string sceneName)
    {
        // loadingScreen.SetActive(true);

        // Run the Async
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        transition.SetTrigger("Start");
        // AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        // while(!loadOperation.isDone)
        // {
        //     float progessValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
        //     loadingSlider.value = progessValue;
        //     yield return null;
        // }
        // loadingSlider.value = 0f;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
