using UnityEngine;

public class LoopAnimation : MonoBehaviour
{
    [SerializeField] string stateName;
    [SerializeField] AnimationClip animationClip;
    [SerializeField] int layer = 0;
    [SerializeField] int frame;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void LoopToFrame()
    {
        float frameTime = frame / animationClip.frameRate;
        float normalizedFrameTime = frameTime / animationClip.length;
        animator.Play(stateName, layer, normalizedFrameTime);
    }
}
