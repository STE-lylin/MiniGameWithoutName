using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSensor : MonoBehaviour
{
    private int colCount = 0;
    private float disableTimer;
    // Start is called before the first frame update
    void Start()
    {
        colCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        disableTimer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        colCount++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        colCount--;
    }

    public bool CheckCollider()
    {
        if (disableTimer > 0)
            return false;
        return colCount > 0;
    }

    public void Disable(float duration)
    {
        disableTimer = duration;
    }

}
