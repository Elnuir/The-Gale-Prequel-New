using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Pathfinding;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{

    public string VirtualCumeraName;
    public float Speed;
    public float MaxDistance;

    private CinemachineVirtualCamera _cumera;


    public void Start()
    {
        _cumera = GameObject.Find(VirtualCumeraName).GetComponent<CinemachineVirtualCamera>();
        _cumera.Follow = transform;
    }

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float positionX = transform.localPosition.x;
        float speed = Speed * Time.deltaTime;


        if (inputX > 0.01f && positionX + speed <= MaxDistance)
            transform.localPosition = new Vector2(positionX + speed, transform.localPosition.y);

        if (inputX < -0.01f && positionX - speed >= -MaxDistance)
            transform.localPosition = new Vector2(positionX - speed, transform.localPosition.y);

    }

}
