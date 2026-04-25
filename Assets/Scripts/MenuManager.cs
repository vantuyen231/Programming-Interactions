using UnityEngine;

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
}
