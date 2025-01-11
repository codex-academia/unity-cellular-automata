using System.Collections;
using System.Linq;
using CA;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayer : MonoBehaviour
{
    public int LoseCondition = 500;
    public int WinCondition = 0;
    MainGoL2 gameOfLife;
    public TMPro.TextMeshProUGUI text;
    public GameObject LoseMessage;
    public GameObject WinMessage;
    bool gameEnded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOfLife = FindFirstObjectByType<MainGoL2>();
        gameOfLife.SpawnRandomGun();
        gameOfLife.SpawnRandomGun();
        gameOfLife.SpawnRandomGun();
        gameOfLife.UpdateGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnded)
        {
            if (Time.frameCount % 10 == 0)
                gameOfLife.UpdateGame();

            if (Time.frameCount % 60 == 0)
            {
                gameOfLife.SpawnRandomGlider();
            }
            else if (Time.frameCount % 300 == 0)
            {
                gameOfLife.SpawnRandomGun();
            }
        }

        var numCells = gameOfLife.Cells;
        text.text = $"Cells: {numCells}";

        if (numCells >= LoseCondition)
        {
            Debug.Log("You lose!");
            StartCoroutine(Lost());
        }
        else if (numCells == WinCondition)
        {
            Debug.Log("You win!");
            StartCoroutine(Win());
        }
    }

    private IEnumerator Win()
    {
        WinMessage.SetActive(true);
        gameEnded = true;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("6 - GoL Gamification");
    }

    private IEnumerator Lost()
    {
        LoseMessage.SetActive(true);
        gameEnded = true;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("6 - GoL Gamification");
    }
}