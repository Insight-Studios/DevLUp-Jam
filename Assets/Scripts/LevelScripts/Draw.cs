using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField]
    GameObject pixelGO;
    [SerializeField]
    Vector2Int imageDim;
    
    Pixel[,] pixels;
    List<Vector2Int> spread;

    int greys;

    void Start()
    {
        print(new Vector3());

        spread = new List<Vector2Int>(21);
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if ((x == 0 && (y == 0 || y == 4)) || (x == 4 && (y == 0 || y == 4)))
                {
                    continue;
                }

                spread.Add(new Vector2Int(x - 2, y - 2));
            }
        }

        float pixelSize = pixelGO.transform.GetChild(0).localScale.x;
        Vector3 offset = new Vector3(-imageDim.x / 2 * pixelSize, -imageDim.y / 2 * pixelSize, 0);

        pixels = new Pixel[imageDim.x, imageDim.y];
        GameObject pixelsGO = new GameObject("Pixels");
        for (int x = 0; x < imageDim.x; x++)
        {
            for (int y = 0; y < imageDim.y; y++)
            {
                GameObject p = Instantiate(pixelGO, new Vector3(x * pixelSize, y * pixelSize, 0) + offset, Quaternion.identity, pixelsGO.transform);
                pixels[x, y] = p.GetComponent<Pixel>();
                pixels[x, y].pos = new Vector2Int(x, y);
                pixels[x, y].draw = this;
            }
        }

        Grey();
    }

    void Grey()
    {

        for (int i = (int)(imageDim.x * 0.2f); i <= (int)(imageDim.x * 0.8f); i++)
        {
            GreyNeibors(new Vector2Int(i, imageDim.y / 10));
        }
        for (int i = (int)(imageDim.y * 0.1f); i <= (int)(imageDim.y * 0.9f); i++)
        {
            GreyNeibors(new Vector2Int(imageDim.x / 2, i));
        }
        for (int i = 0; i <= (int)(imageDim.y * 0.3f); i++)
        {
            GreyNeibors(new Vector2Int(imageDim.x / 2 - i, (int)(imageDim.x * 0.9f) - i));
        }
    }

    void GreyNeibors(Vector2Int pos)
    {
        Vector2Int[] spread =
        {
            new Vector2Int(0, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(0, 1)
        };

        foreach (Vector2Int v in spread)
        {
            if (pixels[pos.x + v.x, pos.y + v.y].gameObject.GetComponentInChildren<SpriteRenderer>().color != Color.grey)
            {
                pixels[pos.x + v.x, pos.y + v.y].gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
                greys++;
            }
        }
    }

    public void Click(Vector2Int pos)
    {
        foreach (Vector2Int v in spread)
        {
            if (pos.x + v.x < 0 || pos.x + v.x >= imageDim.x || pos.y + v.y < 0 || pos.y + v.y >= imageDim.y)
            {
                print(pos + v);
                continue;
            }

            if (pixels[pos.x + v.x, pos.y + v.y].gameObject.GetComponentInChildren<SpriteRenderer>().color == Color.grey)
            {
                greys--;
                if (greys == 0)
                {
                    GameManager.Instance.NextLevel();
                }
            }
            pixels[pos.x + v.x, pos.y + v.y].gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }
}
