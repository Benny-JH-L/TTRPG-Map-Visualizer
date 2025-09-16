using UnityEngine;
using UnityEngine.UI;

public class VerticalSplit1 : AbstractUI
{
    public ViewDataSO viewData;

    private Image thisImage;

    public GameObject top;
    public GameObject bottom;

    private VerticalLayoutGroup verticalLayoutGroup;
    private LayoutElement layoutElement;

    public override void Setup()
    {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        layoutElement = GetComponent<LayoutElement>();
        thisImage = GetComponent<Image>();
    }

    public override void Configure()
    {
        verticalLayoutGroup.padding = viewData.padding;
        verticalLayoutGroup.spacing = viewData.spacing;

        layoutElement.flexibleHeight = viewData.flexibleHeight;
        layoutElement.flexibleWidth = viewData.flexibleWidth;
    }


}
