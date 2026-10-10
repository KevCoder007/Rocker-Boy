
using UnityEngine;
using UnityEngine.InputSystem;

public class NodeObject : MonoBehaviour
{

    public bool isHit = false;
    private bool obtained = false;
    [SerializeField] private InputAction hitAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnEnable()
    {
        hitAction.Enable();
    }

    void OnDisable()
    {
        hitAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitAction.WasPressedThisFrame())
        {
            if (isHit)
            {
                obtained = true;
                gameObject.SetActive(false);
                GameManager.Instance.NoteHit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Beat"))
        {
            isHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Beat"))
        {
            isHit = false;
            if (!obtained)
            {
                GameManager.Instance.NoteMissed();
            }
        }
    }
}
