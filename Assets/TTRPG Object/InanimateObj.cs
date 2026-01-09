using UnityEngine;

public class InanimateObj : TTRPG_SceneObject<GeneralObjectData>
{
    private protected override void OnDestroy()
    {
        DebugOut.Log(this, $"`{this.name}` was destroyed");
        gameData.sceneObjectList.Remove(this);

        DestroySelf();
        //could have gamevent that InanimateObj can call when Destroyed, 
        //and a listener for it where it will remove the InanimateObj from the list in GameData
    }
}
