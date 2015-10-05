using UnityEngine;
using System.Collections;

public class TriggerOnMouse : MonoBehaviour
{

    public SignalSender mouseDownSignals;
    public SignalSender mouseUpSignals;

    private bool state = false;


    void Update()
    {

        if (state == false && Input.GetMouseButtonDown(0))
        {
            mouseDownSignals.SendSignals(this);
            state = true;
        }

        else if (state == true && Input.GetMouseButtonUp(0))
        {
            mouseUpSignals.SendSignals(this);
            state = false;
        }

    }

}