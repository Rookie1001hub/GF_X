#region Comment Head
//------------------------------------------------------------
// Author:LiuXiYuan
// Date:2025/3/21 23:01:36
// Email:854327817@qq.com
//------------------------------------------------------------
#endregion

using System;

/// <summary>
/// 场景类型
/// </summary>
[Serializable]
public enum GamePlaySceneType
{
    /// <summary>
    /// 玩法
    /// 只允许玩法场景被打包
    /// </summary>
    GamePlay,
    /// <summary>
    /// 工具
    /// </summary>
    Tool,
    /// <summary>
    /// 测试
    /// </summary>
    Test,
}

[Serializable]
public class GamePlayScenePair
{
    public string scenePath;
    public string sceneKeyword;
    public GamePlaySceneType sceneTypeTag;
    public GamePlayScenePair(string path, string keyword, GamePlaySceneType sceneType)
    {
        scenePath = path; sceneKeyword = keyword; sceneTypeTag = sceneType;
    }
}