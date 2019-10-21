﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour
{
    public SpriteRenderer mainBody;
    public SpriteRenderer effects;

    public SpriteRenderer shadow;
    public SpriteRenderer subShadow;
    public SpriteMask cutSubShadow;

    float mainBodyYPos;
    float yPos;
    float zPos;

    // Start is called before the first frame update
    void Start()
    {
        mainBodyYPos = mainBody.transform.position.y;
        yPos = transform.position.y;
        zPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainBody.transform.root.GetComponentInChildren<MovementHandler>().facingRight)
            transform.eulerAngles = new Vector3(90, 0, 0);
        else
            transform.eulerAngles = new Vector3(-90, 180, 0);

        transform.position = new Vector3(mainBody.transform.position.x, yPos, zPos + mainBody.transform.position.y - mainBodyYPos);
        shadow.sprite = mainBody.sprite;
        cutSubShadow.sprite = shadow.sprite;
        subShadow.sprite = effects.sprite;
    }
}
