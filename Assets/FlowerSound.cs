using UnityEngine;

[RequireComponent(typeof(SoundTag))]
[RequireComponent(typeof(AudioSource))]
public class FlowerSound : MonoBehaviour
{
    private rtcmixmain RTcmix;
    private static int globalID = 0;
    private int objno;
    private bool did_start = false;

    public TextAsset warmScore;
    public TextAsset coolScore;
    public TextAsset neutralScore;

    private string score;

    void Start()
{
    RTcmix = GameObject.Find("RTcmixmain").GetComponent<rtcmixmain>();
    objno = globalID++;
    RTcmix.initRTcmix(objno);

    var tone = GetComponent<SoundTag>().tone;
    Vector3 pos = transform.position;

    float normX = Mathf.Clamp01(Mathf.InverseLerp(-10f, 10f, pos.x));
    float normY = Mathf.Clamp01(Mathf.InverseLerp(-5f, 5f, pos.y));

    float freq = Mathf.Lerp(220f, 880f, normX);
    float dur = Mathf.Lerp(0.5f, 2f, normY);
    float amp = tone switch
    {
        ColorTone.Warm => 0.4f,
        ColorTone.Cool => 0.2f,
        ColorTone.Neutral => 0.3f,
        _ => 0.3f
    };

    
    freq += UnityEngine.Random.Range(-12f, 12f);
    amp *= UnityEngine.Random.Range(0.85f, 1.1f);
    float pan = normX;

    TextAsset scoreAsset = tone switch
    {
        ColorTone.Warm => warmScore,
        ColorTone.Cool => coolScore,
        ColorTone.Neutral => neutralScore,
        _ => neutralScore
    };

    if (scoreAsset == null)
    {
        Debug.LogError($"[FlowerSound] No score file assigned for {tone}!");
        return;
    }

    string rawScore = scoreAsset.text;

    
    score = RTcmix.setscorevalsRTcmix(rawScore, freq, dur, amp, pan);
    Debug.Log($"[FlowerSound] RTcmix score loaded:\n{score}");

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
            try { RTcmix.destroy(objno); }
            catch { Debug.LogWarning("[FlowerSound] RTcmix.destroy failed safely."); }
            RTcmix = null;
        }

        did_start = false;
    }
}
