using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawn : MonoBehaviour {

    [SerializeField]
    GameObject [] m_bonusArray;

    [Range(0, 100)]
    [SerializeField]
    int m_chanceBonus;

    void Start () {
        if (m_bonusArray.Length > 0)
        {
            if (Tools.ThrowOfDice(m_chanceBonus))
            {
                int nbrRand = Random.Range(0, m_bonusArray.Length);
                GameObject instance = Instantiate(m_bonusArray[nbrRand], transform.position, Quaternion.identity) as GameObject;
                instance.transform.parent = gameObject.transform;
                Debug.Log("Instance bonus " + m_bonusArray[nbrRand].name + " for " + gameObject.transform.parent.name);
            }
        }
    }
}
