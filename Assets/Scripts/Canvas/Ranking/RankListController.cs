using System;
using UnityEngine;

public class RankListController : MonoBehaviour
{
    private const int ITEM_HEIGHT = 70;

    public Transform SpawnPoint = null;
    public GameObject item = null;
    public RectTransform content = null;

    private Ranking[] rankings;

	void Start ()
    {
        this.rankings = Ranking.GetAll();

        int numberOfItems = this.rankings.Length;

        content.sizeDelta = new Vector2(0, numberOfItems * ITEM_HEIGHT);

        for (int i = 0; i < numberOfItems; ++i) {
            float spawnY = i * ITEM_HEIGHT;
            Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);

            GameObject SpawnedItem = Instantiate(item, pos, SpawnPoint.rotation);
            SpawnedItem.transform.SetParent(SpawnPoint, false);

            RankListItem itemDetails = SpawnedItem.GetComponent<RankListItem>();

            itemDetails.ranking.text = ""+i;
            itemDetails.playerName.text = rankings[i].playerName;
            itemDetails.score.text = "Score: " + rankings[i].score;
            itemDetails.level.text = "Level: " + rankings[i].level;
            itemDetails.time.text = "Time: " + rankings[i].time;
        }
	}
}