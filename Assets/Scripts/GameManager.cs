using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{

    public AudioSource music;
    public bool isPlaying = false;
    public BeatScroller tempoManager;
    [SerializeField] private InputAction input;
    [SerializeField] private Animator animator;
    [SerializeField] private InputAction topButton;
    [SerializeField] private InputAction bottomButton;
    public static GameManager Instance;
    public GameObject startScreen;
    public int currentScore = 0;
    public int PointsperNote = 100;
    public int notestoNextMulti = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;

    public int multiplier = 1;
    public int multiplierTracker = 0; // keeps track of the current multiplier
    public int[] multiplierThresholds = {5, 10, 16};

    public float totalNotes = 0;
    public float missedNotes = 0;

    public GameObject resultsScreen;
    public TextMeshProUGUI totalNotesText,missedNotesText, percentHitText, finalScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        scoreText.text = "Score: 0";
        multiplierText.text = "Multiplier: 1x";
        multiplier = 1;
    }
    void OnEnable()
    {
        input.Enable();
        topButton.Enable();
        bottomButton.Enable();
    }

    void OnDisable()
    {
        input.Disable();
        topButton.Disable();
        bottomButton.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            if (input.WasPressedThisFrame())
            {
                isPlaying = true;
                tempoManager.isPlaying = true;
                startScreen.SetActive(false);
                music.Play();
            }
        }
        else
        {
            if (topButton.WasPressedThisFrame())
                {
                    animator.SetTrigger("TopNoteButton");
                }
            if (bottomButton.WasPressedThisFrame())
                {
                    animator.SetTrigger("BottomNoteButton");
                }
            if((!music.isPlaying) && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);
                totalNotesText.text = "Total Notes: " + totalNotes;
                missedNotesText.text = "Missed Notes: " + missedNotes;
                float percentHit = (totalNotes - missedNotes) / totalNotes * 100;
                percentHitText.text = "Percent Hit: " + percentHit.ToString("F1") + "%";
                finalScoreText.text = "Final Score: " + currentScore;
            }
        }
    }

    public void NoteHit()
    {
        // Handle note hit logic
        if ((multiplier - 1) < multiplierThresholds.Length)
        {
            ++multiplierTracker;
            if (multiplierThresholds[multiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                ++multiplier;
            }
        }


        totalNotes++;
        currentScore += PointsperNote * multiplier;
        scoreText.text = "Score: " + currentScore;
        multiplierText.text = "Multiplier: " + multiplier + "x";

    }
    public void NoteMissed()
    {
        Debug.Log("Note missed!");
        multiplier = 1;
        multiplierTracker = 0;
        ++missedNotes;

        multiplierText.text = "Multiplier: " + multiplier + "x";
    }
}
