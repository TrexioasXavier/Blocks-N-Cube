using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : DeviceAbstract
{



    private void OnTriggerEnter(Collider other)
    {
        PlayerController ch = other.gameObject.GetComponent<PlayerController>();

        if (ch != null)
        {
            if (ch.m_team == this.m_friendlyTeam 
                && ch.m_weapons[0].m_ammo < ch.m_weapons[0].m_maxAmmo 
                && ch.m_weapons[1].m_ammo < ch.m_weapons[1].m_maxAmmo
                && m_currentingInEffect == false)
            {
                ch.m_weapons[0].m_ammo += m_increaseAmount;
                ch.m_weapons[1].m_ammo += m_increaseAmount;

                m_currentingInEffect = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController ch = other.gameObject.GetComponent<PlayerController>();

        if (ch != null)
            m_currentingInEffect = false;
    }

}
