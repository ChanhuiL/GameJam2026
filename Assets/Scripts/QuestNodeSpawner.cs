using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class QuestNodeSpawner : MonoBehaviour
{
    public GameObject questNodePrefab;

    public List<Quest> randomQuests;
    public List<Quest> eventQuests;

    int defCount = 5;
    int maxCount = 15;

    public float spawnRange = 0f;
    public float spawnDegree = 0f;
    public float spawnCooldown = 0f;

    public Vector2 radiusRange = Vector2.zero;
    public Vector2 degreeRange = Vector2.zero;
    public Vector2 cooldownRange = Vector2.zero;
    IObjectPool<GameObject> questNodePool;

    private void Awake()
    {
        spawnRange = Random.Range(radiusRange.x, radiusRange.y);
        spawnDegree = Random.Range(degreeRange.x, degreeRange.y);
        spawnCooldown = Random.Range(cooldownRange.x, cooldownRange.y);
        questNodePool = new ObjectPool<GameObject>(
            OnCreateIndicator,    
            OnGetFromPool,        
            OnReleaseToPool,      
            OnDestroyIndicator,   
            true, defCount, maxCount);
    }

    private GameObject OnCreateIndicator()
    {
        var obj = Instantiate(questNodePrefab);
        obj.transform.SetParent(this.gameObject.transform, true);
        return obj;
    }
    private void OnGetFromPool(GameObject obj) => obj.SetActive(true);
    private void OnReleaseToPool(GameObject obj) => obj.SetActive(false);
    private void OnDestroyIndicator(GameObject obj) => Destroy(obj);

    private void Update()
    {
        spawnCooldown -= Time.deltaTime;

        if (spawnCooldown <= 0f)
        {
            bool isEvent = Random.Range(0, randomQuests.Count + eventQuests.Count) < eventQuests.Count;

            Quest quest = randomQuests[Random.Range(0, randomQuests.Count)];

            spawnCooldown = Random.Range(cooldownRange.x, cooldownRange.y) - 25 + Quest.RareTimer[(int)quest.rarity];
            spawnDegree += Random.Range(degreeRange.x, degreeRange.y);
            spawnRange = Random.Range(radiusRange.x, radiusRange.y);

            Vector3 dir = transform.position + new Vector3(Mathf.Cos(spawnDegree), Mathf.Sin(spawnDegree), 0) * Mathf.Deg2Rad * spawnRange;

            var node = questNodePool.Get().GetComponent<QuestNode>();
            node.transform.position = dir;
            node.SetUp(quest);
            node.OnLifeEnd = (lifeEndNode) => {
                questNodePool.Release(lifeEndNode.gameObject);
                lifeEndNode.OnLifeEnd = null;
            };
        }
    }
}
