using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

    GameManager.e_bonusType m_bonusType;
    float time;

    // PUBLIC METHODES

    public GameManager.e_bonusType BonusType
    {
        get
        {
            return m_bonusType;
        }
    }

    public void initBonus(GameManager.e_bonusType type)
    {
        m_bonusType = type;
    }
}
