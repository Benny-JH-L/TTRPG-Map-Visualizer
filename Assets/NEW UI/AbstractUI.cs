using UnityEngine;

//[DefaultExecutionOrder(-999)]
public abstract class AbstractUI : MonoBehaviour
{
    public bool debugDisabled = false;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        Setup();
        Configure();
    }

    public abstract void Setup();
    public abstract void Configure();

    private void OnValidate()
    {
        Init();
    }


}