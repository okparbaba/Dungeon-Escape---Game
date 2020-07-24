using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 5.0f;

    private PlayerAnimation _anim;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        //assign handk
        _anim = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();        
    }
  
    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        
        if(Input.GetMouseButtonDown(0) && IsGrounded() == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutime());
        }
        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
        _anim.Move(move);
    }
    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8);
        if (hitInfo.collider !=null)
        {
            if(_resetJump == true)
                return false;
        }
        return true;
    }
    IEnumerator ResetJumpRoutime()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
    }
}
