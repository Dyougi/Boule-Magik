using UnityEngine;

public class Bonus : MonoBehaviour {

    [SerializeField] GameManager.e_bonusType m_bonusType;
    [SerializeField] float m_bonusTime;

    // PUBLIC METHODES

    public float BonusTime
    {
        get
        {
            return m_bonusTime;
        }
    }

    public GameManager.e_bonusType BonusType
    {
        get
        {
            return m_bonusType;
        }
    }

    public void InitBonus(GameManager.e_bonusType type, float time)
    {
        m_bonusType = type;
        m_bonusTime = time;
    }
}
