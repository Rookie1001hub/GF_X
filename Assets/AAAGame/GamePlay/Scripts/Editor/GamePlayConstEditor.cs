#region Comment Head
//------------------------------------------------------------
// Author:LiuXiYuan
// Date:2025/3/21 23:01:36
// Email:854327817@qq.com
//------------------------------------------------------------
#endregion
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
/// <summary>
/// GamePlay编辑器常量
/// </summary>
public static class GamePlayConstEditor
{
    /// <summary>
    /// GamePlay的场景列表
    /// </summary>
    public static readonly string GamePlaySceneFile = "Assets/AAAGame/GamePlay/Scripts/Editor/Configs/GamePlaySceneList.txt";
    /// <summary>
    ///  GamePlaySceneConfig的配置表名称
    /// </summary>
    public static readonly string GamePlaySceneConfig = "SceneConfig";
    /// <summary>
    /// 场景常量脚本
    /// </summary>
    public static readonly string SceneConstScript = "Assets/AAAGame/GamePlay/Scripts/Runtime/Common/SceneConst.cs";

    /// <summary>
    /// GamePlay的AudioConfig列表
    /// </summary>
    public static readonly string GamePlayAudioConfigFile = "Assets/AAAGame/GamePlay/Scripts/Editor/Configs/GamePlayAudioConfigFile.txt";
    /// <summary>
    ///  GamePlayAudioTable的配置表名称
    /// </summary>
    public static readonly string GamePlayAudioTable = "GamePlayAudioTable";

    /// <summary>
    /// 音频常量脚本
    /// </summary>
    public static readonly string AudioConstScript = "Assets/AAAGame/GamePlay/Scripts/Runtime/Common/AudioConst.cs";

    /// <summary>
    /// 生成场景常量脚本
    /// </summary>
    public static void GenerateSceneConstScript(Dictionary<string, string> pairs)
    {
        var dir = Path.GetDirectoryName(SceneConstScript);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        var stringBuilder = new StringBuilder();
        stringBuilder
                   .AppendLine("//------------------------------------------------------------")
                   .AppendLine("//------------------------------------------------------------")
                   .AppendLine("// 此文件由工具自动生成，请勿直接修改。")
                   .AppendLine("//------------------------------------------------------------")
                   .AppendLine()
                   .AppendLine("/// <summary>")
                   .AppendLine("/// 场景常量标记")
                   .AppendLine("/// </summary>")
                   .AppendLine("public static class SceneConst")
                   .AppendLine("{");
        foreach (var item in pairs)
        {
            stringBuilder.AppendLine($"\tpublic static readonly string Scene_{item.Key} = \"{item.Key}\";");
        }
        stringBuilder.AppendLine("}");
        File.WriteAllText(SceneConstScript, stringBuilder.ToString());
        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 生成Audio常量脚本
    /// </summary>
    /// <param name="audioConfigs"></param>
    public static void GenerateAudioConstScript(List<GamePlayAudioConfig> audioConfigs)
    {
        var dir = Path.GetDirectoryName(AudioConstScript);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        var stringBuilder = new StringBuilder();
        stringBuilder
                   .AppendLine("//------------------------------------------------------------")
                   .AppendLine("//------------------------------------------------------------")
                   .AppendLine("// 此文件由工具自动生成，请勿直接修改。")
                   .AppendLine("//------------------------------------------------------------")
                   .AppendLine()
                   .AppendLine("/// <summary>")
                   .AppendLine("/// 场景常量标记")
                   .AppendLine("/// </summary>")
                   .AppendLine("public static class SceneConst")
                   .AppendLine("{");
       
        stringBuilder.AppendLine("}");
        File.WriteAllText(AudioConstScript, stringBuilder.ToString());
        AssetDatabase.Refresh();
    }
}
