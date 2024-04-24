using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Mgr : MonoBehaviour
{
    //------ InvenRoot
    public GameObject m_InvenRoot = null;
    bool OnOffInven = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) == true)
        {
            OnOffInven = !OnOffInven;
        }

        if (m_InvenRoot != null)
            m_InvenRoot.SetActive(OnOffInven);
    }
}
