using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using OctoberStudio.UI;
using UnityEngine.UI;

public partial class MenuUIForm : UIFormBase
{
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        gameObject.AddComponent<ScalerHelper>();
    }
    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        Log.Debug("打开了MenuUIForm");
        var bgmTableData = GF.DataTable.GetDataTable<GamePlayAudioTable>().GetDataRow(ele => ele.AudioKeyword == AudioConst.Music_music);
        GF.Sound.PlayGamePlaySound(bgmTableData.AudioPath, bgmTableData.SoundGroup.ToString(), true);
        //检查要展示的数据
        ShowCoins();
    }
    protected override void OnButtonClick(object sender, Button btSelf)
    {
        base.OnButtonClick(sender, btSelf);
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
    void ShowCoins()
    {

    }
}