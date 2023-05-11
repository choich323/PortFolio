using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Walk : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;

    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;

    public float maxSpeed;
    public float jumpPower;
    
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D capsule;
    AudioSource audioSource;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "Jump":
                audioSource.clip = audioJump;
                break;
            case "Attack":
                audioSource.clip = audioAttack;
                break;
            case "Damaged":
                audioSource.clip = audioDamaged;
                break;
            case "Item":
                audioSource.clip = audioItem;
                break;
            case "Die":
                audioSource.clip = audioDie;
                break;
            case "Finish":
                audioSource.clip = audioFinish;
                break;
        }
    }

    void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump") && !anim.GetBool("IsJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            anim.SetBool("IsJumping", true);

            //sound
            PlaySound("Jump");
            audioSource.Play();
        }

        // Stop speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); // 0.5를 계속 곱하는 건 속도를 0에 수렴시키는 것
        }

        // 좌우 시점 변환
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // 애니메이터 전환
        if (rigid.velocity.normalized.x == 0)
            anim.SetBool("IsWalking", false);
        else
            anim.SetBool("IsWalking", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Moving
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h * 5, ForceMode2D.Impulse);

        // Moving Speed
        if (rigid.velocity.x > maxSpeed) // right max
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

        else if (rigid.velocity.x < maxSpeed * (-1)) // left max : 좌측으로 가는건 음수의 속도값을 갖기 때문에.
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);


        // Ray cast - Landig flatform - 빔을 쏜다
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); // 빔 시작위치, 방향, 색깔(R,G,B)
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform")); // 빔 시작위치, 방향, 크기(거리), 레이어 값
            if (rayHit.collider != null) // 빔을 쐈는데 뭔가 있다면
                if (rayHit.distance < 0.5f)
                    anim.SetBool("IsJumping", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) // 적과 충돌시 함수
    {
        if(collision.gameObject.tag == "Enemy")
        {
            // Attack
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else
                OnDamaged(collision.transform.position);
        }
    }

    void OnAttack(Transform enemy)
    {
        //Point
        gameManager.stagePoint += 100;

        //Reaction
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //Enemy die
        Monster monster = enemy.GetComponent<Monster>();
        monster.OnDamaged();

        //sound
        PlaySound("Attack");
        audioSource.Play();
    }


    void OnDamaged(Vector2 targetPos) // 무적모드 함수
    {
        // Health down
        gameManager.HealthDown();

        // 무적 상태 진입
        gameObject.layer = 11;

        // 무적 표시(색깔)
        spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 투명하게 만들기

        //sound
        PlaySound("Damaged");
        audioSource.Play();

        // 튕겨 나가는 효과
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        // animation
        anim.SetTrigger("doDamaged");

        // 일정 시간 후 무적 종료
        Invoke("OffDamaged", 1.75f);
    }

    void OffDamaged() // 무적 해제 함수
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            // Point
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");

            if (isBronze)
                gameManager.stagePoint += 50;
            else if (isSilver)
                gameManager.stagePoint += 100;
            else if (isGold)
                gameManager.stagePoint += 300;

            //sound
            PlaySound("Item");
            audioSource.Play();

            // Deactive Item
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Finish")
        {
            // Next Stage
            gameManager.NextStage();

            //sound
            PlaySound("Finish");
        }
    }

    public void OnDie()
    {
        // Sprite alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Sprite Filp y
        spriteRenderer.flipY = true;

        // Collider Disable
        capsule.enabled = false;

        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //sound
        PlaySound("Die");
        audioSource.Play();
    }

    public void VelocitiyZero()
    {
        rigid.velocity = Vector2.zero;
    }
}
