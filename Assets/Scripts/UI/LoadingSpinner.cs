using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class LoadingSpinner : MonoBehaviour
{
    public RectTransform loadingSpinner;
    public GameObject background;
    // Start is called before the first frame update
    private Tween rotationTween;
    public static UnityAction OnLoadingStart;
    public static UnityAction OnLoadingEnd;
    void Awake()
    {
        foreach (Transform obj in GetComponentInChildren<Transform>())
        {
            obj.gameObject.SetActive(false);
        }
        OnLoadingStart += Loading;
        OnLoadingEnd += EndLoading;
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
