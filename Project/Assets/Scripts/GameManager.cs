using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }

public class GameManager : Singleton<GameManager>
{

    [System.Serializable] public class EventGameState : UnityEvent<GameState, GameState> { }

    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    public GameObject[] m_SystemPrefabs;
    public EventGameState OnGameStateChanged;

    List<GameObject> m_InstancedSystemPrebas;
    List<AsyncOperation> m_LoadOperation;
    GameState m_CurrentGameState = GameState.PREGAME;

    string m_CurrentLevelName = string.Empty;

    public GameState CurrentGameState
    {
        get { return m_CurrentGameState; }
        private set { m_CurrentGameState = value; }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        m_InstancedSystemPrebas = new List<GameObject>();
        m_LoadOperation = new List<AsyncOperation>();

        InstantiateSystemPrefabs();
        // StartGame();

        LoadingScreen.Instance.gameObject.SetActive(false);
        Debug.Log(m_CurrentGameState);
    }

    // Update is called once per frame
    void Update()
    {

        if (m_CurrentGameState == GameState.PREGAME)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    void OnLoadOperatoinComplete(AsyncOperation asyncOperation)
    {
        if (m_LoadOperation.Contains(asyncOperation))
        {
            m_LoadOperation.Remove(asyncOperation);

            if (m_LoadOperation.Count == 0)
            {
            //    UpdateGameState(GameState.RUNNING);
            }
        }
        Debug.Log("Load completed");
    }

    void OnUnloadOperatoinComplete(AsyncOperation asyncOperation)
    {
        Debug.Log("Unload completed");
    }

    void UpdateGameState(GameState state)
    {
        GameState previousGameState = m_CurrentGameState;
        m_CurrentGameState = state;

        switch (m_CurrentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;

            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;

            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(m_CurrentGameState, previousGameState);
    }

    void InstantiateSystemPrefabs()
    {

        GameObject prefabInstance;
        for (int i = 0; i < m_SystemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(m_SystemPrefabs[i]);
            m_InstancedSystemPrebas.Add(prefabInstance);
        }
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadSceneWithLoading(levelName));
        //AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        //if (asyncOperation == null)
        //{
        //    Debug.LogError("[GameManager] Load to load level " + levelName);
        //    return;
        //}
        //asyncOperation.completed += OnLoadOperatoinComplete;
        //m_LoadOperation.Add(asyncOperation);
        //m_CurrentLevelName = levelName;
    }

    IEnumerator LoadSceneWithLoading(string levelName)
    {
        yield return null;
        LoadingScreen.Instance.gameObject.SetActive(true);
        //yield return new WaitForSeconds(3);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (asyncOperation == null)
        {
            Debug.LogError("[GameManager] Load to load level " + levelName);
            yield return null;
        }
        asyncOperation.completed += OnLoadOperatoinComplete;
        m_LoadOperation.Add(asyncOperation);
        m_CurrentLevelName = levelName;

        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            LoadingScreen.Instance.m_LoadingProgress.text = (asyncOperation.progress * 100) + "%";

            if (asyncOperation.progress >= 0.9f)
            {
                LoadingScreen.Instance.gameObject.SetActive(false);
                UIManager.Instance.gameObject.SetActive(false);
                asyncOperation.allowSceneActivation = true;

            }

            yield return null;
        }

    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName);
        if (asyncOperation == null)
        {
            Debug.LogError("[GameManager] Unload to load level " + levelName);
            return;
        }
        asyncOperation.completed += OnUnloadOperatoinComplete;
        m_CurrentLevelName = levelName;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < m_InstancedSystemPrebas.Count; i++)
        {
            Destroy(m_InstancedSystemPrebas[i]);
        }
        m_InstancedSystemPrebas.Clear();
    }

    public void StartGame()
    {
        LoadLevel("Level_01");
    }

    public void TogglePause()
    {
        Debug.Log("Escape Initialized Pause Menu");
        UpdateGameState(
            m_CurrentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING
        );
    }
}