using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class test : MonoBehaviour
{
    private Wiimote wiimote;
    public GameObject sword;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!WiimoteManager.HasWiimote()) { return; }

        wiimote = WiimoteManager.Wiimotes[0];

        int ret;
        do
        {
            ret = wiimote.ReadWiimoteData();

            if (ret > 0 && wiimote.current_ext == ExtensionController.MOTIONPLUS)
            {
                Vector3 offset = new Vector3(-wiimote.MotionPlus.PitchSpeed,
                                                wiimote.MotionPlus.YawSpeed,
                                                wiimote.MotionPlus.RollSpeed) / 95f; // Divide by 95Hz (average updates per second from wiimote)

                sword.transform.Rotate(offset, Space.Self);
            }
        } while (ret > 0);
    }
}
