using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //--- Mouse Picking 관련 변수
    Ray m_MousePos;
    RaycastHit hitInfo;
    LayerMask m_LayerMask = -1;

    [HideInInspector] public bool Iswalk = false;     //피킹 이동 OnOff
    Vector3 m_TargetPos = Vector3.zero; //최종 목표 위치
    Vector3 m_MoveDir = Vector3.zero;   //평명 진행 방향
    double m_MoveDurTime = 0.0f;        //목표점까지 도착하는데 걸리는 시간
    double m_AddTimeCount = 0.0f;       //누적 시간 카운트
    Vector3 m_CacLenVec = Vector3.zero; //이동 계산용 변수
    Quaternion m_TargetRot = Quaternion.identity; //회전 계산용 변수
    float m_RotSpeed = 7.0f;            //초당 7도 회전하려는 속도
    float m_MoveVelocity = 8.0f;        //이동속도
    //--- Mouse Picking 관련 변수

    // Start is called before the first frame update
    void Start()
    {
        m_LayerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        MousePickCheck();

        MousePickUpdate(); //마우스 클릭 이동

    }

    void MousePickCheck()
    {
        if (Input.GetMouseButtonDown(1) == true) //왼쪽 마우스 버튼 클릭시
        {
            m_MousePos = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(m_MousePos, out hitInfo, Mathf.Infinity,
                                                        m_LayerMask.value))
            {
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    MousePicking(hitInfo.point);  //지형 바닥 피킹일 때 교차점 인수로 넘겨 목표점 계산
                }
            }
        }//if(Input.GetMouseButtonDown(0) == true) //왼쪽 마우스 버튼 클릭시
    }

    void MousePicking(Vector3 a_PickVec, GameObject magictable = null)
    {
        a_PickVec.y = transform.position.y; //목표 위치
        Vector3 a_StartPos = transform.position;  //출발위치

        Vector3 a_CacLenVec = a_PickVec - a_StartPos;

        if (a_CacLenVec.magnitude < 0.5f) //너무 근거리 피킹은 스킵해 준다.
            return;

        m_TargetPos = a_PickVec;    //최종 목표 위치
        Iswalk = true;   //피킹 이동 OnOff

        m_MoveDir = a_CacLenVec.normalized;
        m_MoveDurTime = a_CacLenVec.magnitude / m_MoveVelocity; //도착하는데까지 걸리는 시간
        m_AddTimeCount = 0.0f;
    }

    void MousePickUpdate()
    {
        if (Iswalk == true)
        {
            m_CacLenVec = m_TargetPos - transform.position;
            m_CacLenVec.y = 0.0f;

            m_MoveDir = m_CacLenVec.normalized;

            //캐릭터를 이동방향으로 회전시키는 코드
            if (0.0001f < m_CacLenVec.magnitude)
            {
                m_TargetRot = Quaternion.LookRotation(m_MoveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                        m_TargetRot,
                                        Time.deltaTime * m_RotSpeed);
            }
            //캐릭터를 이동방향으로 회전시키는 코드

            m_AddTimeCount += Time.deltaTime;
            if (m_MoveDurTime <= m_AddTimeCount) //목표점에 도착한 것으로 판정한다.
            {
                Iswalk = false; //커서 마크 감추기
            }
            else
            {
                transform.position += m_MoveDir * Time.deltaTime * m_MoveVelocity;
            }

        }//if(m_IsPickMoveOnOff == true)
    }
}
