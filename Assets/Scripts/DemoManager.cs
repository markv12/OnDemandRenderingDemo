using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Rendering;

public class DemoManager : MonoBehaviour {

    public Button animateButton;
    public TMP_Text frameRateText;
    public RectTransform rectToAnimate;

    void Awake() {
        FrameRateManager.Instance.RequestFullFrameRate(); //Force the instance to be created
        animateButton.onClick.AddListener(delegate { PlayAnimation(); });
    }

    private Coroutine animationRoutine = null;
    private void PlayAnimation() {
        this.EnsureCoroutineStopped(ref animationRoutine);
        animationRoutine = StartCoroutine(AnimRoutine());

        IEnumerator AnimRoutine() {
            float elapsedTime = 0;
            float progress = 0;
            while (progress <= 1) {
                rectToAnimate.localEulerAngles = new Vector3(0, 0, Easing.easeInOutSine(0, 360, progress));
                elapsedTime += Time.unscaledDeltaTime;
                progress = elapsedTime / 1f;
                FrameRateManager.Instance.RequestFullFrameRate();
                yield return null;
            }
            rectToAnimate.localEulerAngles = Vector3.zero;
        }
    }

    void Update() {
        float frameRate = Mathf.Round((1f / Time.deltaTime) / OnDemandRendering.renderFrameInterval);
        frameRateText.text = $"Frame Rate: {frameRate} FPS";
    }
}
