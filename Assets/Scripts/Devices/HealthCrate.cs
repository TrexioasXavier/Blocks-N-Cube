using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCrate : DeviceAbstract
{



    private void OnTriggerEnter(Collider other)
    {
        PlayerController ch = other.gameObject.GetComponent<PlayerController>();

        if (ch != null)
        {
            if (ch.m_team == this.m_friendlyTeam && ch.m_health < ch.m_maxHealth && m_currentingInEffect == false)
            {
                ch.m_health += m_increaseAmount;
                m_currentingInEffect = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterController ch = other.gameObject.GetComponent<CharacterController>();
        if (ch != null)
            m_currentingInEffect = false;
    }

}
