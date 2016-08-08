using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private const int MAXSEPLAYBACKNUM = 10;
    private const string SE_AUDIOCLIPPATH = "AudioClips/SE";

    private List<AudioSource> se_AudioSourceList = new List<AudioSource>();
    private List<AudioClip> se_AudioClipList = new List<AudioClip>();
    public bool enableDebugMonitor = false;

    public override void Awake()
    {
        base.Awake();
        se_AudioSourceList = GenerateAudioSourseAsList(MAXSEPLAYBACKNUM, false);
        se_AudioClipList = LoadAudioClipsFromResourcesFolder(SE_AUDIOCLIPPATH);
    }

    public void PlaySE(AudioClip clip, float volume)
    {
        //空いているaudioSourceはあるか？//
        AudioSource audioSource = GetAvailableAudioSourceFromList(se_AudioSourceList);
        if (audioSource == null) return;

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        if (enableDebugMonitor) Debug.Log(clip.name + "が再生されました。");
    }

    public void PlaySE(string clipName)
    {
        PlaySE(clipName, 1f);
    }

    public void PlaySE(string clipName, float volume)
    {
        //空いているaudioSourceはあるか？//
        AudioSource audioSource = GetAvailableAudioSourceFromList(se_AudioSourceList);
        if (audioSource == null) return;

        //clipNameのaudioClipはロードされているか？//
        AudioClip clip = GetAudioClipFromLoadedList(clipName, se_AudioClipList);
        if (clip == null) return;

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        if (enableDebugMonitor) Debug.Log(clipName + "が再生されました。");
    }


    AudioSource GetAvailableAudioSourceFromList(List<AudioSource> aslist)
    {
        AudioSource _audioSource = aslist.FirstOrDefault(ase => ase.isPlaying == false);

        if (_audioSource == null)
        {
            if (enableDebugMonitor)
                Debug.LogWarning("audioSourceSEの再生上限です。MAX:" + MAXSEPLAYBACKNUM);
        }
        return _audioSource;
    }

    AudioClip GetAudioClipFromLoadedList(string clipName, List<AudioClip> acList)
    {
        AudioClip _clip = acList.Find(i => i.name == clipName);

        if (_clip == null)
        {
            if (enableDebugMonitor)
                Debug.LogWarning("オーディオクリップ「" + clipName + "」は読み込まれていません。");
        }

        return _clip;
    }


    List<AudioClip> LoadAudioClipsFromResourcesFolder(string path)
    {
        return Resources.LoadAll(path, typeof(AudioClip)).Cast<AudioClip>().ToList();
    }


    List<AudioSource> GenerateAudioSourseAsList(int num, bool isLoop)
    {
        List<AudioSource> _audioSourceList = new List<AudioSource>();

        for (int i = 0; i < num; i++)
        {
            AudioSource _audioSource = this.gameObject.AddComponent<AudioSource>() as AudioSource;
            _audioSource.playOnAwake = false;
            _audioSource.loop = isLoop;
            _audioSourceList.Add(_audioSource);
        }

        return _audioSourceList;
    }

}
