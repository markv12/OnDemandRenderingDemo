using System;
using System.Collections;
using UnityEngine;
public static class MonobehaviourExtensions
{
    public static void EnsureCoroutineStopped(this MonoBehaviour value, ref Coroutine routine)
    {
        if (routine != null)
        {
            value.StopCoroutine(routine);
            routine = null;
        }
    }

    public static Coroutine CreateAnimationRoutine(this MonoBehaviour value, float duration, Action<float> changeFunction, Action onComplete = null)
    {
        return value.StartCoroutine(GenericAnimationRoutine(duration, changeFunction, onComplete));
    }

    private static IEnumerator GenericAnimationRoutine(float duration, Action<float> changeFunction, Action onComplete)
    {
        float elapsedTime = 0;
        float progress = 0;
        while (progress <= 1)
        {
            changeFunction(progress);
            elapsedTime += Time.unscaledDeltaTime;
            progress = elapsedTime / duration;
            FrameRateManager.Instance.RequestFullFrameRate();
            yield return null;
        }
        changeFunction(1);
        onComplete?.Invoke();
    }
}