using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] float jumpPower;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if(x != 0f)
            rigid.velocity = new Vector2(x * 4, rigid.velocity.y);

        if (Input.GetKeyDown(KeyCode.Z))
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
}
