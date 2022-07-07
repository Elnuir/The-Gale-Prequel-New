using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSizeSlider : MonoBehaviour
{
    public Text Output;
    public Slider Slider;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        Slider.onValueChanged.AddListener(HandleValueChanged);
    }

    void HandleValueChanged(float value)
    {
        Output.text = value.ToString();
        if (!_player)
            _player = GameObject.FindWithTag("Player");
        else
            _player.transform.localScale = new Vector3(Slider.value, Slider.value, 1);
    }
}