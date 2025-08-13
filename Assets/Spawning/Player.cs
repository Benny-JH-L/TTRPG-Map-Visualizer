using UnityEngine;

[System.Serializable]
// A wrapper for GameObjects
public class Player : Creature
{
    //public Player(Vector3 pos) : base(CreatureType.Player, pos)
    //{
    //    //disk.AddComponent<Player>();
    //}

    //public static Player Create(Vector3 pos)
    //{
    //    Debug.Log("Creating player");
    //    return new Player(pos);
    //}

    //public static Player Create(Vector3 pos)
    //{
    //    Debug.Log("Creating player");
    //    return gameObject.AddComponent<Player>().disk;
    //}

    public static Player Create(Vector3 pos)
    {
        return Create(pos, new CreatureSaveData());
    }

    public static Player Create(Vector3 pos, CreatureSaveData saveData)
    {
        Debug.Log("Creating player obj...");
        GameObject obj = Creature.CreateGameObject(CreatureType.Player, pos);
        Player player = obj.AddComponent<Player>();
        player.Init(obj);

        Creature.gameData.playerList.Add(player);


        //CreatureSaveData s = new CreatureSaveData();
        //s.ac = 1000;
        //obj.GetComponent<Player>().Init(obj, s);
        //Creature.gameData.playerList.Add(obj.GetComponent<Player>());
        //Debug.Log("returning player gameobject");

        Debug.Log("Player obj created...");

        return player;
    }

    public void Init(GameObject playerObj)
    {
        Debug.Log("Player init");
        base.Init(playerObj, saveData);
    }

}
