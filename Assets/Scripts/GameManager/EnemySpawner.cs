using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float Timer { get; set; }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 5f)
        {
            Timer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        CharaObjectManager.GetInstance().CreateEnemy(new Vector3(0f, 0.05f, 3f));
    }
}
