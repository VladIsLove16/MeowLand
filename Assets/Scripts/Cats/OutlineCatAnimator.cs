using UnityEngine;
using UnityEngine.UI;

public class OutlineCatAnimator : CatAnimator
{
    private Sprite IdleSprite;
    private Sprite MeowSprite;
    private Sprite AngrySprite;
    private float AnimationLength=1f;
    public override void Init(CatInfoSO infoSO)
    {
        IdleSprite = infoSO.IdleSprite;
        MeowSprite = infoSO.MeowSprite;
        AngrySprite = infoSO.AngrySprite;
        Image.sprite = IdleSprite;
    }
    public override void StartAnimation(AnimationType type)
    {
        if (!DoAnimation) return;
        CancelInvoke("OnAnimationEnd");
        switch (type)
        {
            case AnimationType.Meow:
                Image.sprite = MeowSprite;
                break;
            case AnimationType.Angry:
                Image.sprite = AngrySprite;
                break;
        }
        Invoke("OnAnimationEnd", AnimationLength);
    }
    protected override void OnAnimationEnd()
    {
        Image.sprite = IdleSprite;
    }
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