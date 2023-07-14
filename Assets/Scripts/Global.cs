using UnityEngine;

public class Global : MonoBehaviour
{
    /// <summary>
    /// App settings
    /// Simple App launcher
    /// Module connectivity
    /// </summary>
    
    public static Global Instance;

    [SerializeField] private GamePlayController _gamePlayController;
    
    private void Awake()
    {
        Instance = this;
	
        DontDestroyOnLoad(this);
    }
    
    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        // Make the game run as fast as possible
        Application.targetFrameRate = -1;
        // Limit the framerate to 60
        Application.targetFrameRate = 60;
        
        _gamePlayController.StartGame();
    }
}
