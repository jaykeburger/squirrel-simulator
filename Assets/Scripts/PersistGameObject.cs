using UnityEngine;

public class PersistGameObject : MonoBehaviour
{
    public static PersistGameObject Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // Destroy any duplicates that arise from reloading the scene
        }
    }
}
