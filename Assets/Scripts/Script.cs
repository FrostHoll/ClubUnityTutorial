using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    int health = 10;

    void OnMouseEnter()
    {
        health = 10;
        Debug.Log("Здоровье восстановлено!");
    }

    void OnMouseDown()
    {
        if (health > 0)
        {
            health--;
            if (health == 0)
                Debug.Log("Победа!");
            else 
                Debug.Log($"HP: {health}");
        }
    }

    void OnMouseExit()
    {
        Debug.Log($"Оставшееся здоровье: {health}");
    }
}
