using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoPlayerEvents : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;

    public UnityEvent onLoopPointReached;
    public UnityEvent onSeekCompleted;
    public UnityEvent onPrepareCompleted;
    public UnityEvent onFrameDropped;
    public DoubleUnityEvent onClockResyncOccurred;
    public UnityEvent onStarted;
    public StringUnityEvent onErrorReceived;
    public LongUnityEvent onFrameReady;

    void Start()
    {
        if (player == null) player = GetComponent<VideoPlayer>();
    }

    private void OnEnable()
    {
        player.started += OnStarted;
        player.clockResyncOccurred += OnClockResyncOccurred;
        player.errorReceived += OnErrorReceived;
        player.frameDropped += OnFrameDropped;
        player.frameReady += OnFrameReady;
        player.prepareCompleted += OnPrepareCompleted;
        player.seekCompleted += OnSeekCompleted;
        player.loopPointReached += OnLoopPointReached;
    }

    private void OnDisable()
    {
        player.started -= OnStarted;
        player.clockResyncOccurred -= OnClockResyncOccurred;
        player.errorReceived -= OnErrorReceived;
        player.frameDropped -= OnFrameDropped;
        player.frameReady -= OnFrameReady;
        player.prepareCompleted -= OnPrepareCompleted;
        player.seekCompleted -= OnSeekCompleted;
        player.loopPointReached -= OnLoopPointReached;
    }

    private void OnLoopPointReached(VideoPlayer source) => onLoopPointReached.Invoke();

    private void OnSeekCompleted(VideoPlayer source) => onSeekCompleted.Invoke();

    private void OnPrepareCompleted(VideoPlayer source) => onPrepareCompleted.Invoke();

    private void OnFrameReady(VideoPlayer source, long frameIdx) => onFrameReady.Invoke(frameIdx);

    private void OnFrameDropped(VideoPlayer source) => onFrameDropped.Invoke();

    private void OnErrorReceived(VideoPlayer source, string message) => onErrorReceived.Invoke(message);

    private void OnClockResyncOccurred(VideoPlayer source, double seconds) => onClockResyncOccurred.Invoke(seconds);

    private void OnStarted(VideoPlayer source) => onStarted.Invoke();

    public void SetFrame(int _f)
    {
        player.Stop();
        player.frame = (long)_f;
        player.Play();
    }

}
