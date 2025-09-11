using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "TextDataSO", menuName = "Scriptable Objects/TextDataSO")]
public class TextDataSO : ScriptableObject
{
    public TMP_FontAsset font;
    public float size;
}
