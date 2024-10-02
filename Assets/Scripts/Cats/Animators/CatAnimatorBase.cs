using UnityEngine;
public abstract class  CatAnimatorBase : MonoBehaviour
{
    protected Sprite IdleSprite;
    protected Sprite FlashingSprite;
    protected Sprite MeowSprite;
    protected Sprite AngrySprite;
    protected int AnimationLength = 1000;
    protected bool playingAnim;
    protected float timeBetweenFlashing;
    protected SpriteSetter SpriteSetter;

    protected float GetFlashingDelay()
    {
        return Random.Range(1.5f, 4f);
    }
    public virtual void Init(CatInfoSO infoSO)
    {
        IdleSprite = infoSO.IdleSprite;
        MeowSprite = infoSO.MeowSprite;
        AngrySprite = infoSO.AngrySprite;
        FlashingSprite = infoSO.FlashingSprite;
    }
    protected abstract void FlashingAnim();
    public abstract void StartAnimation(AnimationType type);
    protected abstract void OnAnimationEnd();
}

//public class AnimatorCatAnimator : CatAnimator
//{
//    //private Animator animator;

//    //public void Awake()
//    //{
//    //    animator = GetComponent<Animator>();

//    //}
//    //public void SetAngry()
//    //{
//    //    StartCoroutine(AnimationAngry());
//    //}
//    //private IEnumerator AnimationAngry()
//    //{
//    //    animator.SetBool("IsAngry", true);
//    //    yield return new WaitForSeconds(1f);
//    //    animator.SetBool("IsAngry", false);
//    //}
//    //private void PlayAnimation()
//    //{
//    //    if (!trigger_mode)
//    //    {
//    //        StartCoroutine(Animation_Bool());
//    //    }
//    //    else
//    //    {
//    //        StartCoroutine(Animation_Trigger());
//    //    }
//    //}
//    //private IEnumerator Animation_Bool()
//    //{
//    //    animator.SetBool("IsMeowing", true);
//    //    float meowingLength = animator.GetCurrentAnimatorStateInfo(0).length;
//    //    yield return new WaitForSeconds(1f);
//    //    animator.SetBool("IsMeowing", false);
//    //}
//    //private IEnumerator Animation_Trigger()
//    //{
//    //    animator.SetTrigger("Meow");
//    //    yield return new WaitForSeconds(1f);
//    //}
//}