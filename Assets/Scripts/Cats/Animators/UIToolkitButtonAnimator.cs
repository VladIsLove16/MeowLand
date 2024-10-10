using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
public class UIToolkitButtonAnimator : CatAnimatorBase
{
    Button Button;
    private float CurrentAnimTime;
    private bool CanFlash;
    private void Start()
    {
        SoundSequenceGame.instance.roundStateChanged.AddListener(OnSoundSequenceGame_roundStateChanged);
    }
    public void Init(Button button)
    {
        this.Button = button;
        SpriteSetter = new VisualElementSpriteSetter(button);
    }
    public override void StartAnimation(AnimationType type)
    {
        StartCoroutine(StartAnimation_Coroutine(type));
    }
    private void OnSoundSequenceGame_roundStateChanged(SoundSequenceGame.RoundState state)
    {
        if(state == SoundSequenceGame.RoundState.playingSounds)
        {
            SpriteSetter.Set(IdleSprite);
            ToggleFlashing(false);
        }
        else
        {
            ToggleFlashing(true);
        }
    }

    private void ToggleFlashing(bool state)
    {
        CanFlash = state;
    }

    private void Update()
    {
        //Debug.Log(playingAnim + " " + timeBetweenFlashing);
        AnimationTimer();
        if (!playingAnim && CanFlash)
        {
            StartCoroutine(FlashingAnim());
        }
    }

    private void AnimationTimer()
    {
        CurrentAnimTime -= Time.deltaTime;
        if (CurrentAnimTime < 0)
        {
            if (SpriteSetter == null)
                Debug.Log("SpriteSetter is null");
            SpriteSetter.Set(IdleSprite);
            playingAnim = false;
        }
    }

    protected  IEnumerator  FlashingAnim()
    {
        //Debug.Log("Flashing");
        timeBetweenFlashing -= Time.deltaTime;
        if (timeBetweenFlashing < 0)
        {
            CancelInvoke("OnAnimationEnd");
            timeBetweenFlashing = GetFlashDelay();
            playingAnim = true;
            SpriteSetter.Set(FlashingSprite);
            CurrentAnimTime = AnimationLength;
            yield return new WaitForSeconds(AnimationLength);
            OnAnimationEnd();
        }
    }
    private float GetFlashDelay()
    {
        return UnityEngine.Random.Range(1.5f, 4f);
    }

    private IEnumerator StartAnimation_Coroutine(AnimationType type)
    {
        CancelInvoke("OnAnimationEnd");
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
        CurrentAnimTime = AnimationLength;
        yield return new WaitForSeconds(AnimationLength);
        OnAnimationEnd();
    }

    protected override void OnAnimationEnd()
    {
       
    }

    internal void OnDisable()
    {
        if(Button!=null)
            Button.style.visibility = Visibility.Hidden;
    }
    internal void OnEnable()
    {
        if (Button != null)
            Button.style.visibility = Visibility.Visible;
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