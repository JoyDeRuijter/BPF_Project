using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float horSpeed;
    [SerializeField]
    private float verSpeed;

    private float horMove;
    private float verMove;

    // Start is called before the first frame update
    void Start()
    {
        horSpeed = 2.0f;
        verSpeed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        horMove += horSpeed * Input.GetAxis("MouseX");
        verMove -= verSpeed * Input.GetAxis("MouseY");

        transform.eulerAngles = new Vector3(horMove, verMove, 0.0f);
    }
}
