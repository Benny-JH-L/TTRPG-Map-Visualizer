using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.PackageManager;
using UnityEngine.Rendering.Universal.Internal;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public static int UNASSIGNED_INDEX = -1;
    
    public TabGroup tabGroup;
    
    public Image background;
    public Color originalColor;

    public int assignedIndex = UNASSIGNED_INDEX;

    public GameEventSO onTabSelected;
    public GameEventSO onTabDeselected;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    void Start()
    {
        background = GetComponent<Image>();     // or set manually
        originalColor = background.color;
        tabGroup.Subscribe(this);

        if (assignedIndex == UNASSIGNED_INDEX)
        {
            string msg = $"INDEX FOR TAB BUTTON IS `{UNASSIGNED_INDEX}`!";
            throw new System.Exception(msg);
        }
    }

    public void Select()
    {
        if (onTabSelected != null)
            onTabSelected.Raise(tabGroup, this);
    }

    public void Deselect()
    {
        if (onTabDeselected != null)
            onTabDeselected.Raise(tabGroup, this);
    }
}
