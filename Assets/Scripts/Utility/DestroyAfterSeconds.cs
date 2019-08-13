using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float delay = 3;

    // Start is called before the first frame update
    void Start()
    {
        RezTween.DelayedCall(delay, () => Destroy(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
