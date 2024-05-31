using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WinMenu : MonoBehaviour
{
    void Start()
    {
        DebugUIBuilder.instance.AddLabel("Select Sample Scene");

        int n = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < n; ++i)
        {
            string path = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
            var sceneIndex = i;
            DebugUIBuilder.instance.AddButton(Path.GetFileNameWithoutExtension(path), () => LoadScene(sceneIndex));
        }

        DebugUIBuilder.instance.Show();
    }

    void LoadScene(int idx)
    {
        DebugUIBuilder.instance.Hide();
        Debug.Log("Load scene: " + idx);
        UnityEngine.SceneManagement.SceneManager.LoadScene(idx);
    }
}
