using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    // Animation ranges
    [Range(0.1f, 1f)]
    public float m_sword_idle_speed = 1f;
    // Component caching
    private AnimatedObject m_animatedObject = null;
    // Component variables
    private List<AnimationStep> m_steps = new List<AnimationStep> {
        new AnimationStep("sword_idle")
    };
    private void Awake()
    {
        m_animatedObject = GetComponent<AnimatedObject>();
    }

    private void Start()
    {
        SetAnimations();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        SetAnimations();
    }
#endif

    private void SetAnimations()
    {
        if (m_animatedObject == null) { return; }
        m_steps[0].SetAnimationSpeed(m_sword_idle_speed);
        m_animatedObject.SetAnimationSteps(m_steps, false);
    }
}
