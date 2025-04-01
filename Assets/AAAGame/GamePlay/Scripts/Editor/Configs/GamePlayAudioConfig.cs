#region Comment Head
//------------------------------------------------------------
// Author:LiuXiYuan
// Date:2025/3/29 14:39:36
// Email:854327817@qq.com
//------------------------------------------------------------
#endregion

using System;

/// <summary>
/// 游戏音频配置
/// </summary>
[Serializable]
public class GamePlayAudioConfig
{
    /// <summary>
    /// 音频路径
    /// </summary>
    public string audioPath;
    /// <summary>
    /// 音频资源的guid
    /// </summary>
    public string audionGUID;
    /// <summary>
    /// 音频关键字
    /// </summary>
    public string audioKeyword;
    /// <summary>
    /// 音频所在的组
    /// </summary>
    public Const.SoundGroup soundGroup;
    /// <summary>
    /// 音频声音大小
    /// </summary>
    public float volume;
    /// <summary>
    /// 音频的音高
    /// </summary>
    public float pitch;
    
    public GamePlayAudioConfig()
    {
        audioPath = audioKeyword = "";
    }
}
