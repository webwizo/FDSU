using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : Singleton<LoadingScreen>
{

    public Text m_LoadingProgress;
    public Image m_Icon;
    public float m_RotateSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_Icon.gameObject.transform.Rotate(Vector3.back, m_RotateSpeed * Time.deltaTime);
    }
}
