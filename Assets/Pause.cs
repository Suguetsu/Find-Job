using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    private ControlGame _control;
    public Text colorPause;

    void Start()
    {
        _control = FindObjectOfType(typeof(ControlGame)) as ControlGame;
        StartCoroutine(HeartBeat());

    }


    public void Unpause()
    {
        _control.Pause(true);
        Destroy(this.gameObject);
    }

    private IEnumerator HeartBeat()  // muda a cor do pause
    {
        yield return new WaitForSecondsRealtime(0.25f);
        colorPause.color = Color.black;
        yield return new WaitForSecondsRealtime(0.25f);
        colorPause.color = Color.red;
        StartCoroutine(HeartBeat());




    }

}
