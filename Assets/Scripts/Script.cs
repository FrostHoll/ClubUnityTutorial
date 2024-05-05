using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    int health = 10;

    void OnMouseEnter()
    {
        health = 10;
        Debug.Log("�������� �������������!");
    }

    void OnMouseDown()
    {
        if (health > 0)
        {
            health--;
            if (health == 0)
                Debug.Log("������!");
            else 
                Debug.Log($"HP: {health}");
        }
    }

    void OnMouseExit()
    {
        Debug.Log($"���������� ��������: {health}");
    }
}
