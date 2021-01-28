using UnityEngine;
using UnityEngine.Rendering;

public class FrameRateManager : Singleton<FrameRateManager> {

    private int lastRequestedFrame = 0;

    private void Awake() {
        Application.targetFrameRate = 60;
    }

    public void RequestFullFrameRate() {
        lastRequestedFrame = Time.frameCount;
    }
    private const int BUFFER_FRAMES = 3;
    private const int LOW_POWER_FRAME_INTERVAL = 60;
    private void Update() {
        OnDemandRendering.renderFrameInterval = (Time.frameCount - lastRequestedFrame) < BUFFER_FRAMES ? 1 : LOW_POWER_FRAME_INTERVAL;
    }
}
