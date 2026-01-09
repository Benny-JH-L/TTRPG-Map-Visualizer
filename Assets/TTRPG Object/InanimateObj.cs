using UnityEngine;

public class InanimateObj : TTRPG_SceneObject<GeneralObjectData>
{
    private protected override void OnDestroy()
    {
        DebugPrinter.printMessage(this, $"`{this.name}` was destroyed");
        gameData.sceneObjectList.Remove(this);

        DestroySelf();
    }
}
