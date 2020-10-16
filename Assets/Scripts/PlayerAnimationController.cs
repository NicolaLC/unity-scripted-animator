using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatedObject))]
public class PlayerAnimationController : MonoBehaviour
{
    // Animation ranges
    [Range(0.1f, 1f)]
    public float m_pc_idle_speed = .1f;
    [Range(0.1f, 1f)]
    public float m_pc_walk_speed = .15f;
    [Range(0.1f, 1f)]
    public float m_pc_jump_speed = .1f;
    [Range(0.1f, 1f)]
    public float m_pc_fall_speed = 1f;
    [Range(0.1f, 1f)]
    public float m_pc_ground_speed = .2f;
    [Range(0.1f, 1f)]
    public float m_pc_hit_speed = .1f;

    // Component caching
    private AnimatedObject m_animatedObject = null;
    private PlayerController m_playerController = null;
    // Component variables
    private List<AnimationStep> m_steps = new List<AnimationStep> {
        new AnimationStep("pc_idle", .1f),
        new AnimationStep("pc_walk", .15f),
        new AnimationStep("pc_jump", .1f, true),
        new AnimationStep("pc_fall"),
        new AnimationStep("pc_ground", .2f, true),
        new AnimationStep("pc_hit", 1f, true)
    };

    private void Awake()
    {
        m_animatedObject = GetComponent<AnimatedObject>();
        m_playerController = GetComponent<PlayerController>();
    }


    private void Start()
    {
        SetAnimations();
    }

    private void Update()
    {
        Animate();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        SetAnimations();
    }
#endif

    private void SetAnimations()
    {
        if(m_animatedObject == null) { return; }
        m_steps[0].SetAnimationSpeed(m_pc_idle_speed);
        m_steps[1].SetAnimationSpeed(m_pc_walk_speed);
        m_steps[2].SetAnimationSpeed(m_pc_jump_speed);
        m_steps[3].SetAnimationSpeed(m_pc_fall_speed);
        m_steps[4].SetAnimationSpeed(m_pc_ground_speed);
        m_steps[5].SetAnimationSpeed(m_pc_hit_speed);
        m_animatedObject.SetAnimationSteps(m_steps);
    }

    private void Animate()
    {
        bool jumping = m_playerController.m_isGrounded && Input.GetAxisRaw("Jump") != 0f;
        bool walking = m_playerController.m_isGrounded && Input.GetAxisRaw("Horizontal") != 0f;

        if (jumping)
        {
            m_animatedObject.SetAnimatorState(m_steps[2]);
        }
        else
        {
            if (m_playerController.m_wasNotGrounded)
            {
                m_animatedObject.SetAnimatorState(m_steps[4]);
                m_playerController.m_wasNotGrounded = false;
                return;
            }
            if (!m_playerController.m_isGrounded)
            {
                m_animatedObject.SetAnimatorState(m_steps[3]);
                return;
            }
            m_animatedObject.SetAnimatorState(m_steps[walking ? 1 : 0]);
        }
    }

    public void DamageAnimation()
    {
        m_animatedObject.SetAnimatorState(m_steps[5]);
    }

}
