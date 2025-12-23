using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.PackageManager;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public static int UNASSIGNED_INDEX = -1;
    public TabGroup tabGroup;

    public Image background;

    public int assignedIndex = UNASSIGNED_INDEX;

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
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);

        if (assignedIndex == UNASSIGNED_INDEX)
        {
            string msg = $"INDEX FOR TAB BUTTON IS `{UNASSIGNED_INDEX}`!";
            throw new System.Exception(msg);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
