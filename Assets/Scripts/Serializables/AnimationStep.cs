using UnityEngine;
using UnityEditor;

[System.Serializable]
public class AnimationStep
{
    public string animationName;
    public float animationSpeed;
    public bool forceCompleteRun;

    public AnimationStep(string animationName, float animationSpeed = 1f, bool forceCompleteRun = false)
    {
        if(animationSpeed > 1f)
        {
            Debug.LogWarning("Warning - max value for animation speed is 1");
        }
        this.animationName = animationName;
        this.animationSpeed = animationSpeed;
        this.forceCompleteRun = forceCompleteRun;
    }

    override public string ToString()
    {
        return animationName + " - " + animationSpeed + " - " + forceCompleteRun;
    }

    public void SetAnimationSpeed(float amount)
    {
        animationSpeed = amount;
    }
}