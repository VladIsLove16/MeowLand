using UnityEngine;
using UnityEngine.UI;

public class OutlineCatAnimator : CatAnimator
{
    private Sprite IdleSprite;
    private Sprite FlashingSprite;
    private Sprite MeowSprite;
    private Sprite AngrySprite;
    private float AnimationLength=1f;
    private bool playingAnim;
    private float timeBetweenFlashing;
    public override void Init(CatInfoSO infoSO)
    {
        Awake();
        IdleSprite = infoSO.IdleSprite;
        MeowSprite = infoSO.MeowSprite;
        AngrySprite = infoSO.AngrySprite;
        FlashingSprite = infoSO.FlashingSprite;
        Image.sprite = IdleSprite;
    }
    private void Update()
    {
        if (!playingAnim)
        {
            timeBetweenFlashing -= Time.deltaTime;
            if (timeBetweenFlashing < 0)
            {
                CancelInvoke("OnAnimationEnd");
                timeBetweenFlashing = 2f;
                playingAnim = true;
                Image.sprite = FlashingSprite;
                Invoke("OnAnimationEnd", AnimationLength);
            }
        }
    }
    public override void StartAnimation(AnimationType type)
    {
        if (!DoAnimation) return;
        CancelInvoke("OnAnimationEnd");
        playingAnim = true;
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
        playingAnim = false;
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