
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[DefaultExecutionOrder(-999)]
public class View_OLD : AbstractUI
{
    public static string _debugStart = "View | ";
    public ViewDataSO viewData;

    public GameObject leftContainer;
    public List<GameObject> rightContainers;

    // these UI panels will be on the right hand side of the screen
    public GameObject creatureUIPanel;  
    public GameObject inanimateUIPanel;

    public GameObject actionContainer;
    public GameObject actionPanelPrefab;


    // images?

    private HorizontalLayoutGroup horizontalLayoutGroup;

    public override void Configure()
    {
        horizontalLayoutGroup.padding = viewData.padding;
        horizontalLayoutGroup.spacing = viewData.spacing;

        rightContainers = new List<GameObject> { creatureUIPanel, inanimateUIPanel };
        //rightContainers.Add(creatureUIPanel);
        //rightContainers.Add(inanimateUIPanel);
    }

    public override void Setup()
    {
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        //GameObject[] panels = GetComponentsInChildren<GameObject>(); // probably need to add scripts to those panels and then get that script?
        //leftContainer = panels[0];
        //creatureUIPanel = panels[1];
        //inanimateUIPanel = panels[2];


        // images
    }

    
    public void OnSelectedObject(Component comp, object data)
    {
        //if (data is Creature_OLD creature)  // activate Creature UI panel
        //{
        //    Debug.Log($"{_debugStart}selected a Creature");
        //    _DeActivateAllRightContainers();
        //    creatureUIPanel.SetActive(true);
        //}
        //else if (data is InanimateObject_OLD inanimateObj)  // activate InanimateObject UI panel
        //{
        //    Debug.Log($"{_debugStart}selected an InanimateObject");
        //    _DeActivateAllRightContainers();
        //    inanimateUIPanel.SetActive(true);
        //}
        //else // debug
        //{
        //    Debug.Log($"{_debugStart}IDK WHAT U SELECTED AHHHHHH!!!!!");

        //}
    }

    /// <summary>
    /// Deactivates all panels (GameObjects) inside the List of GameObject `right containers`.
    /// </summary>
    private void _DeActivateAllRightContainers()
    {
        Debug.Log($"{_debugStart}deactivating all right containers.");
        foreach (GameObject panel in rightContainers)
        {
            panel.SetActive(false);
        }
    }

    public void OnDeselectObject(Component comp, object data)
    {
        Debug.Log($"{_debugStart}Removing right container");
        _DeActivateAllRightContainers();
    }

    public void OnGeneralObjectDestroyed(Component comp, object data)
    {
        _DeActivateAllRightContainers();
    }
}
