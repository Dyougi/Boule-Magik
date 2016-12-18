using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateToggleValue : MonoBehaviour {

    [SerializeField]
    MenuManager.e_toggleType m_toggleType;

    OptionManager m_optionManager;

    Toggle m_toggle;

	// Use this for initialization
	void Start ()
    {
        m_optionManager = OptionManager.Instance;
        m_toggle = GetComponent<Toggle>();
        switch (m_toggleType)
        {
            case MenuManager.e_toggleType.MUSIC:
                m_toggle.isOn = m_optionManager.Music == 1 ? true : false;
                Debug.Log("START music set to " + m_optionManager.Music);
                break;
            case MenuManager.e_toggleType.SOUND:
                m_toggle.isOn = m_optionManager.Sound == 1 ? true : false;
                Debug.Log("START sound set to " + m_optionManager.Sound);
                break;
        }
    }

    public void updateValue()
    {
        int isOnInt = m_toggle.isOn ? 1 : 0;

        switch (m_toggleType)
        {
            case MenuManager.e_toggleType.MUSIC:
                m_optionManager.Music = isOnInt;
                Debug.Log("UPDATE music set to " + m_optionManager.Music);
                break;
            case MenuManager.e_toggleType.SOUND:
                m_optionManager.Sound = isOnInt;
                Debug.Log("UPDATE sound set to " + m_optionManager.Sound);
                break;
        }
    }
}
