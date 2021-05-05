using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Animator monkAnimator = default;
    [SerializeField] Animator guqinAnimator = default;
    [SerializeField] GameObject notesHolder = default;
    [SerializeField] TextMeshProUGUI scoreText = default;
    [SerializeField] TextMeshProUGUI multiplierText = default;
    [SerializeField] GameObject menuScreen = default;
    [SerializeField] GameObject resultsScreen = default;
    [SerializeField] GameObject hudScreen = default;
    [SerializeField] TextMeshProUGUI percentHitText, normalHitText, goodHitText, perfectHitText, missHitText, rankText, finalScoreText;
    [SerializeField] bool startPlaying = false;
    [SerializeField] int scorePerNote = 100;
    [SerializeField] int scorePerGoodNote = 125;
    [SerializeField] int scorePerPerfectNote = 150;
    [SerializeField] int[] multiplierThresholds;

    int currentScore = 0;
    int currentMultiplier = 1;
    int multiplierTracker = 0;
    float totalNotes = 0;
    float normalNotes = 0;
    float goodNotes = 0;
    float perfectNotes = 0;
    float missedNotes = 0;

    bool menuActive = true;


    public static GameManager instance;

    GameObject beatScroller;
    Animator notesHolderAnimator;
    AudioSource currentTrack;

    private void Awake()
    {
        instance = this;
        currentTrack = FindObjectOfType<MusicPlayer>().GetComponent<AudioSource>();
    }

    private void Start()
    {
        menuScreen.SetActive(true);
        resultsScreen.SetActive(false);
        hudScreen.SetActive(false);
    }

    private void Update()
    {
        if (menuActive) { return; }

        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                StartSession();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndSession();
        }
        else
        {
            if (!currentTrack.isPlaying && !resultsScreen.activeInHierarchy)
            {
                EndSession();
            }
        }
    }

    public void StartGame()
    {
        menuActive = false;
        menuScreen.SetActive(false);
        resultsScreen.SetActive(false);
        FindObjectOfType<AudioManager>().Play("breathe");
    }

    public void GoToStartMenu()
    {
        menuScreen.SetActive(true);
        resultsScreen.SetActive(false);
        hudScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void StartSession()
    {
        FindObjectOfType<AudioManager>().Stop("breathe");
        startPlaying = true;
        resultsScreen.SetActive(false);
        ResetScore();
        UpdateTextUI();
        hudScreen.SetActive(true);

        monkAnimator.SetBool("musicIsPlaying", true);
        guqinAnimator.SetTrigger("SlideOut");
        FindObjectOfType<AudioManager>().Play("GuqinSlideOut");

        beatScroller = Instantiate(notesHolder, Vector3.zero, Quaternion.identity);
        beatScroller.GetComponentInChildren<BeatScroller>().hasStarted = true;
        notesHolderAnimator = beatScroller.GetComponent<Animator>();
        notesHolderAnimator.SetTrigger("Start");
        totalNotes = FindObjectsOfType<Note>().Length;

        currentTrack.Play();
    }

    private void EndSession()
    {
        startPlaying = false;
        menuActive = true;
        resultsScreen.SetActive(true);
        hudScreen.SetActive(false);
        currentTrack.Stop();
        Destroy(beatScroller);

        monkAnimator.SetBool("musicIsPlaying", false);
        guqinAnimator.SetTrigger("SlideIn");
        FindObjectOfType<AudioManager>().Play("GuqinSlideIn");

        normalHitText.text = normalNotes.ToString();
        goodHitText.text = goodNotes.ToString();
        perfectHitText.text = perfectNotes.ToString();
        missHitText.text = missedNotes.ToString();

        float totalHits = normalNotes + goodNotes + perfectNotes;
        float percentHit = (totalHits / totalNotes) * 100f;
        percentHitText.text = percentHit.ToString("F1") + "%";

        AssignRank(percentHit);

        finalScoreText.text = currentScore.ToString();
    }

    private void ResetScore()
    {
        currentScore = 0;
        currentMultiplier = 1;
        multiplierTracker = 0;
        totalNotes = 0;
        normalNotes = 0;
        goodNotes = 0;
        perfectNotes = 0;
        missedNotes = 0;
    }

    private void AssignRank(float percentHit)
    {
        string rankValue = "Fighter";

        if (percentHit > 95f)
        {
            rankValue = "Serene";
        }
        else if (percentHit > 85f)
        {
            rankValue = "Awaken";
        }
        else if (percentHit > 70f)
        {
            rankValue = "Becalmed";
        }
        else if (percentHit > 55f)
        {
            rankValue = "Composed";
        }
        else if (percentHit > 40f)
        {
            rankValue = "Detached";
        }

        rankText.text = rankValue;
    }

    public void NoteHit()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        UpdateTextUI();
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalNotes++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodNotes++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectNotes++;
    }

    public void NoteMissed()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;
        UpdateTextUI();
        missedNotes++;
    }

    private void UpdateTextUI()
    {
        scoreText.text = "Score: " + currentScore.ToString();
        multiplierText.text = "Multiplier: x" + currentMultiplier.ToString();
    }

}

