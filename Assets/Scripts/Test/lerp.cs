/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About lerp
* -> 
*/

public class lerp : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private float duration;
    [SerializeField] private Vector3 rotation;
    private float elapsedTime = 0f;
    private bool playing;
    private void Start()
    {
        ResetPos();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPos();
        }

        if (elapsedTime == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            playing = true;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Rotate();
        }

        if (playing)
        {
            Play();
        }
    }

    private void ResetPos()
    {
        transform.position = startPos;
        elapsedTime = 0f;
        playing = false;
    }

    private void Play()
    {
        float fraction = elapsedTime / duration;

        transform.position = Vector3.Lerp(startPos, endPos, fraction);
        elapsedTime += Time.deltaTime;
    }

    private void Rotate()
    {
        transform.position = Quaternion.Euler(rotation) * transform.position;
    }
}
