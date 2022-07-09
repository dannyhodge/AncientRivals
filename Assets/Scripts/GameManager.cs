﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        Instantiate(character, spawns[0].transform.position, character.transform.rotation);
        
    }

    public Transform GetSpawnPoint(int order) {
        var chosenSpawn = spawns[order];
        return chosenSpawn.transform;
    }

    public string GetCharacterPrefabPath(int order) {
        var chosenCharacter = characters[order];
        return chosenCharacter;
    }
}
