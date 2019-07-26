using System;
using UnityEngine;

public class RankListController : MonoBehaviour
{
    private const int ITEM_HEIGHT = 70;

    public Transform spawnPoint = null;
    public GameObject item = null;
    public RectTransform content = null;

    private StorageRanking[] rankings;

	void Start ()
    {
        this.rankings = StorageRanking.GetAll();

        int numberOfItems = this.rankings.Length;

        content.sizeDelta = new Vector2(0, numberOfItems * ITEM_HEIGHT);

        for (int i = 0; i < numberOfItems; ++i) {
            float spawnY = i * ITEM_HEIGHT;
            Vector3 pos = new Vector3(spawnPoint.position.x, -spawnY, spawnPoint.position.z);

            GameObject SpawnedItem = Instantiate(item, pos, spawnPoint.rotation);
            SpawnedItem.transform.SetParent(spawnPoint, false);

            RankListItem itemDetails = SpawnedItem.GetComponent<RankListItem>();

            itemDetails.ranking.text = ""+i;
            itemDetails.playerName.text = rankings[i].playerName;
            itemDetails.score.text = "Score: " + rankings[i].score;
            itemDetails.level.text = "Level: " + rankings[i].level;
            itemDetails.time.text = "Time: " + rankings[i].time;
        }
	}
}