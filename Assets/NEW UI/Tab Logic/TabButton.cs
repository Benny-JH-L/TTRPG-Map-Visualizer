using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public static int UNASSIGNED_INDEX = -1;    // wont be used
    
    public TabGroup tabGroup;
    public GameObject swapToObject;     // when this tab button is selected (clicked), this object should be switched too
    
    public Image background;
    public Color originalColor;

    public int assignedIndex = UNASSIGNED_INDEX;    // wont be used

    public GameEventSO onTabSelected;
    public GameEventSO onTabDeselected;

    void Start()
    {
        if (swapToObject == null)
        {
            ErrorOut.Throw(this, "swapToObj null");
        }
        if (assignedIndex == UNASSIGNED_INDEX)
        {
            ErrorOut.Throw(this, $"INDEX FOR TAB BUTTON IS `{UNASSIGNED_INDEX}` (unassigned)!");
        }

        background = GetComponent<Image>();     // or set manually
        originalColor = background.color;
        tabGroup.Subscribe(this);
    }

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
