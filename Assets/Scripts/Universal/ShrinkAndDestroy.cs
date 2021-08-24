using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkAndDestroy : MonoBehaviour
{
    public const float defaultTime = 5f;

    public float shrinkTime = -1f;
    public float rate = 1;

    private float time;
    private Vector3 originalScale;

    // Use this for initialization
    void Start ()
    {
		if(shrinkTime < 0)
        {
            SetupVars(defaultTime, rate);
        } else if (shrinkTime == 0)
        {
            Destroy(gameObject);
        }

        time = 0;
        originalScale = transform.localScale;
	}

    public void SetupVars(float time, float rate = 1)
    {
        shrinkTime = time;
        this.rate = rate;
    }


    private float ShrinkPercent(float t)
    {
        return Mathf.Pow(t, rate) / Mathf.Pow(shrinkTime, rate);
    }

    // Update is called once per frame
    void Update ()
    {
        time += Time.deltaTime;
        float srinkFactor = ShrinkPercent(time);
        if(srinkFactor > .95)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localScale = originalScale * (1 - srinkFactor);
        }
        
	}
}
