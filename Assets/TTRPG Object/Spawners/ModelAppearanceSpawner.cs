using UnityEngine;

public class ModelApperanceSpawner : SpawnerBase<TTRPG_SceneObjectBase>
{
    protected override void Setup()
    {
        // nothing...
    }

    protected override void Configure()
    {
        // nothing...
    }

    protected override void OnStart()
    {
        // nothing...
    }

    protected override void DoAfterSpawn(GameData gameData, GameObject gameObj)
    {
        // don't do anything
    }

    public GameObject Spawn(Vector3 position, GameObject appearancePrefab)
    {
        return BaseSpawn(null, appearancePrefab, position);    // will not utilize its `prefab to spawn` var
        // logic to scale appearance/position it on top of disk base
    }
}
