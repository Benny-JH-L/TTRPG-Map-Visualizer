using UnityEngine;
using UnityEngine.UI;

public class HorizontalSplit1 : AbstractUI
{
    public ViewDataSO viewData;
    public GameObject left;     // rest of UI
    public GameObject right;    // Creature specific UI

    private Image rightImage;

    private HorizontalLayoutGroup horizontalLayoutGroup;

    public override void Setup()
    {
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        rightImage = right.GetComponent<Image>();
    }

    public override void Configure()
    {
        horizontalLayoutGroup.padding = viewData.padding;
        horizontalLayoutGroup.spacing = viewData.spacing;
    }

}
