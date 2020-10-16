using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaShellController : MonoBehaviour
{

    // Animation ranges
    [Range(0.1f, 1f)]
    public float m_ss_opening_speed = .5f;
    // Component caching
    private AnimatedObject m_animatedObject = null;

    // Component variables
    private List<AnimationStep> m_steps = new List<AnimationStep> {
        new AnimationStep("ss_idle"),
        new AnimationStep("ss_opening", .5f, true)
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
        m_steps[1].SetAnimationSpeed(m_ss_opening_speed);
        m_animatedObject.SetAnimationSteps(m_steps, false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            m_animatedObject.SetAnimatorState(m_steps[1], false);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_animatedObject.SetAnimatorState(m_steps[0], false);
        }
    }
}
