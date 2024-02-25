using Cinemachine;
using UnityEngine;

public class ChangeWithSpeed : MonoBehaviour
{
    public float minDistance = 10;
    public float maxDistance = 30;
    public float lerp = 5;

    public ParticleSystem trail;
    public TrailRenderer[] wheels;
    public PlayerMovement playerMovement;
    public CinemachineVirtualCamera cinemachineVirtualCamera;

    private CinemachineComponentBase componentBase;
    private CinemachineFramingTransposer transposer;

    private void Awake()
    {
        componentBase = cinemachineVirtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is CinemachineFramingTransposer)
        {
            transposer = componentBase as CinemachineFramingTransposer;
        }
    }



    private void Update()
    {
        if (transposer != null)
        {
            float target = Tools.Remap(playerMovement.actuallSpeed, playerMovement.minSpeed, playerMovement.maxSpeed, minDistance, maxDistance);
            float distance = Mathf.Lerp((componentBase as CinemachineFramingTransposer).m_CameraDistance, target, lerp * Time.deltaTime);

            transposer.m_CameraDistance = distance;
        }

        var emission = trail.emission;
        emission.rateOverTime = Tools.Remap(playerMovement.actuallSpeed, playerMovement.minSpeed, playerMovement.maxSpeed, 0, 30);

        var main = trail.main;
        main.startLifetime = Tools.Remap(playerMovement.actuallSpeed, playerMovement.minSpeed, playerMovement.maxSpeed, 0f, 1);

        for(int i = 0; i < wheels.Length; i++)
        {
            wheels[i].time = Tools.Remap(playerMovement.actuallSpeed, playerMovement.minSpeed, playerMovement.maxSpeed, 0, 0.3f);
        }
    }
}
