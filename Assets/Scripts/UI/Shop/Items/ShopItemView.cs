using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[RequireComponent(typeof(Cat))]
public class ShopItemView : MonoBehaviour
{
    private CatInfoSO item;
    public Cat cat;
    private Image Image;
    private Button btn;
    public float NotBoughtAlpha=0.3f;
    public UnityEvent OnClick;
    public void Awake()
    {
        cat=GetComponent<Cat>();
        item  = cat.catInfoSO;
        Image = GetComponent<Image> ();
        //Image.sprite = item.Sprite;
        item.stateChanged.AddListener(SetImageColor);
        btn = GetComponent<Button>();
        btn.onClick.AddListener(()=> OnClick.Invoke());
    }
    private void Start()
    {
        SetImageColor();
    }
    private void OnEnable()
    {
        SetImageColor();
    }
    private void SetImageColor()
    {
        if (item.IsUnlocked == false)
        { Image.color = Color.black; return; }
        if (item.IsBought == false) 
        {
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b,NotBoughtAlpha );
            return;
        }
        if (item.IsBought == true) 
        {
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b,1f );
            return;
        }
    }
}