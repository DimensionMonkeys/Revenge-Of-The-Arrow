using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View
{

    [SerializeField] private Button shopBtn;
    [SerializeField] private Button leaderBoardBtn;
    [SerializeField] private Button shopItemBtn;
    [SerializeField] private Button shopRechargeBtn;
    [SerializeField] private ShopItemView shopItemView;
    [SerializeField] private ShopRechargeView shopRechargeView;
    public override void Initialize()
    {
        shopBtn.onClick.AddListener(() => ViewManager.Show<ShopView>());
        leaderBoardBtn.onClick.AddListener(() => ViewManager.Show<LeaderBoardView>());
        shopItemBtn.onClick.AddListener(() => {
            Debug.Log("innnnnnnnnn");
            ViewManager.Show(shopRechargeView, shopItemView);
        });
        shopRechargeBtn.onClick.AddListener(() => {

            ViewManager.Show(shopItemView, shopRechargeView);
        });
    }
}
