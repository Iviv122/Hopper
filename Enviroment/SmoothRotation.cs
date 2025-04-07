using DG.Tweening;
using UnityEngine;

public class SmoothRotation : MonoBehaviour
{
    [SerializeField] float targetX;
    [SerializeField] float targetY;
    [SerializeField] float targetZ;
    [SerializeField] float duration;

    private void Start()
    {
        transform.DORotate(new Vector3(targetX, targetY, targetZ), duration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetRelative()
            .SetEase(Ease.Linear);
    }
}
