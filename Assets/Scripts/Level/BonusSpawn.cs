using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawn : MonoBehaviour {

    [SerializeField]
    GameObject [] m_bonusArray;

    [Range(0, 100)]
    [SerializeField]
    short[] m_chanceArray;

    void Start ()
    {
        for (short count = 0; count < m_bonusArray.Length; count++)
        {
            if (Tools.ThrowOfDice(m_chanceArray[count]))
            {
                int nbrRand = Random.Range(0, m_bonusArray.Length);
                GameObject instance = Instantiate(m_bonusArray[nbrRand], transform.position, Quaternion.identity) as GameObject;
                instance.transform.parent = gameObject.transform;
                Debug.Log("Instance bonus " + m_bonusArray[nbrRand].name + " for " + gameObject.transform.parent.name);
                break;
            }
        }
    }
}
