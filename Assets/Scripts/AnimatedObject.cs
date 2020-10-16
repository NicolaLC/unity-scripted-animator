using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedObject : MonoBehaviour
{
    private Animator m_animator;

    private List<AnimationStep> m_steps = new List<AnimationStep>();

    private AnimationStep m_activeStep;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public bool CanAnimate(bool log = true) {
        if(m_activeStep == null)
        {
            return true;
        }
        AnimatorStateInfo info = m_animator.GetCurrentAnimatorStateInfo(0);
        bool result = !m_activeStep.forceCompleteRun || (info.normalizedTime > 1 && info.IsName(m_activeStep.animationName));
#if UNITY_EDITOR
        if(log)
        {
            AnimationSettingsUIManager.SetTransitionEnabled(result);
        }
#endif
        return result;
    }

    public void SetAnimationSteps(List<AnimationStep> steps, bool log = true)
    {
        m_steps = steps;
        if(m_activeStep != null)
        {
            m_activeStep = m_steps.Find(e => e == m_activeStep);
            m_animator.speed = m_activeStep.animationSpeed;
        } else
        {
            m_activeStep = m_steps[0];
            m_animator.speed = m_activeStep.animationSpeed;
        }

#if UNITY_EDITOR
        if (log)
        {
            AnimationSettingsUIManager.SetAnimationStateOptions(m_steps);
        }
#endif
    }

    public void SetAnimatorState(AnimationStep step, bool log = true)
    {
        if(!CanAnimate()) { return; }
        m_activeStep = step;
        m_animator.speed = step.animationSpeed;
        m_animator.Play(step.animationName);
#if UNITY_EDITOR
        if(log)
        {
            AnimationSettingsUIManager.SetAnimationState(m_steps.IndexOf(step));
            AnimationSettingsUIManager.SetAnimationSpeed(m_animator.speed);
        }
#endif
    }
}
