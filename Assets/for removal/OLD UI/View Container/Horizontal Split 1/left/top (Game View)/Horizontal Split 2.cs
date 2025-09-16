using UnityEngine;
using UnityEngine.UI;

public class GameView : AbstractUI
{
    public ViewDataSO viewData;

    public GameObject leftSideButtonsUI;
    public GameObject rightSide;

    private Image thisImage;

    private HorizontalLayoutGroup horizontalLayoutGroup;
    private LayoutElement layoutElement;

    public override void Setup()
    {
        thisImage = GetComponent<Image>();
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        layoutElement = GetComponent<LayoutElement>();
    }

    public override void Configure()
    {
        horizontalLayoutGroup.padding = viewData.padding;
        horizontalLayoutGroup.spacing = viewData.spacing;

        layoutElement.flexibleHeight = viewData.flexibleHeight;
        layoutElement.flexibleWidth = viewData.flexibleWidth;
    }


}
