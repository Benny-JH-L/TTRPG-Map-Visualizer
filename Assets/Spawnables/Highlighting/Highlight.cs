using System;
using UnityEngine;

[System.Serializable]
public class Highlight : MonoBehaviour
{
    private static string _debugStart = "Highlight class | ";
    // GameEventListeners are in GameObjects in the scene
    // GameEventListener<SpawnedObject> -??
    // GameEventListener<selectedObjectEvent>

    [Tooltip("Extra size in percent (0.1 = 10 % bigger)")]
    [Range(0f, 200f)]
    public float padding = 0.1f;

    private static GameObject highlightRingPrefab;
    private GameObject highlightRing;
    private GeneralObject selectedObject;

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
            selectedObject = (GeneralObject)data;
            highlightRing.SetActive(true);
            updatePosition();

            // TODO: Scale ring based on creature size
            //float radius = selectedCreature.GetRadius();  // radius of the disk is 2.5ft or 0.762cm
            //highlightRing.transform.localScale = Vector3.one * radius * 2f;
            ScaleRingToObject();
        }
    }

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
        highlightRing.transform.position = selectedObject.transform.position;
    }

    private void deactivateRing()
    {
        highlightRing.SetActive(false);
        selectedObject = null;
    }


}
