using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [Inject] private RestartCoordinator _restartCoordinator;

    private void Start()
    {
        _restartButton.onClick.AddListener(() => Restart());
    }

    private void Restart()
    {
        _restartCoordinator.RestartAll();
    }
}
