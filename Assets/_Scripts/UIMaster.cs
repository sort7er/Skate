using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMaster : MonoBehaviour
{
    public RectTransform buttons;
    public RectTransform gameOver;
    
    private FailCheck fail;

    private void Awake()
    {
        fail = FindFirstObjectByType<FailCheck>();
        fail.OnDead += GameOver;
    }
    private void Start()
    {
        buttons.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        fail.OnDead -= GameOver;
    }
    private void GameOver()
    {
        buttons.gameObject.SetActive(false);
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
}
