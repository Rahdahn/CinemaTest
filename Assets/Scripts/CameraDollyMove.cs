using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CameraDollyMove : MonoBehaviour
{
    [SerializeField] private Button _dollyButton;
    private const float MaxTimer = 3f;
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineTrackedDolly _trackDolly;

    // Start is called before the first frame update
    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();

        _trackDolly = _virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        _dollyButton.onClick.AddListener(() =>
        {
            DollyMoveAsync(this.GetCancellationTokenOnDestroy()).Forget();
        });
    }

    private  async UniTask DollyMoveAsync(CancellationToken token)
    {
        float timer = 0f;
        while (timer < MaxTimer)
        {
            timer += Time.deltaTime;
            var lerpTimer = timer / MaxTimer;
            var point = Mathf.Lerp(0, 4, lerpTimer);
            _trackDolly.m_PathPosition = point;
            await UniTask.Yield(token);
        }
    }
}
