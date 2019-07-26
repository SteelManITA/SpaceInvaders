using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopController : MonoBehaviour
{
    private const int BASE_POWERUP_COST = 5000;
    private enum Property {
        Health,
        Attack
    }
    private StorageSpaceship[] spaceships;
    private int currentIndex;

    public Button next;
    public Button prev;
    public RectTransform spaceshipImage;
    public Text spaceshipName;
    public Button selectSpaceship;

    public GameObject detailsContainer;
    public GameObject purchaseContainer;
    public Button purchase;

    public Text healthPrice;
    public Button boostHealth;
    public Text attackPrice;
    public Button boostAttack;

    public RectTransform healthIndicatorContainer;
    public RectTransform attackIndicatorContainer;
    public GameObject powerUpIndicator;

    private int calcPowerupCost(int level)
    {
        return (int) Mathf.Pow(2, level) * BASE_POWERUP_COST;
    }

    private void UpdateView()
    {
        StorageSpaceship spaceship = this.spaceships[this.currentIndex];

        this.DisableButtons();

        this.spaceshipName.text = spaceship.name;
        this.healthPrice.text = this.calcPowerupCost(spaceship.healthPowerUpsCount).ToString("N0");
        this.attackPrice.text = this.calcPowerupCost(spaceship.attackPowerUpsCount).ToString("N0");
        this.purchase.GetComponentInChildren<Text>().text = spaceship.purchaseCost.ToString("N0");

        this.FillPowerUpIndicator(
            this.healthIndicatorContainer,
            spaceship.healthPowerUpsCount,
            spaceship.healthPowerUpsMax
        );
        this.FillPowerUpIndicator(
            this.attackIndicatorContainer,
            spaceship.attackPowerUpsCount,
            spaceship.attackPowerUpsMax
        );
    }

    private void FillPowerUpIndicator(RectTransform spawnPoint, int active, int total)
    {
        for (int i = 0; i < total; ++i) {
            float spawnX = i * (5 + 1);
            Vector3 pos = new Vector3(spawnX, 0, 0);

            GameObject spawnedItem = Instantiate(this.powerUpIndicator);
            spawnedItem.transform.position = pos;
            spawnedItem.transform.SetParent(spawnPoint, false);

            if (i > active) {
                spawnedItem.GetComponent<Image>().color = new Color(200, 200, 200);
            }
        }
    }

    private void DisableButtons()
    {
        StorageSpaceship spaceship = this.spaceships[this.currentIndex];

        this.next.enabled = this.currentIndex != this.spaceships.Length - 1;
        this.prev.enabled = this.currentIndex != 0;

        this.selectSpaceship.enabled =
            spaceship.name != PlayerPrefs.GetString(
                "SpaceshipType",
                Enum.GetName(
                    typeof(SpaceshipType),
                    SpaceshipType.SpaceShooter
                )
            );
        this.selectSpaceship.GetComponentInChildren<Text>().text = this.selectSpaceship.enabled ? "Use this Spaceship" : "Already in use";
        {
            this.selectSpaceship.GetComponent<CanvasGroup>().alpha =
                this.detailsContainer.GetComponent<CanvasGroup>().alpha = spaceship.purchased ? 1f : 0f;
            this.purchaseContainer.GetComponent<CanvasGroup>().alpha = !spaceship.purchased ? 1f : 0f;

            this.selectSpaceship.GetComponent<CanvasGroup>().blocksRaycasts =
                this.detailsContainer.GetComponent<CanvasGroup>().blocksRaycasts = spaceship.purchased;
            this.purchaseContainer.GetComponent<CanvasGroup>().blocksRaycasts = !spaceship.purchased;
        }

        this.boostHealth.enabled = spaceship.healthPowerUpsCount != spaceship.healthPowerUpsMax;
        this.boostAttack.enabled = spaceship.attackPowerUpsCount != spaceship.attackPowerUpsMax;
    }

    private void OnNextClick()
    {
        ++this.currentIndex;
        this.UpdateView();
    }

    private void OnPrevClick()
    {
        --this.currentIndex;
        this.UpdateView();
    }

    private void OnSelectSpaceshipClick()
    {
        PlayerPrefs.SetString("SpaceshipType", this.spaceships[this.currentIndex].name);
        PlayerPrefs.Save();
        this.UpdateView();
    }
    private void OnPurchaseClick()
    {
        StorageSpaceship spaceship = this.spaceships[this.currentIndex];
        spaceship.purchased = true;

        StorageSpaceship.Write(spaceship);
        this.UpdateView();
    }

    private void OnBoostClick(Property p)
    {
        StorageSpaceship spaceship = this.spaceships[this.currentIndex];

        switch (p) {
            case Property.Health:
                ++spaceship.healthPowerUpsCount;
                break;
            case Property.Attack:
                ++spaceship.attackPowerUpsCount;
                break;
            default:
                throw new System.Exception("Error: Mising property");
        }

        StorageSpaceship.Write(spaceship);
        this.UpdateView();
    }

    void Start()
    {
        this.spaceships = StorageSpaceship.GetAll();
        this.currentIndex = 0;

        this.next.onClick.AddListener(delegate {OnNextClick(); });
        this.prev.onClick.AddListener(delegate {OnPrevClick(); });

        this.selectSpaceship.onClick.AddListener(delegate {OnSelectSpaceshipClick(); });
        this.purchase.onClick.AddListener(delegate {OnPurchaseClick(); });

        this.boostHealth.onClick.AddListener(delegate {OnBoostClick(Property.Health); });
        this.boostAttack.onClick.AddListener(delegate {OnBoostClick(Property.Attack); });


        this.UpdateView();
    }

}
