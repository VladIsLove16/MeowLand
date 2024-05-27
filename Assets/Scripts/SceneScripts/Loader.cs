using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public static class Loader
{
    private class LoadingMonoBehaviour : MonoBehaviour { }
    private static AsyncOperation loadingAsyncOperation;
    public enum Scene
    {
        Game,
        Loading,
        MainMenu
    }
    private static Action onLoaderCallback;
    public static void Load(Scene scene)
    {
        onLoaderCallback = () =>
        {
            GameObject loadingGO = new GameObject("loadingGO");
            loadingGO.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
            
        };
        SceneManager.LoadScene(Scene.Loading.ToString());
    }
    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;
        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }
    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation == null) return 1f;
        return loadingAsyncOperation.progress;
    }
    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
