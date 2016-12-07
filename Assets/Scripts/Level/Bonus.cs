using UnityEngine;

public class Bonus : MonoBehaviour {

    public delegate void BonusAction(GameManager.e_bonusType type);
    public static event BonusAction OnBonusOn;
    public static event BonusAction OnBonusOff;

    [SerializeField] GameManager.e_bonusType m_bonusType;
    [SerializeField] float m_bonusTime;
    float timer;
    bool isOn;
    
    // UNITY METHODES

    void Start()
    {
        isOn = false;
    }

    void Update()
    {
        if (isOn)
        {
            if (timer + m_bonusTime < Time.time)
            {
                if (OnBonusOff != null)
                    OnBonusOff(m_bonusType);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player" && !isOn)
        {
            if (OnBonusOn != null)
                OnBonusOn(m_bonusType);
            isOn = true;
            timer = Time.time;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            transform.parent = null;
        }
    }

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

    public void initBonus(GameManager.e_bonusType type, float time)
    {
        m_bonusType = type;
        m_bonusTime = time;
    }
}
