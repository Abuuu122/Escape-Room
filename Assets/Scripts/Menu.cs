using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Canvas MenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        MenuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        // ��ȡ��ǰ���������
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ���س���
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void Quit()
    {
        SceneManager.LoadScene("StartScene");
    }
}
