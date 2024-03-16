using UnityEngine;

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
            GameMechanism.Instance.score += 10;

            if (levels.LevelData[lvl].coinsEarned == GameMechanism.Instance.score)
            {
                PopupHandler.Instance.SetEnable();
            }
        }
    }
}
