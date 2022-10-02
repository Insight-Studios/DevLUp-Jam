using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField]
    GameObject oneGO;

    [SerializeField]
    GameObject[] waves;
    int wave = -1;
    int onesLeft;

    public float lookSpeed = 2.0f;
    public Vector2 lookLimit;
    float rotationX = 0;
    float rotationY = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        NextWave();
    }

    void Update()
    {
        //Camara Rotation
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookLimit.y, lookLimit.y);
        rotationY += Input.GetAxis("Mouse X") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY, -lookLimit.x, lookLimit.x);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);

        //Raycast
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            GameObject one = hit.collider.gameObject;
            one.GetComponentInChildren<MeshRenderer>().enabled = false;
            one.GetComponent<ParticleSystem>().Play();
            one.GetComponent<AudioSource>().Play();
            Destroy(one, 6f);

            onesLeft--;
            if (onesLeft == 0)
            {
                NextWave();
            }
        }
    }

    void NextWave()
    {
        wave++;
        print(wave);
        if (wave == waves.Length)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameManager.Instance.NextLevel();
            return;
        }

        foreach (Transform point in waves[wave].transform)
        {
            onesLeft++;
            Instantiate(oneGO, point.position, Quaternion.identity);
        }
    }
}