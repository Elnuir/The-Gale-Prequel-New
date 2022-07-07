using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadGameDisabler : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = PlayerPrefs.HasKey("full-score");
    }

    // Update is called once per frame
    void Update()
    {
    }
}