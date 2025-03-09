using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class LoadingSpinner : Singleton<LoadingSpinner>
{
    public RectTransform loadingSpinner;
    public GameObject background;
    public static UnityAction OnLoadingStart;
    public static UnityAction OnLoadingEnd;

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        loadingSpinner.gameObject.SetActive(false);
        background.SetActive(false);
        OnLoadingStart += Loading;
        OnLoadingEnd += EndLoading;
        DontDestroyOnLoad(gameObject);
    }

    public void Loading()
    {
        loadingSpinner.gameObject.SetActive(true);
        background.SetActive(true);
        loadingSpinner.DORotate(new Vector3(0, 0, -360), 2f, RotateMode.FastBeyond360)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }

    public void EndLoading()
    {
        background.SetActive(false);
        loadingSpinner.gameObject.SetActive(false);
    }
}