using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButon> tabButons;
    public Color tabIdle, tabHover, tabActive;
    public Color bgIdle, bgHover, bgActive;

    public TabButon selectedTab;
    public List<GameObject> objectsToSwap;

    private void Start()
    {
        ResetTabs();
    }

    public void Subscride(TabButon buton)
    {
        if (tabButons == null)
        {
            tabButons = new List<TabButon>();
        }

        tabButons.Add(buton);
    }

    public void OnTabEnter(TabButon button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            if (button.text)
                button.text.color = tabHover;
            if (button.Background)
                button.Background.color = bgHover;
        }
    }

    public void OnTabExit(TabButon buton)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButon buton)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = buton;
        selectedTab.Select();
        ResetTabs();

        if (buton.text)
            buton.text.color = tabActive;

        if (buton.Background)
            buton.Background.color = bgActive;
        int index = buton.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButon buton in tabButons)
        {
            if (selectedTab != null && buton == selectedTab)
            {
                if (buton.text)
                    buton.text.color = tabActive;

                if (buton.Background)
                    buton.Background.color = bgActive;
                continue;
            }

            if (buton.text)
                buton.text.color = tabIdle;

            if (buton.Background)
                buton.Background.color = bgIdle;
        }
    }
}