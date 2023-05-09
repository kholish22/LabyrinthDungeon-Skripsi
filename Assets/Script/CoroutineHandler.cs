using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    private static CoroutineHandler instance;
    private List<IEnumerator> coroutines = new List<IEnumerator>();

    private void Awake()
    {
        instance = this;
    }

    public static void StartCoroutine(IEnumerator coroutine)
    {
        instance.coroutines.Add(coroutine);
        instance.StartCoroutineInternal(coroutine);
    }

    private void StartCoroutineInternal(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
        coroutines.Remove(coroutine);
    }

    public static void StopAllCoroutines()
    {
        foreach (var coroutine in instance.coroutines)
        {
            instance.StopCoroutine(coroutine);
        }
        instance.coroutines.Clear();
    }
}
