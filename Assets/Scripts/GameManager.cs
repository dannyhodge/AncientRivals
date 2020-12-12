using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject character;
    public List<GameObject> spawns;
    public List<string> characters;

    void Start()
    {
        var spawnsArray = GameObject.FindGameObjectsWithTag("Spawn");
        spawns.AddRange(spawnsArray);
        characters.Add("samuraired");
        characters.Add("templarred");
        characters.Add("templarblue");
        characters.Add("samuraiblue");
        if(!PhotonNetwork.IsConnected) {
            Instantiate(character, spawns[0].transform.position, character.transform.rotation);
        }
    }

    public Transform GetSpawnPoint(int order) {
        // var spawnIndex = Random.Range(0, spawns.Count);
        // var chosenSpawn = spawns[spawnIndex];
        // spawns.Remove(chosenSpawn);
        var chosenSpawn = spawns[order];
        return chosenSpawn.transform;
    }

    public string GetCharacterPrefabPath(int order) {
        // var characterIndex = Random.Range(0, characters.Count);
        // Debug.Log(characterIndex);
        // var chosenCharacter = characters[characterIndex];
        // Debug.Log(chosenCharacter);
        // characters.Remove(chosenCharacter);
        // Debug.Log("removed");
        var chosenCharacter = characters[order];
        return chosenCharacter;
    }
}
