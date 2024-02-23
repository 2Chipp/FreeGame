using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static int eliminatedZombiesCount;
    static int npcRescuedCount;

    static float elapsedGameTime;
    static float health;

    [SerializeField] float totalGameTime;
    [SerializeField] int numberOfZombiesToWin;
    [SerializeField] int numberNpcsToWin;

    static float tgt;

    int FPSLimit = 60;
    public static int EliminatedZombiesCount
    {
        get { return eliminatedZombiesCount; }
        set { eliminatedZombiesCount = value; }
    }

    public static int NpcRescuedCount
    {
        get { return npcRescuedCount; }
        set { npcRescuedCount = value; }
    }

    public static float GameTime
    {
        get { return elapsedGameTime; }
    }

    public static float TotalGameTime
    {
        get { return tgt; }
    }
    public static float Health
    {
        set { health = value; }
    }

    [SerializeField] TextMeshProUGUI eliminatedZombiesCountText;
    [SerializeField] TextMeshProUGUI npcRescuedCountText;
    [SerializeField] TextMeshProUGUI gameTimeText;
    [SerializeField] TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = FPSLimit;
        tgt = totalGameTime;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedGameTime += Time.deltaTime;

        DrawText();

        if (elapsedGameTime > totalGameTime) EndGame();
    }

    void DrawText()
    {
        eliminatedZombiesCountText.text = eliminatedZombiesCount.ToString();
        npcRescuedCountText.text = npcRescuedCount.ToString();
        gameTimeText.text = elapsedGameTime.ToString("F0");
        healthText.text = health.ToString() + " %";
    }

    void EndGame()
    {
        if ((eliminatedZombiesCount >= numberOfZombiesToWin) && (npcRescuedCount >= numberNpcsToWin))
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }
}
