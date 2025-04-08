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
using UnityEngine;
using System.Reflection;
using System.Globalization;
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
    public static bool CreateGameConfigExcel(string excelPath)
    {
        if (File.Exists(excelPath))
        {
            Debug.LogWarning($"创建配置表失败! 文件已存在:{excelPath}");
            return false;
        }
        try
        {
            var excelDir = Path.GetDirectoryName(excelPath);
            if (!Directory.Exists(excelDir))
            {
                Directory.CreateDirectory(excelDir);
            }
            using (var excel = new ExcelPackage(excelPath))
            {
                var sheet = excel.Workbook.Worksheets.Add("Sheet 1");
                sheet.SetValue(1, 1, "#");
                sheet.SetValue(1, 2, Path.GetFileNameWithoutExtension(excelPath));
                sheet.SetValue(2, 1, "#");
                sheet.SetValue(2, 2, "Key");
                sheet.SetValue(2, 3, "备注");
                sheet.SetValue(2, 4, "Value");
                excel.Save();
            }
            return true;
        }
        catch (Exception emsg)
        {
            Debug.LogError($"创建Excel:{excelPath}失败! Error:{emsg}");
            return false;
        }

    }

    /// <summary>
    /// 调整config文件
    /// </summary>
    /// <param name="excelPath"></param>
    /// <param name="pairs"></param>
    /// <returns></returns>
    public static bool ChangeGameConfigExcel(string excelPath, Dictionary<string, string> pairs)
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
    /// <summary>
    /// 调整GamePlayAudioTable.xlsx文件
    /// </summary>
    /// <param name="excelPath"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static bool ChangeGameConfigExcel(string excelPath, List<GamePlayAudioConfig> config)
    {
        try
        {
            var excelDir = Path.GetDirectoryName(excelPath);
            if (!Directory.Exists(excelDir))
            {
                Directory.CreateDirectory(excelDir);
            }
            using (var excel = new ExcelPackage(excelPath))
            {
                ExcelWorksheet sheet = null;
                if (!File.Exists(excelPath))
                    sheet = excel.Workbook.Worksheets.Add("Sheet 1");
                else
                    sheet = excel.Workbook.Worksheets["Sheet 1"];
                sheet.SetValue(1, 1, "#");
                sheet.SetValue(1, 2, Path.GetFileNameWithoutExtension(excelPath));
                sheet.SetValue(2, 1, "#");
                sheet.SetValue(2, 2, "ID");
                sheet.SetValue(3, 1, "#");
                sheet.SetValue(3, 2, "int");
                sheet.SetValue(4, 1, "#");
                sheet.SetValue(4, 3, "备注");
                sheet.SetValue(4, 4, "请添加字段, 字段名首字母大写");
                int row = 4;
                for (int i = sheet.Dimension.End.Row; i >= row; i--)
                {
                    if (sheet.Dimension.Rows > row) // 确保行存在
                    {
                        sheet.DeleteRow(i); // 删除行
                    }
                }
                int index = 0;
                List<GamePlayAudioConfig> tempList = new List<GamePlayAudioConfig>();
                foreach (var item in config)
                {
                    if (AssetDatabase.IsValidFolder(item.audioPath))
                    {
                        var filesGuid = AssetDatabase.FindAssets("", new string[] { item.audioPath });
                        foreach (var guid in filesGuid)
                        {
                            var path = AssetDatabase.GUIDToAssetPath(guid);
                            var suffix = Path.GetExtension(path).TrimStart('.');
                            if (!Enum.GetNames(typeof(EAudioSuffix)).Contains(suffix))
                            {
                                continue;
                            }
                            var t = new GamePlayAudioConfig();
                            t.audioPath = path;
                            t.audionGUID = guid;
                            t.soundGroup = item.soundGroup;
                            t.volume = item.volume;
                            t.pitch = item.pitch;
                            t.audioKeyword = item.soundGroup.ToString() + "_" + Path.GetFileNameWithoutExtension(path);
                            tempList.Add(t);
                        }
                    }
                    else
                    {
                        item.audioKeyword = item.soundGroup.ToString() + "_" + Path.GetFileNameWithoutExtension(item.audioPath);
                        tempList.Add(item);
                    }
                }

                foreach (var item in tempList)
                {
                    row++;
                    index++;
                    int cellCol = 1;
                    sheet.Cells[row, cellCol + 1].Value = index;
                    int col = 4;
                    foreach (var tp in item.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                    {
                        bool isEnum = false;
                        var temp = tp.FieldType;
                        sheet.Cells[2, col].Value = tp.Name[0].ToString().ToUpper()+tp.Name.Substring(1);
                        if (temp.Equals(typeof(string)))
                        {
                            sheet.Cells[3, col].Value = "string";
                        }
                        else if (temp.BaseType.Equals(typeof(Enum)))
                        {
                            sheet.Cells[3, col].Value = "Enum";
                            isEnum = true;
                        }
                        else if (temp.Equals(typeof(float)))
                        {
                            sheet.Cells[3, col].Value = "float";
                        }
                        else
                        {
                            Debug.LogError("请继续完善此类型" + temp.ToString()); ;
                        }
                        sheet.Cells[row, col].Value = isEnum ? temp.Name + "." + tp.GetValue(item) : "" + tp.GetValue(item);
                        col++;
                    }
                }
                excel.Save();
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"生成{excelPath}失败" + e.ToString());
            return false;
        }
    }
}
