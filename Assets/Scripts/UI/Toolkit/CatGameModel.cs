using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class CatGameModel
{
    public CatInfoSO catInfoSO;
    public UnityEvent Clicked;
    private bool clickable;
    public void Init(CatInfoSO shopItemSO)
    {
        catInfoSO = shopItemSO;
    }
    public void Awake()
    {
        if (catInfoSO != null)
            Init(catInfoSO);
    }
    public void SetUnclickable(bool b)
    {
        clickable = b ;
    }
    public void OnClick()
    {
        Debug.Log("ButtonClicked");
        Clicked.Invoke();
    }
}