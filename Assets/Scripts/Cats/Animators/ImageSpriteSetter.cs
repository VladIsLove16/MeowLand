﻿using UnityEngine;
using UnityEngine.UIElements;

public class ImageSpriteSetter : SpriteSetter
{
    public Image Image;
    public ImageSpriteSetter(Image image)
    {
        Image = image; 
    }
    public override void Set(Sprite sprite)
    {
        Image.sprite = sprite;
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