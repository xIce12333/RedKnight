using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    //  プレイヤーキャラクターのアニメーション用
    public enum State {Idle, Run, Jump, Dash, Hurt, Die, Attack, Win};
    protected State currentState;
    protected Animator animator;
    public virtual void Start()
    {   
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(State newState)
    {
        // prevent looping the same animation   同じアニメーションをループしないようにします
        if (currentState == newState) return;

        // play the animation
        animator.Play(newState.ToString());

        // reassign the current state;     現在のアニメーション状態を記録します
        currentState = newState;
    }
}
