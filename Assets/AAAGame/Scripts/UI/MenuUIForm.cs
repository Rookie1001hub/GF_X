using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using OctoberStudio.UI;
using UnityEngine.UI;
using OctoberStudio;

/// <summary>
/// 菜单界面
/// </summary>
public partial class MenuUIForm : UIFormBase
{
    #region public

    #endregion
    #region serialize
    [SerializeField]
    StagesDatabase stagesDatabase;
    #endregion
    #region private 
    GamePlayAudioTable btnClickTableData;
    PlayerDataModel playerData;
    GamePlayDataModel gameplayerData;
    #endregion
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        OnFormInit();
    }
    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        OnFormOpen();
    }
    protected override void OnButtonClick(object sender, Button btSelf)
    {
        base.OnButtonClick(sender, btSelf);
        OnBtnClick(btSelf);
    }
    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }
    void OnFormInit()
    {
        gameObject.AddComponent<ScalerHelper>();
        btnClickTableData = GF.DataTable.GetDataTable<GamePlayAudioTable>().GetDataRow(ele => ele.AudioKeyword == AudioConst.Sound_click);
    }
    void OnFormOpen()
    {
        Log.Debug("打开了MenuUIForm");
        var bgmTableData = GF.DataTable.GetDataTable<GamePlayAudioTable>().GetDataRow(ele => ele.AudioKeyword == AudioConst.Music_music);
        GF.Sound.PlayGamePlayBGM(bgmTableData.AudioPath, bgmTableData.SoundGroup.ToString(), bgmTableData.Volume);
        //加载所有的游戏数据
        playerData = GF.DataModel.GetOrCreate<PlayerDataModel>();
        gameplayerData = GF.DataModel.GetOrCreate<GamePlayDataModel>();
        RefershCoins();
    }
    void OnBtnClick(Button btSelf)
    {
        GF.Sound.PlayGamePlaySound(btnClickTableData.AudioPath, btnClickTableData.SoundGroup.ToString(), false, btnClickTableData.Volume);
        if (btSelf == varCharactersButton)
        {
            varCharactersWindow.SetActive(true);
        }
        else if (btSelf == varUpgradeButton)
        {
            varUpgradesWindow.SetActive(true);
        }
        else if (btSelf == varSettingsButton)
        {
            GF.UI.OpenUIForm(UIViews.SettingDialog);
        }
        else if (btSelf == varPlayButton)
        {
            //进行游戏
        }
        else if (btSelf == varLeftButton)//关卡切换
        {

        }
        else if (btSelf == varRightButton)
        {

        }
        else if (btSelf == varUpgradBackBtn)
        {
            varUpgradesWindow.SetActive(false);
        }
        else if (btSelf == varChaBackBtn)
        {
            varCharactersWindow.SetActive(false);
        }
    }
    void RefershCoins()
    {
        varGoldText.text = playerData.Coins.ToString();
    }
}