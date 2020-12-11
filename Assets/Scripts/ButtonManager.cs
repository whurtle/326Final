using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public string CurrentScene;
    public string NextScene;

    public Button nextLevel;

    public Button restartLevel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowNextLevelButton()
    {
        nextLevel.gameObject.SetActive(true);
    }

    public void ShowRestartLevelButtoN()
    {
        restartLevel.gameObject.SetActive(true);
    }

    public void LoadScene()
    {
        Loader.Load(NextScene);
    }

    public void Restart()
    {
        Loader.Load(CurrentScene);
    }
}
