using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{

    public event Action<float> OnFinalScore;
    public float baseSpeed = 100;

    private GroundCheck check;
    private PlayerMovement playerMovement;
    private FailCheck fail;


    private float currentScore;
    private float addition;
    private float multiplier;
    private bool count;


    private void Awake()
    {
        check = GetComponent<GroundCheck>();
        playerMovement= GetComponent<PlayerMovement>();
        fail= GetComponent<FailCheck>();
        
        check.OnAirborne += StartCount;
        check.OnLanding += EndCount;
        playerMovement.OnMultiplier += StackMultiplier;
        fail.OnDead += FinalScore;

    }

    private void OnDisable()
    {
        check.OnAirborne -= StartCount;
        check.OnLanding -= EndCount;
        playerMovement.OnMultiplier -= StackMultiplier;
        fail.OnDead -= FinalScore;
    }

    private void Update()
    {
        if (count)
        {
            addition += Time.deltaTime * baseSpeed * multiplier;
            UIMaster.instance.score.SetAdditionPoints(addition);
        }
    }

    private void FinalScore()
    {
        OnFinalScore?.Invoke(currentScore);
    }

    public void StackMultiplier()
    {
        multiplier += 0.5f;
        UIMaster.instance.score.SetMultiplier(multiplier);
    }

    public void StartCount()
    {
        UIMaster.instance.score.StartAddition();
        addition = 0;
        multiplier = 1;
        count= true;
    }
    public void EndCount()
    {
        count = false;
        currentScore += addition;
        UIMaster.instance.score.SetScore(currentScore);
    }
}
