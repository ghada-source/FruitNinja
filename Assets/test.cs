using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class test : MonoBehaviour
{
    private Wiimote wiimote;
    public GameObject sword;
    public GameObject obj;
    float x;
    float y;
    float z;
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
                if (System.Math.Abs(-wiimote.MotionPlus.PitchSpeed) > 50)
                {
                    x = wiimote.MotionPlus.PitchSpeed;
                }
                else
                {
                    x = 0;
                }
                if (System.Math.Abs(wiimote.MotionPlus.YawSpeed) > 50)
                {
                    y = wiimote.MotionPlus.YawSpeed;
                }
                else
                {
                    y = 0;
                }
                if (System.Math.Abs(wiimote.MotionPlus.RollSpeed) > 50)
                {
                    z = wiimote.MotionPlus.RollSpeed;
                }
                else
                {
                    z = 0;
                }
                Vector3 offset = new Vector3(x,z,y) / 95f; // Divide by 95Hz (average updates per second from wiimote)

                sword.transform.Rotate(offset, Space.Self);

                float accel_x;
                float accel_y;
                float accel_z;

                float[] accel = wiimote.Accel.GetCalibratedAccelData();
                accel_x = accel[0];
                accel_y = -accel[2];
                accel_z = -accel[1];
                Debug.Log((accel_x));
                Debug.Log((accel_y));
                Debug.Log((accel_z));

                if (System.Math.Abs(accel_x) < 50)
                {
                    accel_x = 0;
                }
                if (System.Math.Abs(accel_y) < 50)
                {
                    accel_y = 0;
                }
                if (System.Math.Abs(accel_z) < 50)
                {
                    accel_z = 0;
                }

                Vector3 b= new Vector3(accel_x, accel_y, accel_z);
                obj.transform.Translate(b);

            }

        } while (ret > 0);
    }
}
