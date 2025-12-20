using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "TTRPG_TextSO", menuName = "Scriptable Objects/TTRPG_TextSO")]
public class TTRPG_TextSO : ScriptableObject
{
    public TMP_FontAsset font;
    public float fontSize;
}
