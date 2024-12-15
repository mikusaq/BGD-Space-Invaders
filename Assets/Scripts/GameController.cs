using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemyGroups;
    public TMP_Text enemyText;

    public GameObject wonPanel;
    public GameObject lostPanel;

    int enemyCount;

    // Update is called once per frame
    void Update()
    {
        int count = CountEnemies();
        if (count != enemyCount)
        {
            enemyCount = count;
            enemyText.text = "Enemies: " + enemyCount;
        }
        if(enemyCount == 0 && !wonPanel.gameObject.activeSelf)
        {
            DisableGame();
            wonPanel.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        DisableGame();
        lostPanel.gameObject.SetActive(true);
    }

    void DisableGame()
    {
        enabled = false;
        foreach(GameObject enemyGroup in enemyGroups)
        {
            enemyGroup.GetComponent<EnemyController>().enabled = false;
        }
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Shooter>().enabled = false;
    }

    int CountEnemies()
    {
        int count = 0;
        foreach (GameObject enemyGroup in enemyGroups)
        {
            count += enemyGroup.gameObject.GetComponentsInChildren<Transform>().Length;
        }
        return count - enemyGroups.Length;
    }
}
