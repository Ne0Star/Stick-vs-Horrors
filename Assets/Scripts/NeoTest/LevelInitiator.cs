using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Отвечает за инициализацию уровня
/// </summary>
public class LevelInitiator : MonoBehaviour
{


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SwitchScene(int index)
    {
        StartCoroutine(OpenSceneAsyncSingle(index));
    }
    public IEnumerator OpenSceneAsyncSingle(int index)
    {
        AsyncOperation time = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        Scene scene = gameObject.scene;
        while (!time.isDone)
        {
                float progress = Mathf.Clamp01(time.progress / 1.05f);
            Debug.Log("Load... " + progress);
            yield return new WaitForEndOfFrame();
        }
    }

}
