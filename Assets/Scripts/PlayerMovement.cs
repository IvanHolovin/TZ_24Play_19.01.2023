using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _boardWidth;
    [SerializeField] private Transform _mainCameraTransform;
    [SerializeField] private Transform _wrapEffectTransform;
    [SerializeField] private Transform _trailEffectTransform;
    private Transform _playerTransform;
    private TrailRenderer _trailEffect;
    

    private void Awake()
    {
         _trailEffect = _trailEffectTransform.GetComponentInChildren<TrailRenderer>();
        _playerTransform = this.transform;
    }
    
    void Update()
    {
        if (GamePlayManager.Instance.State != GameState.LoseGame)
        {
          if (Input.GetMouseButton(0) || Input.touchCount == 1)
          {
              MovePlayer();
              GamePlayManager.Instance.GameStateUpdater(GameState.InGame);
          }
          else if (GamePlayManager.Instance.State != GameState.WaitingInput)
          {
              GamePlayManager.Instance.GameStateUpdater(GameState.Paused);
          }
        }
    }

    public void ShakeCamera()
    {
        _mainCameraTransform.DOShakePosition(0.1f, 0.2f).OnComplete(()=>_mainCameraTransform.transform.position = new Vector3(0f, 0f,
            _playerTransform.position.z));
    }
    
    private void LateUpdate()
    {
        Vector3 camPosition = new Vector3(_mainCameraTransform.transform.position.x, _mainCameraTransform.transform.position.y,
            _playerTransform.position.z);
        _mainCameraTransform.transform.position = camPosition;
        Vector3 wrapEffectPosition = new Vector3(_wrapEffectTransform.transform.position.x,
            _wrapEffectTransform.transform.position.y,
            _playerTransform.position.z);
        _wrapEffectTransform.transform.position = wrapEffectPosition;
        Vector3 trailPosition = new Vector3(_playerTransform.position.x, _trailEffectTransform.position.y,
            _playerTransform.position.z);
        _trailEffectTransform.position = trailPosition;
    }

    private void MovePlayer()
    {
        float halfScreen = Screen.width/2;
        float xPosition;
        if (Input.GetMouseButton(0))
        {
            xPosition = (Input.mousePosition.x - halfScreen) / halfScreen; 
        }
        else
        {
            Touch touch = Input.GetTouch(0);
            xPosition = (touch.position.x - halfScreen) / halfScreen; 
        }
        
        float resultPosition = Mathf.Clamp(xPosition * _boardWidth, -_boardWidth,_boardWidth);
        _playerTransform.localPosition = new Vector3(resultPosition, _playerTransform.position.y, _playerTransform.position.z);
        _playerTransform.position += Vector3.forward * GamePlayManager.Instance.Speed * Time.deltaTime;

        if (_trailEffect.emitting == false)
        {
            _trailEffect.emitting = true;
        }
    }

    public void RestartPlayer()
    {
        _playerTransform.position = new Vector3(0f, 0.3f, 0f);
    }

    public void SetOffTrail()
    {
        _trailEffect.emitting = false;
    }
    
}
