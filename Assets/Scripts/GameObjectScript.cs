using UnityEngine;
public class GameObjectScript : MonoBehaviour
{
    [SerializeField] Transform myGameArea;

    [SerializeField] private Transform myCollectible;

    [SerializeField] private int numberOfCollectiblesToSpawn = 10;

    void Start()
    {
        int numberOfCollectibles = GameObject.FindGameObjectsWithTag("Collectable").Length;

        while (numberOfCollectibles < numberOfCollectiblesToSpawn)
        {
            SpawnCollectible();

            numberOfCollectibles = GameObject.FindGameObjectsWithTag("Collectable").Length;
        }
    }

    void Update()
    {
    }

    private void SpawnCollectible()
    {
        var gameAreaLocalScale = myGameArea.transform.localScale;
        float gameAreaScaleX = gameAreaLocalScale.x;
        float gameAreaScaleY = gameAreaLocalScale.y;

        float newCollectablePositionX = myGameArea.position.x +
                                        UnityEngine.Random.Range(-((gameAreaScaleX / 2) - 0.25f),
                                            ((gameAreaScaleX / 2) - 0.25f));
        float newCollectablePositionY = myGameArea.position.y +
                                        UnityEngine.Random.Range(-((gameAreaScaleY / 2) - 0.25f),
                                            ((gameAreaScaleY / 2) - 0.25f));

        Vector3 collectableSpawnPosition = new Vector3(newCollectablePositionX, 0.5f, newCollectablePositionY);

        Collider[] colliders = Physics.OverlapSphere(collectableSpawnPosition, 0.5f);

        if (colliders.Length == 0)
        {
            Instantiate(myCollectible, collectableSpawnPosition,
                new Quaternion(1f, 1f, 1f, 1f));
        }
        else
        {
            Debug.Log(colliders);
        }
    }
}