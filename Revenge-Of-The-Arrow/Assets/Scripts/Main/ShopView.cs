
using UnityEngine;
using UnityEngine.UI;
public class ShopView : View
{

    [SerializeField] private Button backBtn;
    
    public override void Initialize()
    {
        backBtn.onClick.AddListener(() => ViewManager.showLast());
    }
}
