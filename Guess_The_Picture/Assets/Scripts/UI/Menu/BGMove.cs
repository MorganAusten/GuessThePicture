using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{   //Vitesse de loop
    [SerializeField] float xDeltaMultiplier;
    [SerializeField] float yDeltaMultiplier;

    //distance de déplacement
    [SerializeField] float xTransformMultiplier;
    [SerializeField] float yTransformMultiplier;

    float moveOnX;
    float moveOnY;
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if (moveOnX >= 360)
            moveOnX -= Time.deltaTime * xDeltaMultiplier;
        else
            moveOnX += Time.deltaTime * xDeltaMultiplier;

        if (moveOnY >= 360)
            moveOnY -= Time.deltaTime * yDeltaMultiplier;
        else
            moveOnY += Time.deltaTime * yDeltaMultiplier;
        transform.position = new Vector3(transform.position.x + Mathf.Sin(moveOnX) * xTransformMultiplier, transform.position.y +Mathf.Cos(moveOnY) * yTransformMultiplier);
    }
}
