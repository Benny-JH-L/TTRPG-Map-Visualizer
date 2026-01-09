using UnityEngine;

/// <summary>
/// Wrapper class that contains creature data instead of GeneralObjectData and to differentiate.
/// </summary>
public class Creature : TTRPG_SceneObject<CreatureData>
{
    private protected override void OnDestroy()
    {
        DebugOut.Log(this, $"`{this.name}` was destroyed");
        gameData.creatureList.Remove(this);
        gameData.sceneObjectList.Remove(this);

        DestroySelf();
        //could have gamevent that Creature can call when Destroyed, 
        //and a listener for it where it will remove the Creature from the list in GameData
    }
}
