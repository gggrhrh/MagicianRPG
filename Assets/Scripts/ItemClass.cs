using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public enum Item
{
    FireStone,
    WaterStone,
    GroundStone,
}

public class ItemClass
{
    public Item m_Item = Item.FireStone;
    public string m_Name = "���� ��";
    public Sprite m_IconImg = null;
    public int m_SellPrice = 0;

    public void SetType(Item item)
    {
        m_Item = item;

        if(item == Item.FireStone)
        {
            m_Name = "���� ��";
            m_SellPrice = 50;
        }
        else if(item == Item.WaterStone)
        {
            m_Name = "���� ��";
        }
        else if (item == Item.GroundStone)
        {
            m_Name = "���� ��";
        }
    }
}
