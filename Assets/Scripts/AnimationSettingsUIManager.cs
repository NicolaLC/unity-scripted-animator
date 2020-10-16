using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationSettingsUIManager : MonoBehaviour
{
    private static AnimationSettingsUIManager m_instance = null;

    [SerializeField]
    private Slider slider_animationSpeed = null;

    [SerializeField]
    private Dropdown dropdown_animationState = null;

    [SerializeField]
    private Toggle toogle_transitionEnabled = null;

    private void Awake()
    {
        if(!m_instance)
        {
            m_instance = this;
        }
    }

    public static void SetAnimationSpeed(float amount)
    {
        m_instance.slider_animationSpeed.value = amount;
    }

    public static void SetTransitionEnabled(bool enabled)
    {
        m_instance.toogle_transitionEnabled.isOn = enabled;
    }

    public static void SetAnimationState(int stateIndex)
    {
        m_instance.dropdown_animationState.value = stateIndex;   
    }

    public static void SetAnimationStateOptions(List<AnimationStep> steps)
    {
        m_instance.dropdown_animationState.options.Clear();
        foreach(AnimationStep step in steps)
        {
            m_instance.dropdown_animationState.options.Add(new Dropdown.OptionData(step.animationName));
        }
    }
}
