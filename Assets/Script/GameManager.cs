using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LevelBuilder m_LevelBuilder;
    public GameObject m_NextButton;
    private bool readyForInput;
    private Player player;

    void Start()
    {
        m_LevelBuilder.Build();
        player = FindObjectOfType<Player>();
    }

    void Update() 
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.Normalize();

        if (input.sqrMagnitude > 0.5) 
        {
            if (readyForInput) 
            {
                readyForInput = false;
                player.Move(input);
                //nextButton.SetActive(IsLevelComplete());
            }
        }
        else 
        {
            readyForInput = true;
        }
    }

    public void NextLevel() 
    {
        m_NextButton.SetActive(false);
        m_LevelBuilder.NextLevel();
        StartCoroutine(ResetSceneAsync());
    }


    public void ResetScene() 
    {
        StartCoroutine(ResetSceneAsync());
        print("The button is working");
    }

    IEnumerator ResetSceneAsync() 
    {
        if (SceneManager.sceneCount > 1) {
            AsyncOperation async = SceneManager.UnloadSceneAsync("LevelScene");
            while (!async.isDone) {
                yield return null;
                Debug.Log("Unloading");
            }
            Debug.Log("Unloading done.");
            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone) {
            yield return null;
            Debug.Log("Loading");
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
        m_LevelBuilder.Build();
        player = FindObjectOfType<Player>();
        Debug.Log("Level loaded.");
    }
}
