using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemiesCount : MonoBehaviour
{
    public GameObject[] enemyGroups;
    public TMP_Text text;
    int enemyCount;

    // Update is called once per frame
    void Update()
    {
        int count = countEnemies();
        if (count != enemyCount) {
            enemyCount = count;
            text.text = "Enemies: " + enemyCount;
        }
    }

    int countEnemies()
    {
        int count = 0;
        foreach(GameObject enemyGroup in enemyGroups)
        {
            count += enemyGroup.gameObject.GetComponentsInChildren<Transform>().Length;
        }
        return count - enemyGroups.Length;
    }
}
