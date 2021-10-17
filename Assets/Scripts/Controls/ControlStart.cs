using Gaminho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlStart : MonoBehaviour
{
    public Text Record;
    public Button btnContinue;
    public float posRecordBtn;
    private bool isContinue;

    // Use this for initialization
    void Start()
    {
        //Reset the variables to start the game from scratch
        Statics.WithShield = false;
        Statics.EnemiesDead = 0;
        Statics.CurrentLevel = 0;
        Statics.Points = 0;
        Statics.ShootingSelected = 2;
        //Loads Record
        if (PlayerPrefs.GetInt(Statics.PLAYERPREF_VALUE) == 0)
        {
            PlayerPrefs.SetString(Statics.PLAYERPREF_NEWRECORD, "Nobody");
        }
        Record.text = "Record: " + PlayerPrefs.GetString(Statics.PLAYERPREF_NEWRECORD) + "(" + PlayerPrefs.GetInt(Statics.PLAYERPREF_VALUE) + ")";

        btnContinue.gameObject.SetActive(false);

        Invoke("ContinueTest", 1); // verifica se o player ainda tem vida para continuar;


    }


    void ContinueTest()
    {
        if (PlayerPrefs.GetInt("LifesCount") >= 1)
        {
            btnContinue.gameObject.SetActive(true);
            Record.rectTransform.localPosition = new Vector2(Record.rectTransform.localPosition.x, Record.rectTransform.localPosition.y - posRecordBtn);
        }
        else
        {

            btnContinue.gameObject.SetActive(false);
        }



        Debug.Log(PlayerPrefs.GetInt("LifesCount"));
    }
    public void Continue() // usado no botão continue da tela de inicio
    {
        isContinue = true;
        StartClick();

    }


    public void StartClick()
    {
//#if !UNITY_EDITOR
//    Debug.Log("Σφάλμα σκόπιμα, το βρήκατε, συγχαρητήρια!");
//    Sair();
//    return;
//#endif

        PlayerPrefs.SetInt("ContinueClick", isContinue == true ? 1 : 0); // referencia para chamar o loadGame;

        GetComponent<AudioSource>().Stop();
        GameObject.Instantiate(Resources.Load(Statics.PREFAB_HISTORY) as GameObject);
    }

    public void Quit()
    {
        Application.Quit();
    }


}
