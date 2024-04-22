using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //--- Mouse Picking ���� ����
    Ray m_MousePos;
    RaycastHit hitInfo;
    LayerMask m_LayerMask = -1;

    [HideInInspector] public bool Iswalk = false;     //��ŷ �̵� OnOff
    Vector3 m_TargetPos = Vector3.zero; //���� ��ǥ ��ġ
    Vector3 m_MoveDir = Vector3.zero;   //��� ���� ����
    double m_MoveDurTime = 0.0f;        //��ǥ������ �����ϴµ� �ɸ��� �ð�
    double m_AddTimeCount = 0.0f;       //���� �ð� ī��Ʈ
    Vector3 m_CacLenVec = Vector3.zero; //�̵� ���� ����
    Quaternion m_TargetRot = Quaternion.identity; //ȸ�� ���� ����
    float m_RotSpeed = 7.0f;            //�ʴ� 7�� ȸ���Ϸ��� �ӵ�
    float m_MoveVelocity = 8.0f;        //�̵��ӵ�
    //--- Mouse Picking ���� ����

    // Start is called before the first frame update
    void Start()
    {
        m_LayerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        MousePickCheck();

        MousePickUpdate(); //���콺 Ŭ�� �̵�

    }

    void MousePickCheck()
    {
        if (Input.GetMouseButtonDown(1) == true) //���� ���콺 ��ư Ŭ����
        {
            m_MousePos = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(m_MousePos, out hitInfo, Mathf.Infinity,
                                                        m_LayerMask.value))
            {
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    MousePicking(hitInfo.point);  //���� �ٴ� ��ŷ�� �� ������ �μ��� �Ѱ� ��ǥ�� ���
                }
            }
        }//if(Input.GetMouseButtonDown(0) == true) //���� ���콺 ��ư Ŭ����
    }

    void MousePicking(Vector3 a_PickVec, GameObject magictable = null)
    {
        a_PickVec.y = transform.position.y; //��ǥ ��ġ
        Vector3 a_StartPos = transform.position;  //�����ġ

        Vector3 a_CacLenVec = a_PickVec - a_StartPos;

        if (a_CacLenVec.magnitude < 0.5f) //�ʹ� �ٰŸ� ��ŷ�� ��ŵ�� �ش�.
            return;

        m_TargetPos = a_PickVec;    //���� ��ǥ ��ġ
        Iswalk = true;   //��ŷ �̵� OnOff

        m_MoveDir = a_CacLenVec.normalized;
        m_MoveDurTime = a_CacLenVec.magnitude / m_MoveVelocity; //�����ϴµ����� �ɸ��� �ð�
        m_AddTimeCount = 0.0f;
    }

    void MousePickUpdate()
    {
        if (Iswalk == true)
        {
            m_CacLenVec = m_TargetPos - transform.position;
            m_CacLenVec.y = 0.0f;

            m_MoveDir = m_CacLenVec.normalized;

            //ĳ���͸� �̵��������� ȸ����Ű�� �ڵ�
            if (0.0001f < m_CacLenVec.magnitude)
            {
                m_TargetRot = Quaternion.LookRotation(m_MoveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                        m_TargetRot,
                                        Time.deltaTime * m_RotSpeed);
            }
            //ĳ���͸� �̵��������� ȸ����Ű�� �ڵ�

            m_AddTimeCount += Time.deltaTime;
            if (m_MoveDurTime <= m_AddTimeCount) //��ǥ���� ������ ������ �����Ѵ�.
            {
                Iswalk = false; //Ŀ�� ��ũ ���߱�
            }
            else
            {
                transform.position += m_MoveDir * Time.deltaTime * m_MoveVelocity;
            }

        }//if(m_IsPickMoveOnOff == true)
    }
}
