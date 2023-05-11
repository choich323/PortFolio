using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range};
    public Type type;
    public int damage; // ���ݷ�
    public float rate; // ���� �ӵ�
    public int maxAmmo; // �ִ� źȯ
    public int curAmmo; // ���� źȯ

    public BoxCollider meleeArea; // ���� ����
    public TrailRenderer trailEffect; // ���� ����Ʈ
    public Transform bulletPos; // �Ѿ� ��ġ
    public GameObject bullet; // �Ѿ�
    public Transform bulletCasePos; // ź�� ��ġ
    public GameObject bulletCase; // ź��
    
    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");  // �������� �ڷ�ƾ�� �����Ȳ�� ������� ������Ŵ.
            StartCoroutine("Swing"); // �ڷ�ƾ �Լ��� �׳� ���� �ȵǰ� ��ŸƮ�ڷ�ƾ �Լ��� �̿��ؾ���
        }
        else if(type == Type.Range && curAmmo > 0)
        {
            curAmmo--;
            StartCoroutine("Shot");
        }
    }

    // �ڷ�ƾ�̶�?
    // �Ϲ������� ���� �Լ����� ���� �Լ��� ���� �� ���� ������ �̾ �����ϴ� �Ͱ� �޸�
    // �ڷ�ƾ�� �Լ��� ���ÿ� �����Ű�� ���� �ǹ��Ѵ�.

    IEnumerator Swing()
    {
        // 1
        yield return new WaitForSeconds(0.1f); // return null�� ������ ���� �� 1������ ���
                                               // �� �Լ��� ��� n�� ���
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;

        // yield break;  ��ó�� break�� ���� �ڷ�ƾ Ż�� ���
    }

    IEnumerator Shot()
    {
        // �߻�
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;
        
        yield return null;


        // ź�� ����
        GameObject instantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }
}
