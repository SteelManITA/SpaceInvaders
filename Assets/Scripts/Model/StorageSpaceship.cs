using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StorageSpaceship
{
    public string name;
    public int healthPowerUpsCount;
    public int attackPowerUpsCount;
    public int healthPowerUpsMax = 10;
    public int attackPowerUpsMax = 10;

    public StorageSpaceship(
        string name,
        int healthPowerUpsCount,
        int attackPowerUpsCount
    ) {
        this.name = name;
        this.healthPowerUpsCount = healthPowerUpsCount;
        this.attackPowerUpsCount = attackPowerUpsCount;
    }

    private static StorageSpaceship[] Load()
    {
        string[] spaceshipTypes = (string[]) Enum.GetNames(typeof(SpaceshipType));
        StorageSpaceship[] spaceships = new StorageSpaceship[spaceshipTypes.Length];

        for (int i = 0; i < spaceshipTypes.Length; ++i) {
            spaceships[i] = new StorageSpaceship(
                spaceshipTypes[i],
                0,
                0
            );
        }

        return spaceships;
    }

    public static void Write(StorageSpaceship spaceship)
    {
        StorageSpaceship[] spaceships = StorageSpaceship.GetAll();

        // search element in spaceships
        for (int i = 0; i < spaceships.Length; ++i) {
            if (spaceships[i].name == spaceship.name) {
                spaceships[i] = spaceship;
                break;
            }
        }

        // replace element in spaceships
        string json = JsonHelper.ToJson(spaceships, true);

        PlayerPrefs.SetString("Spaceships", json);
        PlayerPrefs.Save();
    }

    public static StorageSpaceship[] GetAll()
    {
        string spaceshipsString = PlayerPrefs.GetString("Spaceships", "");

        if (spaceshipsString == "") {
            return Load();
        }

        return JsonHelper.FromJson<StorageSpaceship>(spaceshipsString);
    }
}
