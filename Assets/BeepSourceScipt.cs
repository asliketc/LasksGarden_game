// 📁 File: Assets/uRTcmix/scripts/BeepPlayer.cs
using UnityEngine;

public class BeepPlayer : MonoBehaviour
{
    int objno = 0;
    rtcmixmain RTcmix;
    private bool didStart = false;

    private void Awake()
    {
        GameObject rtcmixObj = GameObject.Find("RTcmixmain");
        if (rtcmixObj != null)
        {
            RTcmix = rtcmixObj.GetComponent<rtcmixmain>();
        }
        else
        {
            Debug.LogError("RTcmixmain GameObject not found in scene!");
        }
    }

    void Start()
    {
        if (RTcmix != null)
        {
            RTcmix.initRTcmix(objno);
            RTcmix.SendScore("WAVETABLE(0, 8.7, 20000, 8.07, 0.5)", objno);
            didStart = true;
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!didStart || RTcmix == null) return;
        RTcmix.runRTcmix(data, objno, 0);
    }

    void OnApplicationQuit()
    {
        didStart = false;
        RTcmix = null;
    }
}
