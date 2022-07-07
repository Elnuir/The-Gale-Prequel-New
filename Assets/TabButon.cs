using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButon : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    public Text text;
    public Image Background;

    public UnityEvent onTabSelected, onTabDeselected;
    // Start is called before the first frame update
    void Start()
    {
        text ??= GetComponent<Text>();
        Background ??= GetComponent<Image>();
        tabGroup.Subscride(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      tabGroup.OnTabExit(this);
    }

    public void Select()
    {
        if (onTabSelected != null)
        {
            onTabSelected.Invoke();
        }
    }

    public void Deselect()
    {
        if (onTabDeselected != null)
        {
            onTabDeselected.Invoke();
        }
    }
}
