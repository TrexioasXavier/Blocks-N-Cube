using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceAbstract : MonoBehaviour
{
    public int m_effectRange;

    public int m_rateOfIncrease;
    public int m_increaseAmount;

    public bool m_currentingInEffect; // This is used so that heal and ammo effects would not be stacked

    public TEAM m_friendlyTeam;

}
