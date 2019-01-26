using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button StartButton;
    public Button OptionsButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(HandleStartButton);
        OptionsButton.onClick.AddListener(HandleOptionsButton);
    }

    private void HandleOptionsButton()
    {
        Debug.Log("Options Button");
    }

    void HandleStartButton()
    {
        Debug.Log("Start Button");
         GameManager.Instance.StartGame();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
