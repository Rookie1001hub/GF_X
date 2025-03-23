using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using OctoberStudio.UI;
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
        GF.Sound.PlayBGM("music");
    }
}