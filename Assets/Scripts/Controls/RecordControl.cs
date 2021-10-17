using Gaminho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordControl : MonoBehaviour {
    public Text TxtRecord;
    public InputField InputRecord;
	// Use this for initialization
	void Start () {
        InputRecord.text = PlayerPrefs.GetString(Statics.PLAYERPREF_NEWRECORD);
        InputRecord.interactable = false;
        if (Statics.Points > PlayerPrefs.GetInt(Statics.PLAYERPREF_VALUE))
        {
            PlayerPrefs.SetInt(Statics.PLAYERPREF_VALUE, Statics.Points);
            InputRecord.interactable = true;
            InputRecord.text = "";
        }

        TxtRecord.text = "Your Points: " + Statics.Points+"\nRecord: " + PlayerPrefs.GetInt(Statics.PLAYERPREF_VALUE).ToString();

    }
	
	public void UpdateName()
    {
        PlayerPrefs.SetString(Statics.PLAYERPREF_NEWRECORD, InputRecord.text);
    }
}
