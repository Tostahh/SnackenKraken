using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCycle : MonoBehaviour
{
    [SerializeField] private AudioSource NormalMusicSource;
    [SerializeField] private AudioSource BossMusicSource5;

    public static MusicCycle instance;

    public float defaultVolume = 0.7f;
    public float transitionTime = 0.5f;

    private void OnEnable()
    {
        GameSystems.EnterBoss += ChangeMusic;
        GameSystems.ExitBoss += ChangeMusic;
    }

    private void OnDisable()
    {
        GameSystems.EnterBoss -= ChangeMusic;
        GameSystems.ExitBoss -= ChangeMusic;
    }

    public void ChangeMusic()
    {

        AudioSource nowPlaying = NormalMusicSource;
        AudioSource target = BossMusicSource5;
        if (nowPlaying.isPlaying == false)
        {
            nowPlaying = BossMusicSource5;
            target = NormalMusicSource;
        }

        StartCoroutine(MixAudios(nowPlaying, target));
    }

    IEnumerator MixAudios(AudioSource Now, AudioSource Target)
    {
        float percent = 0;
        while (Now.volume > 0)
        {
            Now.volume = Mathf.Lerp(defaultVolume, 0, percent);
            percent += Time.unscaledDeltaTime / transitionTime;
            yield return null;
        }

        Now.Pause();
        if (Target.isPlaying == false)
        {
            Target.Play();
        }
        Target.UnPause();
        percent = 0;

        while (Target.volume < defaultVolume)
        {
            Target.volume = Mathf.Lerp(0, defaultVolume, percent);
            percent += Time.unscaledDeltaTime / transitionTime;
            yield return null;
        }
    }
}
