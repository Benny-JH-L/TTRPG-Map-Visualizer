using UnityEngine;
using UnityEngine.UI;

public class View : AbstractUI
{
    public ViewDataSO viewData;

    public GameObject leftContainer;
    public GameObject rightContainer;

    // images?

    private HorizontalLayoutGroup horizontalLayoutGroup;

    public override void Configure()
    {
        horizontalLayoutGroup.padding = viewData.padding;
        horizontalLayoutGroup.spacing = viewData.spacing;
    }

    public override void Setup()
    {
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        // images
    }
}
