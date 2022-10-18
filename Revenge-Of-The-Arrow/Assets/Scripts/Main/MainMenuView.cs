using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View
{

    [SerializeField] private Button shopBtn;
    [SerializeField] private Button leaderBoardBtn;

    public override void Initialize()
    {
        shopBtn.onClick.AddListener(() => ViewManager.Show<ShopView>());
        leaderBoardBtn.onClick.AddListener(() => ViewManager.Show<LeaderBoardView>());
    }
}
