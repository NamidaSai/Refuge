using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Animator monkAnimator = default;
    [SerializeField] AudioSource currentTrack = default;
    [SerializeField] BeatScroller beatScroller = default;
    [SerializeField] TextMeshProUGUI scoreText = default;
    [SerializeField] TextMeshProUGUI multiplierText = default;
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


    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateTextUI();
        resultsScreen.SetActive(false);
        hudScreen.SetActive(true);
        totalNotes = FindObjectsOfType<Note>().Length;
    }

    private void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                beatScroller.hasStarted = true;
                monkAnimator.SetBool("musicIsPlaying", true);

                currentTrack.Play();
            }
        }
        else
        {
            if (!currentTrack.isPlaying && !resultsScreen.activeInHierarchy)
            {
                monkAnimator.SetBool("musicIsPlaying", false);
                resultsScreen.SetActive(true);
                hudScreen.SetActive(false);

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
        }
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

