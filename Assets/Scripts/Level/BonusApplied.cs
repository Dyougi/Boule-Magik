using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusApplied {

    public float m_bonusTime;
    public float m_startBonusTime;
    public GameManager.e_bonusType m_bonusType;

    public BonusApplied(float bonusTime, GameManager.e_bonusType bonusType)
    {
        m_bonusTime = bonusTime;
        m_startBonusTime = MyTimer.Instance.TotalTime;
        m_bonusType = bonusType;
    }
}
