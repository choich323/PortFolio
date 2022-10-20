using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{

    public GameObject missile;
    public Transform missilePortA;
    public Transform missilePortB;
    public bool isLook;

    Vector3 lookVec;
    Vector3 tauntVec;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        nav.isStopped = true;
        StartCoroutine(Think());
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            StopAllCoroutines(); // 모든 코루틴 중지
            return;
        }

        if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);
        }
        else
            nav.SetDestination(tauntVec);
    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f); // 발동 시간을 통해 난이도 조절 가능

        int ranAction = Random.Range(0, 5); // 0~4 사이의 수 랜덤으로
        switch (ranAction)
        {
            case 0:
            case 1:
                // 0과 1의 경우. 여러 케이스를 합쳐서 확률 배분이 가능
                // 미사일 발사 패턴
                StartCoroutine(MissileShot());
                break;
            case 2:
            case 3:
                // 돌 굴러가는 패턴
                StartCoroutine(RockShot());
                break;
            case 4:
                // 점프 공격 패턴
                StartCoroutine(Taunt());
                break;
        }
    }

    IEnumerator MissileShot()
    {
        anim.SetTrigger("doShot");
        yield return new WaitForSeconds(0.2f);
        GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        BossMissile bossMissileA = instantMissileA.GetComponent<BossMissile>();
        bossMissileA.target = target;

        yield return new WaitForSeconds(0.3f);
        GameObject instantMissileB = Instantiate(missile, missilePortB.position, missilePortB.rotation);
        BossMissile bossMissileB = instantMissileB.GetComponent<BossMissile>();
        bossMissileB.target = target;

        yield return new WaitForSeconds(2f);

        StartCoroutine(Think()); // 행동이 끝나면 다음 행동으로 넘어감
    }

    IEnumerator RockShot()
    {
        isLook = false;
        anim.SetTrigger("doBigShot");
        Instantiate(bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(3f);

        isLook = true;
        StartCoroutine(Think());
    }
    IEnumerator Taunt()
    {
        tauntVec = target.position + lookVec;

        isLook = false; // 뛸 때 시선이 플레이어를 추적하지 않도록
        nav.isStopped = false; // 추격도 잠시 정지
        boxCollider.enabled = false; // 뛸 때 플레이어를 밀어내지 않도록
        anim.SetTrigger("doTaunt");

        yield return new WaitForSeconds(1.5f);
        meleeArea.enabled = true; // 공격 범위 활성화

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false; // 공격이 끝난 후 비활성화

        yield return new WaitForSeconds(1f);

        isLook = true;
        nav.isStopped = true;
        boxCollider.enabled = true;
        StartCoroutine(Think());
    }
}
