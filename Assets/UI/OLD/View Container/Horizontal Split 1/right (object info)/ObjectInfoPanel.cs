using UnityEngine;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour
{
    public GameObject objectInfoPanelView;
    public GameObject generalObjectPanelPreset;
    public GameObject creaturePanelPreset;

    public void OnObjectSelected(Component comp, Object data)
    {
        if (data is Creature)
        {
            Creature creature = (Creature)data;
            
        }
    }
}
