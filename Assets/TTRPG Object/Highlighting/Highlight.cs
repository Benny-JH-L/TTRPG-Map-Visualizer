using System;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Highlight : MonoBehaviour
{
    //private static string _debugStart = "Highlight class | ";
    // GameEventListeners are in GameObjects in the scene
    // GameEventListener<SpawnedObject> -??
    // GameEventListener<selectedObjectEvent>

    [Tooltip("Extra size in percent (0.1 = 10 % bigger)")]
    [Range(0f, 200f)]
    public float padding = 0.1f;

    [SerializeField] private GameObject highlightRingPrefab;    // prefab we instantiate from
    [SerializeField] private GameObject highlightRing;          // object used to `hightlight`
    //[SerializeField] private TTRPG_SceneObjectBase selectedObject;
    public bool debugDisabled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (highlightRingPrefab == null)
            ErrorOut.Throw(this, "highlightRingPrefab cannot be null");

        highlightRing = Instantiate(highlightRingPrefab);
        DeactivateRing();
    }

    // Update is called once per frame
    //void LateUpdate()   // used temp, will switch to an event based system, where if the selected creature moves, the highlight moves too, instead of wasting resources checking this
    //{
    //    if (highlightRing.activeSelf && selectedCreature != null)
    //        updatePosition();
    //}

    //public static void Initialize(GameObject prefab)
    //{
    //    highlightRingPrefab = prefab;
    //}

    public void SelectedObjectChanged(Component comp, object data)
    {
        if (data is ChangedObject changedObject)
        {
            TTRPG_SceneObjectBase selectedObj = changedObject.newSelectedObj;
            // dehighlight when `newSelectedObj` is null
            if (selectedObj == null)
            {
                DebugOut.Log(this, "dehighlighting TTRPG_SceneObjectBase", debugDisabled);
                DeactivateRing();
            }
            // highlight the new object
            else
            {
                DebugOut.Log(this, "highlighting TTRPG_SceneObjectBase", debugDisabled);
                HighlightObject(selectedObj);
            }
        }
        else
        {
            WarningOut.Log(this, " - OnSelectedObjectChanged() - data is not ChangedObject");
        }
    }

    private void HighlightObject(TTRPG_SceneObjectBase sceneObj) 
    {

        DebugOut.Log(this, $"highlighting TTRPG_SceneObjectBase at position: {sceneObj.transform.position}", debugDisabled);

        // set the parent transform of highlight ring to `sceneObj`
        highlightRing.transform.SetParent(sceneObj.transform, false);   // `false` so it moves to the new position
        highlightRing.SetActive(true);
        

        //if (data is TTRPG_SceneObjectBase sceneObj)
        //{
        //    DebugOut.Log(this, $"highlighting TTRPG_SceneObjectBase at position: {sceneObj.transform.position}");

        //    // set the parent transform of highlight ring to `data`
        //    highlightRing.transform.SetParent(sceneObj.transform, false);   // `false` so it moves to the new position
        //    highlightRing.SetActive(true);
        //}


        //if (data == null)
        //{
        //    DebugPrinter.printMessage(this, "this part of the code should thearetically shouyldn't be called");
        //    deactivateRing();
        //}
        //else if (data is GeneralObject_OLD)   // will also need to resize the highlight ring to encompass the object!
        //{
        //    Debug.Log(_debugStart + "Highlighting GeneralObject (Selected object event)");
        //    selectedObject = (GeneralObject_OLD)data;
        //    highlightRing.SetActive(true);
        //    updatePosition();

        //    // TODO: Scale ring based on creature size
        //    //float radius = selectedCreature.GetRadius();  // radius of the disk is 2.5ft or 0.762cm
        //    //highlightRing.transform.localScale = Vector3.one * radius * 2f;
        //    ScaleRingToObject();
        //}
    }

    //public void Dehighlight()
    //{
    //    DebugOut.Log(this, "dehighlighting TTRPG_SceneObjectBase");
    //    DeactivateRing();
    //}

    private void ScaleRingToObject()
    {
        // neither `work` from my self imports of hexagons -> may need to generate new models using a unit scale... (or find how much 1 unit is from unity to blender)

        // 1. Get the mesh that the collider uses
        //MeshCollider mc = selectedObject.GetComponent<MeshCollider>();
        //if (mc == null || mc.sharedMesh == null)
        //{
        //    Debug.LogError("Disk needs a MeshCollider with a mesh assigned", this);
        //    return;
        //}
        //Mesh mesh = mc.sharedMesh;

        //// 2. Figure out how big the disk is in world space
        //Vector3 meshSize = mesh.bounds.size;                 // local size
        //Vector3 lossyDisk = selectedObject.transform.lossyScale;        // world scale
        //Vector3 worldSize = Vector3.Scale(meshSize, lossyDisk);

        //// 3. Compute the scale that would make obj1 match that world size
        //Vector3 targetLocal = Vector3.Scale(worldSize, transform.worldToLocalMatrix.lossyScale);

        //// 4. Add padding and apply
        //transform.localScale = targetLocal * (1f + padding);
    




    //if (selectedObject == null || highlightRing == null)
    //    return;

    //// Move ring under the selected object
    ////highlightRing.transform.position = selectedObject.transform.position;

    //// Get collider bounds
    //MeshCollider meshCol = selectedObject.GetComponent<MeshCollider>();
    //if (meshCol != null)
    //{
    //    Bounds bounds = meshCol.bounds;

    //    // Get max dimension in XZ plane (since ring is flat on ground)
    //    float maxSize = Mathf.Max(bounds.size.x, bounds.size.z);

    //    // Add a small buffer so the ring is bigger than the object
    //    float scaleFactor = maxSize * 1.1f;

    //    // Since your ring is probably modeled with a unit scale (diameter ~1),
    //    // scale uniformly in X and Z
    //    highlightRing.transform.localScale = new Vector3(scaleFactor, 1f, scaleFactor);
    //}


    //if (selectedObject == null || highlightRing == null) return;

    //// Get the mesh collider bounds directly
    //MeshCollider meshCollider = selectedObject.GetComponent<MeshCollider>();

    //if (meshCollider == null)
    //{
    //    Debug.LogWarning(_debugStart + "No MeshCollider found on selected object");
    //    highlightRing.transform.localScale = Vector3.one;
    //    return;
    //}

    //Bounds bounds = meshCollider.bounds;

    //// Calculate the maximum horizontal dimension (width or depth) for circular scaling
    //float maxDimension = Mathf.Max(bounds.size.x, bounds.size.z);

    //Debug.Log($"{_debugStart}maxDimension: {maxDimension}");

    //// Add a small buffer (10% larger than the object)
    //float scaleFactor = maxDimension * 1.1f;

    //// Apply the scale to the highlight ring
    //highlightRing.transform.localScale = Vector3.one * scaleFactor;
}



    private void DeactivateRing()
    {
        highlightRing.SetActive(false);
        highlightRing.transform.parent = null;
        highlightRing.transform.position = Vector3.zero;    // reset position (or else it will stay in place and keep the old position of parent)
    }


}
