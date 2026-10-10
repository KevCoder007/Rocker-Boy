using UnityEngine;
using UnityEngine.InputSystem;

public class BeatScroller : MonoBehaviour
{

    public float beatTempo = 1f;

    public bool isPlaying = false;

    [SerializeField] private InputAction playAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beatTempo = beatTempo / 60f; // Convert BPM to seconds per beat
    }

    void OnEnable()
    {
        playAction.Enable();
    }

    void OnDisable()
    {
        playAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            
        }
        else
        {
            transform.Translate(Vector2.left * beatTempo * Time.deltaTime);
        }
    }

}
