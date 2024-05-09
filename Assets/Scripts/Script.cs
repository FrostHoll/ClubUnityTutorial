using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    bool isSelected = false;

    float speed = 5f;

    Light halo;

    void Start()
    {
        halo = GetComponent<Light>();
        halo.enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) && isSelected)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left);
        }
        if (Input.GetKey(KeyCode.D) && isSelected)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isSelected)
        {
            Instantiate(gameObject, transform.position + Random.onUnitSphere * 3f, Quaternion.identity);
        }
    }

    void OnMouseDown()
    {
        isSelected = !isSelected;
        halo.enabled = isSelected;
    }
}
