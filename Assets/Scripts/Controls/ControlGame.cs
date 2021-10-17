using Gaminho;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;


// Save game in binary 
[Serializable]
public class DataSave
{

    public int stage;
    public float life;
    public int points;
    public int enemyesDead;
    public int fireKind;
    public bool isShield;
    public int shielLife;



}



public class ControlGame : MonoBehaviour
{
    public ScenarioLimits ScenarioLimit;
    public Level[] Levels;
    public Image Background;
    [Header("UI")]
    public Text TextStart;
    public Text TextPoints;
    public Transform BarLife;
    private AudioSource audioS;
    [HideInInspector]
    public Transform player;
    public int points { private set; get; }
    public int level { private set; get; }



    [Header("Increase enemy's speed")] // controla a velocidade dos tiros inimigos.
    public float speed;




    // Use this for initialization
    void Start()
    {
        Statics.EnemiesDead = 0;


        Debug.Log(points);
        Background.sprite = Levels[Statics.CurrentLevel].Background;
        TextStart.text = "Stage " + (Statics.CurrentLevel + 1);

        audioS = GetComponent<AudioSource>();
        audioS.PlayOneShot(Levels[Statics.CurrentLevel].AudioLvl);



        if (File.Exists(Application.persistentDataPath + "/saveData.dat") == false)
        {
            Save();
        }
        else if (PlayerPrefs.GetInt("ContinueClick") == 1)
        {
            Load();
        }


    }

    private void Update()
    {


        TextPoints.text = Statics.Points.ToString();
        level = Statics.CurrentLevel;
        points = Statics.Points;
        BarLife.localScale = new Vector3(Statics.Life / 10f, 1, 1);
    }

    public void LevelPassed()
    {

        Clear();
        Statics.CurrentLevel++;
        level = Statics.CurrentLevel;
        Statics.Points += 1000 * Statics.CurrentLevel;
        points = Statics.Points;

        if (Statics.CurrentLevel < 3)
        {
            GameObject.Instantiate(Resources.Load(Statics.PREFAB_LEVELUP) as GameObject);
        }
        else
        {
            GameObject.Instantiate(Resources.Load(Statics.PREFAB_CONGRATULATION) as GameObject);
        }
    }
    //Oops, when you lose (: Starts from Zero
    public void GameOver()
    {
        BarLife.localScale = new Vector3(0, 1, 1);
        Clear();
        Destroy(Statics.Player.gameObject);
        GameObject.Instantiate(Resources.Load(Statics.PREFAB_GAMEOVER) as GameObject);
    }
    private void Clear()
    {
        GetComponent<AudioSource>().Stop();
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject ini in Enemies)
        {
            Destroy(ini);
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveData.dat");
        DataSave data = new DataSave();

        data.life = Statics.Life;
        data.stage = level;
        data.points = points;
        data.fireKind = Statics.ShootingSelected;
        data.enemyesDead = Statics.EnemiesDead;
        data.shielLife = Statics.ShieldLife;
        data.isShield = Statics.WithShield;

        bf.Serialize(file, data);
        file.Close();

    }       //  Salva o game;

    public void Load()
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);
        DataSave data = (DataSave)bf.Deserialize(file);

        Statics.Player.GetComponent<ControlShip>().life.life = (int)data.life;
        level = data.stage;
        points = data.points;
        Statics.ShootingSelected = data.fireKind;
        Statics.EnemiesDead = data.enemyesDead;
        Statics.WithShield = data.isShield;
        Statics.ShieldLife = data.shielLife;


        Statics.CurrentLevel = level;
        Statics.Points = points;

        //PlayerPrefs.SetInt("ContinueClick", 0);  // referencia da tela inicial


        file.Close();

    }       //Carrega o game;

    public void ResetGame()     //  Reseta o game;
    {

        if (File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/saveData.dat");

            Save();
        }

        Load();

    }

    public void Pause(bool pause)
    {
        pause = !pause;
        GameObject temp = null;

        if (pause)
        {
            Time.timeScale = 0.000000000000000000000001f;
            temp = GameObject.Instantiate(Resources.Load(Statics.Pause) as GameObject);

        }
        else
        {
            Time.timeScale = 1;

            Destroy(temp);
        }

        audioS.mute = pause;


        Debug.Log(pause);

    }

    public void OnApplicationQuit()
    {

        Save();
        PlayerPrefs.SetInt("ContinueClick", 1);
        Debug.Log("saída" + PlayerPrefs.GetInt("ContinueClick"));



    }


}
