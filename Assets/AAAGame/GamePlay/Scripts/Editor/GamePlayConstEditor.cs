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
using System.Linq;
using OfficeOpenXml;
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
            stringBuilder.AppendLine($"\tpublic static readonly string  {item.Key} = \"{item.Key}\";");
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
                   .AppendLine("public static class AudioConst")
                   .AppendLine("{");
        foreach (var item in audioConfigs)
        {
            //TODO:嵌套梳理
            //是一个文件夹
            if (AssetDatabase.IsValidFolder(item.audioPath))
            {
                var filesGuid = AssetDatabase.FindAssets("", new string[] { item.audioPath });
                foreach (var guid in filesGuid)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    if (!IsValidAudio(item.soundGroup.ToString(), Path.GetFileNameWithoutExtension(path), path, stringBuilder))
                        continue;
                }
            }
            else
            {
                if (!IsValidAudio(item.soundGroup.ToString(), Path.GetFileNameWithoutExtension(item.audioPath), item.audioPath, stringBuilder))
                    continue;
            }
        }
        stringBuilder.AppendLine("}");
        File.WriteAllText(AudioConstScript, stringBuilder.ToString());
        AssetDatabase.Refresh();

        bool IsValidAudio(string k1, string k2, string path, StringBuilder builder)
        {
            var suffix = Path.GetExtension(path).TrimStart('.');
            if (!Enum.GetNames(typeof(EAudioSuffix)).Contains(suffix))
                return false;
            builder.AppendLine($"\tpublic static readonly string {k1}_{k2} = \"{k1}_{k2}\";");
            return true;
        }
    }
    /// <summary>
    /// 调整GamePlayAudioTable.xlsx文件
    /// </summary>
    /// <param name="excelPath"></param>
    /// <param name="pairs"></param>
    /// <returns></returns>
    public static bool ChangeGameConfigExcel(string excelPath, GamePlayAudioConfig config)
    {
        if (!File.Exists(excelPath))
        {
            return false;
        }
        else
        {
            try
            {
                using (var excel = new ExcelPackage(excelPath))
                {
                    var sheet = excel.Workbook.Worksheets["Sheet 1"];
                    int row = 2;
                    for (int i = sheet.Dimension.End.Row; i >= row; i--)
                    {
                        if (sheet.Dimension.Rows > row) // 确保行存在
                        {
                            sheet.DeleteRow(i); // 删除行
                        }
                    }
                    foreach (var item in pairs)
                    {
                        row++;
                        sheet.Cells[row, 2].Value = item.Key;
                        sheet.Cells[row, 4].Value = item.Value;
                    }
                    excel.Save();
                }
                return true;
            }
            catch (Exception emsg)
            {
                Debug.LogError($"修改Excel:{excelPath}失败! Error:{emsg}");
                return false;
            }
        }
    }
}
