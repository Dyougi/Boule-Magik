using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateToggleValue : MonoBehaviour {

    [SerializeField]
    OptionManager m_optionManager;

    [SerializeField]
    MenuManager.e_toggleType m_toggleType;

    Toggle m_toggle;

	// Use this for initialization
	void Start ()
    {
        m_toggle = GetComponent<Toggle>();
        switch (m_toggleType)
        {
            case MenuManager.e_toggleType.MUSIC:
                m_toggle.isOn = m_optionManager.Music == 1 ? true : false;
                break;
            case MenuManager.e_toggleType.SOUND:
                m_toggle.isOn = m_optionManager.Sound == 1 ? true : false;
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
                break;
            case MenuManager.e_toggleType.SOUND:
                m_optionManager.Sound = isOnInt;
                break;
        }
    }
}
