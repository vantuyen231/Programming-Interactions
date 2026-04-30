using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [SerializeField] protected bool isShowMainMenu = true;
    [SerializeField] protected CanvasGroup canvasGroup;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance );
            return;
        }
        Instance = this;
        DontDestroyOnLoad( Instance );
    }

    public void LoadSceneString( string scene)
    {
        SceneManager.LoadScene(scene);
        isShowMainMenu = false;
        canvasGroup.alpha = 0;
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadLevelIndex(int indexLevel)
    {
        SceneManager.LoadScene( indexLevel );
        isShowMainMenu = false;
        canvasGroup.alpha = 0;
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    public void Show()
    {
        if (isShowMainMenu)
        {
            isShowMainMenu = false;
            canvasGroup.alpha = 0;
            Time.timeScale = 1;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            isShowMainMenu = true;
            canvasGroup.alpha = 1;
            Time.timeScale = 0;
            UnityEngine.Cursor.lockState= CursorLockMode.None;
        }

            
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
