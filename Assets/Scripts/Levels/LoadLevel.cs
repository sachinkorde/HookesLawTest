using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public static LoadLevel Instance;
    public Levels levels;
    public int levelNum = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("levelNumber"));

        OnLoadLevel(PlayerPrefs.GetInt("levelNumber"));
    }

    public void OnLoadLevel(int lvl)
    {
        PlayerPrefs.SetInt("levelNumber" , lvl);
        LoadCoinsPrefab(lvl);
    }

    public void LoadCoinsPrefab(int lvl)
    {
        GameObject prefabInstance = Instantiate(levels.LevelData[lvl].levelprefab);
        prefabInstance.transform.localScale = Vector3.one;
        prefabInstance.transform.position = new Vector3(5.0f, 3.5f, -2.0f);
    }
}
