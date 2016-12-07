using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] Transform m_endEntity; // Landamrk of where the platform have to be destroyed on himself
    [SerializeField] Transform m_unspawnEntity; // Landmark of where the platform have to be destroyed in the world
    [SerializeField] Transform[] m_bonusSpawnPosition; // Array of where the bonus can be spawned on the platform
    [SerializeField] GameObject[] m_bonus; // List of bonus that can be instanciated
    [SerializeField] int widthSize; // Width size of the platform
    [SerializeField] int m_pointGived; // How much point gived when passed

    float m_speedTranslate; // Speed of the translate for this platform (for the scrolling purpose)
    bool m_isPaused; // Is game paused ? Then stop translate

    // UNITY METHODES

    void Start()
    {
        m_isPaused = false;
    }

    void Update()
    {
        if (!m_isPaused)
        {
            if (m_endEntity.position.x <= m_unspawnEntity.position.x)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().updatePoint(PointGived);
                Destroy(gameObject);
            }
            transform.Translate(new Vector3(-m_speedTranslate * Time.deltaTime, 0, 0));
        }
    }

    // PUBLIC METHODES

    public bool Pause
    {
        get
        {
            return m_isPaused;
        }

        set
        {
            m_isPaused = value;
        }
    }

    public int WidthSize
    {
        get
        {
            return widthSize;
        }
    }

    public int PointGived
    {
        get
        {
            return m_pointGived;
        }
    }

    public float Speed
    {
        get
        {
            return m_speedTranslate;
        }

        set
        {
            m_speedTranslate = value;
        }
    }

    public void initPlatform(float newSpeed, int chanceBonus)
    {
        m_speedTranslate = newSpeed;
        if (m_bonusSpawnPosition.Length != 0)
        {
            foreach (Transform tr in m_bonusSpawnPosition)
            {
                if (Tools.throwOfDice(chanceBonus))
                {
                    Debug.Log("m_bonus.Length = " + (m_bonusSpawnPosition.Length) + ", Instance bonus for " + gameObject.name);
                    int nbrRand = Random.Range(0, m_bonus.Length);
                    GameObject instance =  Instantiate(m_bonus[nbrRand], tr.position, Quaternion.identity) as GameObject;
                    instance.transform.parent = gameObject.transform;
                    //instance.GetComponent<Bonus>().initBonus((GameManager.e_bonusType)nbrRand, 5);
                }
            }
        }
    }
}
