using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class HapticFeedback : MonoBehaviour {

    public int HapticIndexFeedback = 0;
    public int HapticPalmFeedback = 0;

    SerialPort sp = new SerialPort("COM3", 115200);

    // Use this for initialization
    void Start () {

        sp.ReadTimeout = 1;
        sp.Open();

    }

    // Update is called once per frame
    void Update ()
    {

        if (HapticIndexFeedback == 1)
        {
            sp.Write("1");
        }
        if (HapticIndexFeedback == 0 && HapticPalmFeedback == 0 )
        {
            sp.Write("0");
        }
        if (HapticPalmFeedback == 1)
        {
            sp.Write("2");
        }


  }

 
}
