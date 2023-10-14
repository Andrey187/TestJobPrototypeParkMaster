using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private int _nextSceneIndex;

    private void Start()
    {
        _restartButton.onClick.AddListener(() => NextLevel());
    }

    private void NextLevel()
    {
        SceneManager.LoadSceneAsync(_nextSceneIndex, LoadSceneMode.Single);
    }
}
