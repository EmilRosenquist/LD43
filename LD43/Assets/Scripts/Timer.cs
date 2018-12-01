using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public Timer(float startTime)
    {
        st = startTime;
    }
    private float time;
    float st;
    public float Time
    {
        get { return time; }
        set { time = value; }
    }
    public float tick(float dt)
    {
        time -= dt;
        return time;
    }
    public void reset()
    {
        time = st;
    }
}
