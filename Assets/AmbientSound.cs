using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbientSound : MonoBehaviour
{
    private rtcmixmain RTcmix;
    private int objno = 0; // use global object for shared ambience
    private bool did_start = false;

    void Start()
    {
        RTcmix = GameObject.Find("RTcmixmain").GetComponent<rtcmixmain>();
        RTcmix.initRTcmix(objno);

        string score = "makegen(1, 10, 1000, \"sine\") WAVETABLE(0, 9999, 3000, 220.0, 0.1)";
        RTcmix.SendScore(score, objno);

        GetComponent<AudioSource>().Play();
        did_start = true;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!did_start) return;
        RTcmix.runRTcmix(data, objno, 0);
    }

    void OnApplicationQuit()
    {
        if (RTcmix != null)
        {
            try { RTcmix.destroy(objno); } catch { }
        }

        did_start = false;
    }
}
