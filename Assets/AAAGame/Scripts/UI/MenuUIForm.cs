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
    
    #endregion
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        gameObject.AddComponent<ScalerHelper>();
        btnClickTableData = GF.DataTable.GetDataTable<GamePlayAudioTable>().GetDataRow(ele => ele.AudioKeyword == AudioConst.Sound_click);
    }
    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        Log.Debug("打开了MenuUIForm");
        var bgmTableData = GF.DataTable.GetDataTable<GamePlayAudioTable>().GetDataRow(ele => ele.AudioKeyword == AudioConst.Music_music);
        GF.Sound.PlayGamePlayBGM(bgmTableData.AudioPath, bgmTableData.SoundGroup.ToString(), bgmTableData.Volume);
        //检查要展示的数据
        ShowCoins();
    }
    protected override void OnButtonClick(object sender, Button btSelf)
    {
        base.OnButtonClick(sender, btSelf);
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
    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }
    void ShowCoins()
    {

    }
}