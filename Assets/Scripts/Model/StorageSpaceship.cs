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
    public int purchaseCost;
    public bool purchased = false;

    public StorageSpaceship(
        string name,
        int healthPowerUpsCount,
        int attackPowerUpsCount,
        int purchaseCost,
        bool purchased
    ) {
        this.name = name;
        this.healthPowerUpsCount = healthPowerUpsCount;
        this.attackPowerUpsCount = attackPowerUpsCount;
        this.purchaseCost = purchaseCost;
        this.purchased = purchased;
    }

    private static StorageSpaceship[] Load()
    {
        string[] spaceshipTypes = (string[]) Enum.GetNames(typeof(SpaceshipType));
        StorageSpaceship[] spaceships = new StorageSpaceship[spaceshipTypes.Length];

        for (int i = 0; i < spaceshipTypes.Length; ++i) {
            spaceships[i] = new StorageSpaceship(
                spaceshipTypes[i],
                0,
                0,
                i * 100000,
                i == 0 ? true : false
            );
        }

        return spaceships;
    }

    public static void Write(StorageSpaceship spaceship)
    {
        StorageSpaceship[] spaceships = StorageSpaceship.GetAll();

        // search element in spaceships
        int i = StorageSpaceship.GetIndex(spaceship.name);
        spaceships[i] = spaceship;

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

    public static int GetIndex(string name)
    {
        StorageSpaceship[] spaceships = StorageSpaceship.GetAll();
        for (int i = 0; i < spaceships.Length; ++i) {
            if (spaceships[i].name == name) {
                return i;
            }
        }
        return -1;
    }

    public static StorageSpaceship Get(string name)
    {
        StorageSpaceship[] spaceships = StorageSpaceship.GetAll();
        int i = StorageSpaceship.GetIndex(name);
        return i != -1 ? spaceships[i] : null;
    }
}
