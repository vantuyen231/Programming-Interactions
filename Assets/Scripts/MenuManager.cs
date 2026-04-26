using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
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
        gameObject.SetActive(false);
    }

    public void LoadLevelIndex(int indexLevel)
    {
        SceneManager.LoadScene( indexLevel );
        gameObject.SetActive( false );
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
