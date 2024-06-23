using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopItemView : MonoBehaviour
{
    [SerializeField]
    Shop Shop;
    public ShopItem item;
    [SerializeField]
    public Cat cat;
    private Image Image;
    private Button btn;
    public float NotBoughtAlpha=0.3f;
    private void Awake()
    {
        Shop.Awake();
        item  = cat.shopItem;
        cat.OutlineEnabled = false;
        Image = GetComponent<Image>();
        Image.sprite = item.Sprite;
        item.stateChanged.AddListener(SetImageColor);
        SetImageColor();
        btn= GetComponent<Button>();
        btn.onClick.AddListener(()=>Shop.TryBuy(item));
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