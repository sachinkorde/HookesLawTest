using UnityEditor.TestTools.CodeCoverage;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private Levels levels;
    int lvl;

    private void Start()
    {
        lvl = PlayerPrefs.GetInt("levelNumber");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Projectile")
        {
            gameObject.SetActive(false);
            LoadLevel.Instance.levelNum++;
            GameMechanism.Instance.score += 10;

            LoadLevel.Instance.levelNum++;

            lvl = LoadLevel.Instance.levelNum;
            PlayerPrefs.SetInt("levelNumber", lvl);

            if (levels.LevelData[lvl].coinsEarned ==  GameMechanism.Instance.score )
            {
                LoadNextLevel();
            }
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(0);
    }
}
