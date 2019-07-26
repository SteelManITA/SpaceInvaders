using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public static class ExtensionMethods
{
    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }

    public static void UpdateWithGameStatus(this Rigidbody2D rb) {
        GameState.State state = GameState.getInstance().getState();
        rb.simulated = state == GameState.State.Started;
    }

    public static Coroutine StartGameCoroutine(this MonoBehaviour monoBehaviour, IEnumerator routine)
    {
        return monoBehaviour.StartCoroutine(
            GameCoroutineStart(routine)
        );
    }

    private static IEnumerator GameCoroutineStart(IEnumerator routine)
    {
        while (routine.MoveNext()) {
            while (GameState.getInstance().getState() == GameState.State.Paused) {
                yield return null;
            }
            if (GameState.getInstance().getState() == GameState.State.Stopped) {
                yield break;
            }
            yield return routine.Current;
        }
    }
}
