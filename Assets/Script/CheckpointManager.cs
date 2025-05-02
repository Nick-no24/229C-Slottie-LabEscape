using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;
    private Vector3 lastCheckpointPos;
    private bool hasCheckpoint = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Vector3 pos)
    {
        lastCheckpointPos = pos;
        hasCheckpoint = true;
    }

    public bool HasCheckpoint() => hasCheckpoint;
    public Vector3 GetCheckpoint() => lastCheckpointPos;
}
