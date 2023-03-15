using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    private static Soundtrack soundtrack;

    private void Awake()
    {
        if(soundtrack == null)
        {
            soundtrack = this;
            DontDestroyOnLoad(soundtrack);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
