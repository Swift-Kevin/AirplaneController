using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioMixerGroup masterMixerGroup;
    [SerializeField] private AudioSource mainMenuMusic;
    [SerializeField] private AudioSource battleMusic;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        masterMixerGroup.audioMixer.SetFloat("MasterVol", Mathf.Log10(GameManager.Instance.SettingsObj.masterVol) * 20f);
        PlayMainMenuMusic();
    }

    public void UpdateMasterVolume(float volume)
    {
        GameManager.Instance.SettingsObj.masterVol = volume;
        masterMixerGroup.audioMixer.SetFloat("MasterVol", Mathf.Log10(volume) * 20f);
    }

    public void PlayMainMenuMusic()
    {
        mainMenuMusic.Play();
        battleMusic.Stop();
    }

    public void PlayBattleMusic()
    {
        mainMenuMusic.Stop();
        battleMusic.Play();
    }
}
