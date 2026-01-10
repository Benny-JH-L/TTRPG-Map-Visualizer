using UnityEngine;

public class DiskBaseSpawner : SpawnerBase<TTRPG_SceneObjectBase>
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

    public GameObject Spawn(Vector3 position)
    {
        return BaseSpawn(null, position);
    }
}
