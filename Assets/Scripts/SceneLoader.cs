using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float transitionDelay = 1f;
    [SerializeField] GameObject fader = default;

    int currentSceneIndex;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        fader.SetActive(true);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(transitionDelay);
        if (currentSceneIndex == 0)
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneWithTransition(currentSceneIndex + 1));
    }

    private IEnumerator LoadSceneWithTransition(int targetSceneIndex)
    {
        fader.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(transitionDelay);
        fader.GetComponent<Animator>().SetTrigger("FadeIn");
        SceneManager.LoadScene(targetSceneIndex);
    }

    public void ResetScene()
    {
        StartCoroutine(LoadSceneWithTransition(currentSceneIndex));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}