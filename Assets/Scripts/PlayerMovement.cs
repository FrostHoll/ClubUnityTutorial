using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    [SerializeField]
    private GameObject _sprite;

    private Rigidbody _rb;

    private Camera _cam;

    [SerializeField]
    private float _handOffset = 0.7f;

    private GameObject _hand;

    float isReversed;

    public void Init(GameObject hand)
    {
        _hand = hand;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 movement = (transform.right * h + transform.forward * v).normalized * _moveSpeed;

        Move(movement);

        Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 vec = mousePos - transform.position;
        vec.y = 0f;
        vec = vec.normalized * _handOffset;

        MoveHand(vec);

        isReversed = Mathf.Sign(Vector3.Dot(transform.right, vec));

        ReverseSprite();
    }

    private void Move(Vector3 movement)
    {
        if (movement != Vector3.zero) 
        {
            _rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
        }
    }

    private void MoveHand(Vector3 mousePos)
    {        
        _hand.transform.SetLocalPositionAndRotation(new Vector3(mousePos.x, _hand.transform.position.y, mousePos.z), 
            Quaternion.LookRotation(mousePos));
    }

    private void ReverseSprite()
    {
        if (isReversed != Mathf.Sign(_sprite.transform.localScale.x))
            _sprite.transform.localScale = new Vector3(isReversed * Mathf.Abs(_sprite.transform.localScale.x),
            _sprite.transform.localScale.y, _sprite.transform.localScale.z);
        if (isReversed != Mathf.Sign(_hand.transform.localScale.x))
            _hand.transform.localScale = new Vector3(isReversed * Mathf.Abs(_hand.transform.localScale.x),
            _hand.transform.localScale.y, _hand.transform.localScale.z);
    }
}
