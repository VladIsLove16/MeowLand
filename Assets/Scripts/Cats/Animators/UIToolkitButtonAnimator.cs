using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
public class UIToolkitButtonAnimator : CatAnimatorBase
{
    Button Button;
    private float currentAnimTime;
    public UIToolkitButtonAnimator()
    { 
    }
    public UIToolkitButtonAnimator(CatInfoSO infoSO, Button button)
    {
        Init(infoSO);
        Init(button);
    }
    public  void Init(Button button)
    {
        this.Button = button;
        SpriteSetter = new VisualElementSpriteSetter(button);
    }
    public void Update()
    {
        if(!playingAnim)
            FlashingAnim();
        else if (currentAnimTime<0)
            OnAnimationEnd();
        else
            currentAnimTime -= Time.deltaTime;
    }
    public override void StartAnimation(AnimationType type)
    {
        playingAnim = true;
        switch (type)
        {
            case AnimationType.Meow:
                SpriteSetter.Set(MeowSprite);
                break;
            case AnimationType.Angry:
                SpriteSetter.Set(AngrySprite);
                break;
        }
        currentAnimTime = AnimationLength + 300;
    }
    protected override void OnAnimationEnd()
    {
        SpriteSetter.Set(IdleSprite);
        playingAnim = false;
    }

    internal void OnDisable()
    {
        Button.style.visibility = Visibility.Hidden;
    }
    internal void OnEnable()
    {
        Button.style.visibility = Visibility.Visible;
    }
    protected override void FlashingAnim()
    {
        timeBetweenFlashing -= Time.deltaTime;
        if (timeBetweenFlashing < 0)
        {
            timeBetweenFlashing = GetFlashingDelay();
            SpriteSetter.Set(FlashingSprite);
            playingAnim = true;
            currentAnimTime = AnimationLength;
        }
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