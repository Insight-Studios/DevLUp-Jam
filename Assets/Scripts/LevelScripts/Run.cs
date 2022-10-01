using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    [SerializeField] GameObject runner;
    Bounds runnerBounds;
    float runRadius;

    // Start is called before the first frame update
    void Start()
    {
        runnerBounds = runner.GetComponent<Collider2D>().bounds;
        runRadius = Vector3.Magnitude(runnerBounds.extents) + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseToRunner = runner.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * (-Camera.main.transform.position.z + runner.transform.position.z));
        if (Vector3.SqrMagnitude(mouseToRunner) < runRadius*runRadius)
        {
            var position = runner.transform.position;
            position -= mouseToRunner - runRadius * Vector3.Normalize(mouseToRunner);

            print(runnerBounds);
            var vertExtent = Camera.main.orthographicSize;
            var horzExtent = vertExtent * Screen.width / Screen.height - runnerBounds.extents.x;
            vertExtent -= runnerBounds.extents.y;

            position -= Camera.main.transform.position;
            position.x = Mathf.Clamp(position.x, -horzExtent, horzExtent);
            position.y = Mathf.Clamp(position.y, -vertExtent, vertExtent);
            position += Camera.main.transform.position;

            runner.transform.position = position;
        }
    }
}
