using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGImageMovement : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
