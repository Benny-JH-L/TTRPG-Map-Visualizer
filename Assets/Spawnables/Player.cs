using UnityEngine;

[System.Serializable]
// A wrapper for GameObjects
public class Player : Character
{
    public static new Player Create(Vector3 pos)
    {
        CharacterSaveData saveData = new()
        {
            // set default values -> from a json file prolly
            //creatureType = CreatureType.Player,
            ac = 10
            // ...
        };

        return Create(pos, saveData);
    }

    public static new Player Create(Vector3 pos, CharacterSaveData saveData)
    {
        Debug.Log("Creating player obj...");
        GameObject obj = Character.CreateGameObject(pos);
        Player player = obj.AddComponent<Player>();
        player.Init(obj, saveData); // set values

        Character.gameData.playerList.Add(player);

        //CreatureSaveData s = new CreatureSaveData();
        //s.ac = 1000;
        //obj.GetComponent<Player>().Init(obj, s);
        //Creature.gameData.playerList.Add(obj.GetComponent<Player>());
        //Debug.Log("returning player gameobject");

        Debug.Log("Player obj created...");

        return player;
    }
}
