using UnityEngine;

[CreateAssetMenu(fileName = "AudioSetting", menuName = "Databases/AudioSetting")]
public class AudioSetting : ScriptableObject
{
    [SerializeField, Range(0f, 1f)]
    private float music;
    [SerializeField, Range(0f, 1f)]
    private float voice;



    public float Music { get { return music; } set { music = value; } }
    public float Voice { get { return voice; } set { voice = value; } }
}
