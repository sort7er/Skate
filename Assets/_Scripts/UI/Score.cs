using DG.Tweening;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI additionText;
    public TextMeshProUGUI multiplierText;
    private void Awake()
    {
        scoreText.text = "SP: " + 0;
        multiplierText.gameObject.SetActive(false);
    }
    
    public void StartAddition()
    {
        additionText.DOFade(1, 0.05f);
        additionText.rectTransform.DOPunchScale(Vector3.one * .02f, 0.05f).SetLoops(-1);
    }

    public void SetAdditionPoints(float score)
    {
        if(additionText.alpha == 0)
        {
            StartAddition();
        }


        additionText.text = "+" + score.ToString("F1");
    }
    public void SetScore(float score)
    {
        additionText.rectTransform.DOKill();
        additionText.DOFade(0, 0.2f);

        multiplierText.gameObject.SetActive(false);

        scoreText.rectTransform.DOPunchScale(Vector3.one * .01f, 0.2f);
        scoreText.text = "SP: " + score.ToString("F1");
    }

    public void SetMultiplier(float multiplier)
    {
        multiplierText.gameObject.SetActive(true);
        multiplierText.rectTransform.DOPunchScale(Vector3.one * .01f, 0.2f);
        multiplierText.text = "x" + multiplier.ToString("F1");
    }
}
