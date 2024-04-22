using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject m_Player = null;
    Vector3 m_TargetPos = Vector3.zero;

    Vector3 m_CamPos = new Vector3(3.0f, 6.0f, -6.0f);

    // Update is called once per frame
    void LateUpdate()
    {
        if (m_Player == null)
            return;

        m_TargetPos = m_Player.transform.position;

        transform.position = m_TargetPos + m_CamPos;
    }
}
