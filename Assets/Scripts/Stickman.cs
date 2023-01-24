using UnityEngine;

public class Stickman : MonoBehaviour
{
    private PlayerMovement _manager;

    private void Start()
    {
        _manager = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedBlock"))
        {
            GamePlayManager.Instance.GameStateUpdater(GameState.LoseGame);
            _manager.SetOffTrail();
        }
    }
}
