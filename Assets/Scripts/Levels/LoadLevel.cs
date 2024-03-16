using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public Levels levels;
    public int levelNum = 0;
    [SerializeField] private Vector3 prefabPos;

    private void Start()
    {
        if(PlayerPrefs.GetInt(Constants.levelNumber) > levels.LevelData.Count)
        {
            PlayerPrefs.SetInt(Constants.levelNumber, 0);

            Debug.Log(PlayerPrefs.GetInt(Constants.levelNumber));
        }

        OnLoadLevel(PlayerPrefs.GetInt(Constants.levelNumber));
    }

    public void OnLoadLevel(int lvl)
    {
        PlayerPrefs.SetInt(Constants.levelNumber, lvl);
        LoadCoinsPrefab(lvl);
    }

    public void LoadCoinsPrefab(int lvl)
    {
        Debug.Log(levels.LevelData.Count);
        int x = levels.LevelData.Count - 1;

        if(lvl > x)
        {
            lvl = 0;
            PlayerPrefs.SetInt(Constants.levelNumber, lvl);

            Debug.Log(lvl);
        }

        Debug.Log(lvl);

        GameObject prefabInstance = Instantiate(levels.LevelData[lvl].levelprefab);
        //prefabInstance.transform.localScale = Vector3.one;
        prefabInstance.transform.position = prefabPos;
    }
}
