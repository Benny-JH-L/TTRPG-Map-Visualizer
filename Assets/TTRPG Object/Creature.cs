using UnityEngine;

/// <summary>
/// Wrapper class that contains creature data instead of GeneralObjectData and to differentiate.
/// </summary>
public class Creature : TTRPG_SceneObject<CreatureData>
{
    private protected override void OnDestroy()
    {
        DebugPrinter.printMessage(this, $"`{this.name}` was destroyed");
        gameData.creatureList.Remove(this);
        gameData.sceneObjectList.Remove(this);

        DestroySelf();
    }
}
