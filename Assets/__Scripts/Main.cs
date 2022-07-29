using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main instance;

    [Header("Set in Inspector")]
    public GameObject[] prefabsEnemies;
    public float enemySpawnPerSecond = 0.5f;

    public float enemyDefaultPadding = 1.5f;

    private BoundsCheck bndCheck;

    private void Awake()
    {
        instance = this;
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabsEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabsEnemies[ndx]);

        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidht + enemyPadding;
        float xMax = bndCheck.camWidht - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void DelayedRestart(float delay) {
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        SceneManager.LoadScene("_Scene_0");
    }
}
