using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(Call), 1.0f);

        Call();
    }

    private void Call()
    {
        Debug.Log("ºÒ·È´Ù.");
    }
}
