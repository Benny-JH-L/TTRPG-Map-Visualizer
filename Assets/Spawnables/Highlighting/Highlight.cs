using System;
using UnityEngine;

[System.Serializable]
public class Highlight : MonoBehaviour
{
    private static string _debugStart = "Highlight class | ";
    // GameEventListeners are in GameObjects in the scene
    // GameEventListener<SpawnedObject> -??
    // GameEventListener<selectedObjectEvent>

    private static GameObject highlightRingPrefab;
    private GameObject highlightRing;
    private GeneralObject selectedCreature;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highlightRing = Instantiate(highlightRingPrefab);
        deactivateRing();
    }

    // Update is called once per frame
    //void LateUpdate()   // used temp, will switch to an event based system, where if the selected creature moves, the highlight moves too, instead of wasting resources checking this
    //{
    //    if (highlightRing.activeSelf && selectedCreature != null)
    //        updatePosition();
    //}

    public static void Initialize(GameObject prefab)
    {
        highlightRingPrefab = prefab;
    }

    public void HighlightObject(Component comp, object data)
    {
        if (data == null)
        {
            Debug.Log(_debugStart + "this part of the code should thearetically shouyldn't be called");
            deactivateRing();
        }
        else if (data is GeneralObject)   // will also need to resize the highlight ring to encompass the object!
        {
            Debug.Log(_debugStart + "Highlighting GeneralObject (Selected object event)");
            selectedCreature = (GeneralObject)data;
            highlightRing.SetActive(true);
            updatePosition();

            // TODO: Scale ring based on creature size
            //float radius = selectedCreature.GetRadius();  // radius of the disk is 2.5ft or 0.762cm
            //highlightRing.transform.localScale = Vector3.one * radius * 2f;
        }
    }

    public void OnObjectMove(Component comp, object data)
    {
        if (data == null)   // no game object has been selected
            return;
        updatePosition();

        // note: could update using the information in `data` it will contain Tuple<GameObject, Vector3>.
        // // where Gameobject is the selected object (which we already know) and Vector3 is the moved amount
        // // (which we can infer from selected object's transform.position)
    }

    public void OnDeselectObject(Component comp, object data)
    {
        Debug.Log(_debugStart + "dehighlighting GeneralObject (deselect object event)");
        deactivateRing();
    }

    private void updatePosition()
    {
        highlightRing.transform.position = selectedCreature.transform.position;
    }

    private void deactivateRing()
    {
        highlightRing.SetActive(false);
        selectedCreature = null;
    }


}
