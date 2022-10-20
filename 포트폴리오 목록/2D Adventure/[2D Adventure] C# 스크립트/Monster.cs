using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsule;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsule = GetComponent<CapsuleCollider2D>();
        Invoke("Think", 5); // 5초후에 Think함수를 실행한다.
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // platform check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); // 빔 시작위치, 방향, 색깔(R,G,B)
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Platform")); // 빔 시작위치, 방향, 크기(거리), 레이어 값
        if (rayHit.collider == null)
        {
            Turn();
        } 
    }

    void Think()
    {
        // set next active
        nextMove = Random.Range(-1, 2); // [-1, 2) 로 적용이라 1이 아닌 2로 기입하여 1이 되도록..

        // sprite animation
        anim.SetInteger("WalkSpeed", nextMove);

        // flip sprite
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1; // 0이 아닐때만 방향을 바꾼다

        // recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime); // 재귀를 통해 지속적으로 속도 변화주기
    }

    void Turn()
    {
        nextMove = nextMove * -1;
        spriteRenderer.flipX = nextMove == 1; // 0이 아닐때만 방향을 바꾼다
        CancelInvoke(); // 진행중이던 모든 인보크를 취소
            Invoke("Think", 5); // 다시 인보크 실행
    }

    // 몬스터의 죽음 함수
    public void OnDamaged()
    {
        // Sprite alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Sprite Filp y
        spriteRenderer.flipY = true;

        // Collider Disable
        capsule.enabled = false;

        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //Destroy - 비활성화 : 시간 차를 두고 실행
        Invoke("DeActive", 5);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
