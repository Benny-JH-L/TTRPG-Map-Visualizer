using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class TTRPG_SceneObjectBase : MonoBehaviour
{
    //int id;
    public static GameData gameData;    // static for now

    public GameObject appearanceGameObj;  // the look of the object
    public GameObject diskBase;           // base the `appearanceGameObj` is on top of
    public bool debugDisabled = false;

    public abstract GeneralObjectData GetData { get; }  // ensures that every `TTRPG_SceneObjectBase` has a `GeneralObjectData` or its subclass.
}

public abstract class TTRPG_SceneObject<T> : TTRPG_SceneObjectBase
    where T : GeneralObjectData
{
    public T data;
    public override GeneralObjectData GetData => data;  // returns the specific `GeneralObjectData`


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DebugOut.Log(this, " - Start() - START", debugDisabled);
        ConfirmInit();
        DebugOut.Log(this, "- Start() - END", debugDisabled);
    }

    protected private abstract void OnDestroy();

    /// <summary>
    /// Call this function to confirm values were all initialized and set properly.
    /// </summary>
    public void ConfirmInit()
    {
        bool fail = false;

        if (appearanceGameObj == null)
        {
            ErrorOut.Throw(this, "`appearanceGameObj` cannot be null");
            fail = true;
        }

        if (diskBase == null)
        {
            ErrorOut.Throw(this, "`diskBase` cannot be null");
            fail = true;
        }

        if (data == null)
        {
            ErrorOut.Throw(this, "`data` cannot be null");
            fail = true;
        }

        if (!appearanceGameObj.transform.IsChildOf(diskBase.transform))
        {
            // if somehow the transform is not parented, fix it
            DebugOut.Log(this, "`appearanceGameObj` transform was not child of `diskBase`, it has been fixed", debugDisabled);
            appearanceGameObj.transform.SetParent(diskBase.transform, true);
        }

        if (!diskBase.transform.IsChildOf(this.transform))
        {
            // if somehow the transform is not parented, fix it
            DebugOut.Log(this, $"`diskBase` transform was not child of `{this.GetType()}`, it has been fixed", debugDisabled);
            diskBase.transform.SetParent(this.transform, true);
        }

        // set RigidBody values
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.mass = 1.0f;
        rb.linearDamping = 0.9f;
        rb.angularDamping = 0.05f;
        rb.automaticCenterOfMass = true;
        rb.automaticInertiaTensor = true;
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;

        if (!fail)
            DebugOut.Log(this, $"Successfully initialized this `TTRPG_SceneObject` with data `{data.GetType()}`...", debugDisabled);
    }

    /// <summary>
    /// Ensure this is called for subclasses.
    /// </summary>
    protected void DestroySelf()
    {
        Destroy(appearanceGameObj);
        Destroy(diskBase);
    }
}
