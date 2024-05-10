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
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        if (xAxis != 0 && isSelected)
        {
            Move(new Vector3(xAxis, 0f, 0f));
        }
        if (zAxis != 0 && isSelected)
        {
            Move(new Vector3(0f, 0f, zAxis));
        }
        if (Input.GetKeyDown(KeyCode.Space) && isSelected)
        {
            Instantiate(gameObject, transform.position + Random.onUnitSphere * 3f, Quaternion.identity);
        }
    }

    private void Move(Vector3 directon)
    {
        transform.Translate(speed * Time.deltaTime * directon);
    }

    void OnMouseDown()
    {
        CameraMotion.Target = this;
    }

    public void ToogleSelected()
    {
        isSelected = !isSelected;
        halo.enabled = isSelected;
    }
}
