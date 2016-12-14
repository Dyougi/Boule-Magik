using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateSliderValue : MonoBehaviour {

    [SerializeField]
    Text m_valueText;

    [SerializeField]
    MenuManager.e_sliderType m_sliderType;

    [SerializeField]
    OptionManager m_optionManager;

    Slider m_currentslider;

	void Start ()
    {
        m_currentslider = GetComponent<Slider>();
        switch (m_sliderType)
        {
            case MenuManager.e_sliderType.SPEEDSTART:
                m_valueText.text = m_optionManager.SpeedStart.ToString("0");
                m_currentslider.value = m_optionManager.SpeedStart;
                break;
            case MenuManager.e_sliderType.SPEEDUPDATE:
                m_valueText.text = m_optionManager.Speedupdate.ToString("0.00");
                m_currentslider.value = m_optionManager.Speedupdate;
                break;
        }
    }

    public void updateValue()
    {
        switch (m_sliderType)
        {
            case MenuManager.e_sliderType.SPEEDSTART:
                m_optionManager.SpeedStart = m_currentslider.value;
                m_valueText.text = m_currentslider.value.ToString("0");
                break;
            case MenuManager.e_sliderType.SPEEDUPDATE:
                m_optionManager.Speedupdate = m_currentslider.value;
                m_valueText.text = m_currentslider.value.ToString("0.00");
                break;
        }
    }
}
