using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slideshow : MonoBehaviour
{
    public List<GameObject> Items = new List<GameObject>();
    public GameObject SelectedItem => Items[_selectedIndex];
    public int InitSelectedIndex;
    public bool Looped;

    private int _selectedIndex;

    private void Start()
    {
        _selectedIndex = InitSelectedIndex;
        RefreshSelectedItem();
    }

    public void MoveNext()
    {
        if (_selectedIndex < Items.Count - 1)
        {
            _selectedIndex++;
            RefreshSelectedItem();
        }
        else if (Looped)
        {
            _selectedIndex = 0;
            RefreshSelectedItem();
        }
    }

    public void MovePrevious()
    {
        if (_selectedIndex > 0)
        {
            _selectedIndex--;
            RefreshSelectedItem();
        } 
        else if (Looped)
        {
            _selectedIndex = Items.Count - 1;
            RefreshSelectedItem();
        }
    }

    private void RefreshSelectedItem()
    {
        for (int i = 0; i < Items.Count; i++)
            Items[i].SetActive(i == _selectedIndex);
    }
}