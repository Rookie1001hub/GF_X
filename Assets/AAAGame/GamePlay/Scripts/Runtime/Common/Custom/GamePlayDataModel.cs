using OctoberStudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using OctoberStudio.Input;
using OctoberStudio.Upgrades;

//TODO:系统设置：音频、震屏等其他设置由框架来完成

/// <summary>
/// 玩法数据模型
/// </summary>
public class GamePlayDataModel : DataModelStorageBase
{
    /// <summary>
    /// 能力
    /// </summary>
    public Dictionary<AbilityType, int> abilitiesLevels;
    /// <summary>
    /// 升级方向
    /// </summary>
    public Dictionary<UpgradeType, int> upgradesLevels;
    /// <summary>
    /// 战斗舞台信息
    /// </summary>
    public Stage stage;
    /// <summary>
    /// 角色信息
    /// </summary>
    public Character character;
    /// <summary>
    /// 操作输入方式
    /// </summary>
    public InputType activeInput;


    protected override void OnInitialDataModel()
    {
        base.OnInitialDataModel();
        abilitiesLevels = new Dictionary<AbilityType, int>();
        upgradesLevels = new Dictionary<UpgradeType, int>();
        character = new Character()
        {
            boughtCharacterIds = new int[] { 0 },
            selectedCharacterId = 0
        };
        stage = new Stage();
    }
}
/// <summary>
/// 战斗舞台遗留信息
/// </summary>
[Serializable]
public class Stage
{
    public int maxReachedStageId;
    public int selectedStageId;

    public bool isPlaying;
    public float time;
    public bool resetAbilities;
    public int xpLevel;
    public float xp;
    public int enemiesKilled;
}
/// <summary>
/// 角色信息
/// </summary>
[Serializable]
public class Character
{
    /// <summary>
    /// 解锁的角色
    /// </summary>
    public int[] boughtCharacterIds;
    /// <summary>
    /// 选择的角色
    /// </summary>
    public int selectedCharacterId;
}