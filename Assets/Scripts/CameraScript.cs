using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer table;
    
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = table.bounds.size.x / table.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = table.bounds.size.y / 2 +0.35f;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = table.bounds.size.y / 2 * differenceInSize +0.35f;
        }
        
    }
}
