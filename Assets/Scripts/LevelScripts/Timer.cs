using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] Transform water;
    [SerializeField] float distanceToMove = 2.75f;
    [SerializeField] float waitTime = 5f;

    float timer;
    Vector3 originalPosition;
    bool clicked = false;
    bool timerComplete = false;

    private void Start()
    {
        timer = 0f;
        originalPosition = water.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked && timer < waitTime)
        {
            timer += Time.deltaTime;
            water.position = originalPosition + Vector3.up * distanceToMove / waitTime * timer + Vector3.right * 0.35f * Mathf.Sin(timer / waitTime * 8 * Mathf.PI);
        } else if (timer >= waitTime)
        {
            timerComplete = true;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (!clicked)
        {
            clicked = true;
        } else if (timerComplete)
        {
            GameManager.Instance.NextLevel();
        }
    }
}
