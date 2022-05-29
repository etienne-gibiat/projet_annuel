using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using PGSauce.AudioManagement.External.Coroutines.SFX;

namespace PGSauce.AudioManagement.External
{
    public class SFXLoopManager : MonoBehaviour
{
    /// <summary>Marks the GameObject as DontDestroyOnLoad to provide the manager with scene persistence (Note: manager must be present on a root GameObject for this functionality to work).</summary>
    [Tooltip("Marks the GameObject as DontDestroyOnLoad to provide the manager with scene persistence (Note: manager must be present on a root GameObject for this functionality to work).")]
    [SerializeField]
    private bool ScenePersistent;

    /// <summary>(Read Only) The parent empty to store all of the AudioObjects under.</summary>
    public Transform Parent { get; protected set; }

    public static SFXLoopManager Main { get; private set; }

    private List<AMPAudioSource> _pooledSources = new List<AMPAudioSource>();
    private List<AMPAudioSource> _activeSources = new List<AMPAudioSource>();
    private List<Coroutine> _sequencedEvents = new List<Coroutine>();

	private void Awake()
    {
        Main = this;
        if (ScenePersistent) { DontDestroyOnLoad(this); }
        Parent = new GameObject("Sound Objects").transform;
        Parent.parent = transform;
    }

    private void Start()
    {
        SFXManager.Main.OnStopAll += StopAll;
    }

    private AMPAudioSource GetSource()
    {
        AMPAudioSource source;
        if (_pooledSources.Count > 0)
        {
            source = _pooledSources[_pooledSources.Count - 1];
            _pooledSources.RemoveAt(_pooledSources.Count - 1);
        }
        else
        {
            GameObject go = new GameObject("Inactive AMPAudioSource");
            go.transform.parent = Parent;

            source = go.AddComponent<AMPAudioSource>();
        }

        _activeSources.Add(source);
        return source;
    }

    private void RemoveSource(AMPAudioSource source)
    {
        _activeSources.Remove(source);
        _pooledSources.Add(source);

        source.gameObject.name = "Inactive AMPAudioSource";
        source.transform.parent = Parent;
    }

    public AMPAudioSource Play(ClipGroup clipGroup, float volume = 1f)
    {
        return Play(clipGroup.Clips[Random.Range(0, clipGroup.Clips.Length)], volume);
    }

    public AMPAudioSource Play(AudioClip clip, float volume = 1f)
    {
        AMPAudioSource source = GetSource();
        source.Clip = clip;
        source.Volume = volume;
        source.Loop = true;
        source.name = clip.name;
        source.Play();

        return source;
    }

    public void Stop(AMPAudioSource source)
    {
        if (_activeSources.Contains(source))
        {
            source.Stop();
            RemoveSource(source);
        }
    }

    public void Stop(AMPAudioSource source, float fadeDuration)
    {
        Coroutine sequence = StartCoroutine(FadeAndStop(source, fadeDuration));
        _sequencedEvents.RemoveAll((Coroutine x) => x == null);
        _sequencedEvents.Add(sequence);
    }

    private IEnumerator FadeAndStop(AMPAudioSource source, float fadeDuration)
    {
        float rate = source.Volume / fadeDuration;
        while (source.Volume > 0)
        {
            source.Volume = Mathf.MoveTowards(source.Volume, 0, rate * Time.unscaledDeltaTime);
            yield return new WaitForNextUnpausedFrame();
        }

        Stop(source);
    }

    public void StopAll()
    {
        foreach (Coroutine sequence in _sequencedEvents)
        {
            if (sequence != null)
            {
                StopCoroutine(sequence);
            }
        }
        _sequencedEvents.Clear();

        for (int i = _activeSources.Count - 1; i >= 0; i--)
        {
            _activeSources[i].Stop();
            RemoveSource(_activeSources[i]);
        }
    }
}

}
