using Cinemachine;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public float minDistance = 10;
    public float maxDistance = 30;
    public float lerp = 5;


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
    }
}
