using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range};
    public Type type;
    public int damage; // 공격력
    public float rate; // 공격 속도
    public int maxAmmo; // 최대 탄환
    public int curAmmo; // 현재 탄환

    public BoxCollider meleeArea; // 공격 범위
    public TrailRenderer trailEffect; // 공격 이펙트
    public Transform bulletPos; // 총알 위치
    public GameObject bullet; // 총알
    public Transform bulletCasePos; // 탄피 위치
    public GameObject bulletCase; // 탄피
    
    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");  // 실행중인 코루틴을 진행상황에 상관없이 정지시킴.
            StartCoroutine("Swing"); // 코루틴 함수는 그냥 쓰면 안되고 스타트코루틴 함수를 이용해야함
        }
        else if(type == Type.Range && curAmmo > 0)
        {
            curAmmo--;
            StartCoroutine("Shot");
        }
    }

    // 코루틴이란?
    // 일반적으로 상위 함수에서 하위 함수를 실행 후 남은 로직을 이어서 실행하는 것과 달리
    // 코루틴은 함수를 동시에 실행시키는 것을 의미한다.

    IEnumerator Swing()
    {
        // 1
        yield return new WaitForSeconds(0.1f); // return null인 경우로직 실행 후 1프레임 대기
                                               // 이 함수의 경우 n초 대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;

        // yield break;  이처럼 break를 쓰면 코루틴 탈출 기능
    }

    IEnumerator Shot()
    {
        // 발사
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;
        
        yield return null;


        // 탄피 배출
        GameObject instantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }
}
