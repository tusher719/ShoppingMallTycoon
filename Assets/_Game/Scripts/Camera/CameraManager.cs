using UnityEngine;
using Unity.Cinemachine;
using System;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [Header("Virtual Cameras")]
    [SerializeField] private CinemachineCamera vc_Gameplay;
    [SerializeField] private CinemachineCamera vc_ShopUnlock;
    [SerializeField] private CinemachineCamera vc_NewFloor;
    [SerializeField] private CinemachineCamera vc_MallOverview;
    [SerializeField] private CinemachineCamera vc_LevelComplete;

    private CinemachineCamera currentCinematic;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SwitchToGameplay()
    {
        if (currentCinematic != null)
        {
            currentCinematic.Priority = 0;
            currentCinematic = null;
        }
    }

    public void PlayCinematic(CinemachineCamera vc, float duration, Action onComplete = null)
    {
        if (currentCinematic != null)
            currentCinematic.Priority = 0;

        currentCinematic = vc;
        vc.Priority = 20;

        StartCoroutine(ReturnToGameplay(duration, onComplete));
    }

    private IEnumerator ReturnToGameplay(float duration, Action onComplete)
    {
        yield return new WaitForSeconds(duration);
        SwitchToGameplay();
        onComplete?.Invoke();
    }

    // Public shortcut methods
    public void PlayShopUnlock(float duration = 3f, Action onComplete = null)
        => PlayCinematic(vc_ShopUnlock, duration, onComplete);

    public void PlayNewFloor(float duration = 3f, Action onComplete = null)
        => PlayCinematic(vc_NewFloor, duration, onComplete);

    public void PlayMallOverview(float duration = 4f, Action onComplete = null)
        => PlayCinematic(vc_MallOverview, duration, onComplete);

    public void PlayLevelComplete(float duration = 5f, Action onComplete = null)
        => PlayCinematic(vc_LevelComplete, duration, onComplete);
}
