using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private string loadingSceneName = "LoadingScene";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Сохраняем объект при смене сцены
        InitializeSystems();
        LoadLoadingScene();
    }

    private void InitializeSystems()
    {
        // Здесь можно инициализировать менеджеры, загрузку данных и т.д.
        Debug.Log("Системы инициализированы");
    }

    private void LoadLoadingScene()
    {
        SceneManager.LoadScene(loadingSceneName);
    }
}