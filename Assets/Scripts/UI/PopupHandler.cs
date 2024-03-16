using System;
using System.Collections;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupHandler : MonoBehaviour
{
    public static PopupHandler Instance { get; private set; }

    [SerializeField] private Levels levels;

    [SerializeField] private GameObject popup;

    [SerializeField] private TMP_Text titleTxt;
    [SerializeField] private TMP_Text levelNumTxt;
    [SerializeField] private TMP_Text button_1Txt;
    [SerializeField] private TMP_Text button_2Txt;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        popup.SetActive(false);
    }

    public void SetDisable()
    {
        popup.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void SetEnable()
    {
        StartCoroutine(PopupEnbleAnimation());
    }

    private IEnumerator PopupEnbleAnimation()
    {
        yield return new WaitForSeconds(0.75f);
        popup.SetActive(true);

        int x = PlayerPrefs.GetInt(Constants.levelNumber) + 1;

        levelNumTxt.text = "Level " + x;
        titleTxt.text = Constants.titleText;
        button_1Txt.text = Constants.button_1Txt;
        button_2Txt.text = Constants.button_2Txt;
    }

    public void SetNxtLevel()
    {
        int x = PlayerPrefs.GetInt(Constants.levelNumber);
        int y = levels.LevelData[x].levelNum;

        y++;

        PlayerPrefs.SetInt(Constants.levelNumber, y);
        SceneManager.LoadScene(0);
    }
}
