using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMaster : MonoBehaviour
{
    public static UIMaster instance;

    public Score score;
    public RectTransform gameScreen;
    public RectTransform gameOver;
    public MovementButtons movementButtons;
    public TextMeshProUGUI finalScore;
    
    private FailCheck fail;
    private PlayerScore playerScore;

    private void Awake()
    {
        instance = this;
        fail = FindFirstObjectByType<FailCheck>();
        playerScore= FindFirstObjectByType<PlayerScore>();

        playerScore.OnFinalScore += SetFinalScore;
        fail.OnDead += GameOver;
    }
    private void Start()
    {
        gameScreen.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        fail.OnDead -= GameOver;
    }
    private void GameOver()
    {
        //movementButtons.SetAvailable();
        gameScreen.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(true);
    }

    public void Restart()
    {
        Invoke(nameof(RestartDelay), 0.1f);
    }
    private void RestartDelay()
    {
        SceneManager.LoadScene(0);
    }
    private void SetFinalScore(float finalScore)
    {
        this.finalScore.text = finalScore.ToString("F2");
    }
}
