using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UGF.EditorTools;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[EditorToolMenu("GamePlay/GamePlay", null, 6, true)]
public class GamePlayTools : EditorToolBase
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    static readonly string ToolTitle = "GamePlayTools";
    public override string ToolName => ToolTitle;

    public override Vector2Int WinSize => new Vector2Int(600, 800);


    ScrollView Content; EnumField configTypeEnum;

    //音频数据列表
    List<GamePlayAudioConfig> gamePlayAudioConfigs = new List<GamePlayAudioConfig>();

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
        //获取配置选择框
        configTypeEnum = labelFromUXML.Q<EnumField>("ConfigType");
        configTypeEnum.Init(EGamePlayConfigType.None);
        configTypeEnum.RegisterCallback<ChangeEvent<Enum>>(OnConfigTypeChange);
        //获取中间展示的滚动组
        Content = root.Q<ScrollView>("Content");
        //获取生成按钮
        var generateBtn = labelFromUXML.Q<Button>("Generate");

        generateBtn.clicked += GenerateBtn_onClick;

    }



    /// <summary>
    ///  生成按钮的响应方法
    /// </summary>
    private void GenerateBtn_onClick()
    {
        if ((EGamePlayConfigType)configTypeEnum.value == EGamePlayConfigType.Audio)
        {
            //写数据
            File.WriteAllText(GamePlayConstEditor.GamePlayAudioConfigFile, UtilityBuiltin.Json.ToJson(gamePlayAudioConfigs));
            //生成配置常量 
            GamePlayConstEditor.GenerateAudioConstScript(gamePlayAudioConfigs);
            //生成excel
            var excelDir = GameDataGenerator.GetGameDataExcelDir(GameDataType.DataTable);
            GamePlayConstEditor.ChangeGameConfigExcel(excelDir + "/" + GamePlayConstEditor.GamePlayAudioTable + ".xlsx", gamePlayAudioConfigs);

        }
        AssetDatabase.Refresh();
    }
    private void OnConfigTypeChange(ChangeEvent<Enum> evt)
    {
        if ((EGamePlayConfigType)evt.newValue == EGamePlayConfigType.Audio)
        {
            //获取数据
            if (File.Exists(GamePlayConstEditor.GamePlayAudioConfigFile))
                gamePlayAudioConfigs = UtilityBuiltin.Json.ToObject<List<GamePlayAudioConfig>>(File.ReadAllText(GamePlayConstEditor.GamePlayAudioConfigFile));
            //生成元素
            MakeAudioConfigEle(gamePlayAudioConfigs);
        }
    }
    private void MakeAudioConfigEle(List<GamePlayAudioConfig> audioConfigs)
    {
        Content.Clear();
        VisualElement top = new VisualElement();
        //头部
        {
            var label = new Label();
            label.text = "配置音频";
            label.style.fontSize = 18;
            label.style.unityTextAlign = TextAnchor.MiddleLeft;
            top.Add(label);
        }
        var button = new Button();
        button.name = "Add";
        button.text = "+";
        button.style.fontSize = 18;
        button.style.unityTextAlign = TextAnchor.MiddleRight;
        button.clicked += () =>
        {
            var config = new GamePlayAudioConfig();
            audioConfigs.Add(config);
            Content.Add(MakeAudioConfigElement(config));
        };
        top.Add(button);
        top.style.flexDirection = FlexDirection.Row;
        Content.Add(top);
        foreach (var item in audioConfigs)
        {
            Content.Add(MakeAudioConfigElement(item));
        }
    }



    /// <summary>
    /// 生成音频配置元素
    /// </summary>
    /// <returns></returns>
    private VisualElement MakeAudioConfigElement(GamePlayAudioConfig config)
    {
        VisualElement element = new VisualElement();
        //元素上部
        VisualElement elementTop = new VisualElement();
        elementTop.style.flexDirection = FlexDirection.Row;
        element.Add(elementTop);
        //元素下部
        VisualElement elementBottom = new VisualElement();
        elementBottom.style.flexDirection = FlexDirection.Row;
        element.Add(elementBottom);

        {
            var button = new Button();
            button.name = "Remove";
            button.text = "-";
            button.style.unityTextAlign = TextAnchor.MiddleLeft;
            button.style.flexGrow = 0f;
            button.clicked += () =>
            {
                gamePlayAudioConfigs.Remove(config);
                MakeAudioConfigEle(gamePlayAudioConfigs);
            };
            elementTop.Add(button);
        }
        {
            var objectField = new ObjectField();
            objectField.name = "AudioPath";
            objectField.label = "AudioPath";
            objectField.objectType = typeof(UnityEngine.Object);
            if (!string.IsNullOrEmpty(config.audioPath))
            {
                var obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(config.audioPath);
                objectField.SetValueWithoutNotify(obj);
                objectField.value.name = config.audioPath;
            }
            objectField.style.unityTextAlign = TextAnchor.MiddleLeft;
            objectField.style.flexGrow = 1f;
            elementTop.Add(objectField);
            var label = objectField.Q<Label>();
            label.style.minWidth = 63;
            objectField.RegisterValueChangedCallback(evt =>
            {
                config.audioPath = AssetDatabase.GetAssetPath(evt.newValue);
                config.audionGUID = AssetDatabase.AssetPathToGUID(config.audioPath);
            });
        }
        {
            var popupField = new PopupField<string>(Enum.GetNames(typeof(Const.SoundGroup)).ToList(), 0);
            popupField.name = "SoundGroup";
            popupField.value = config.soundGroup.ToString();
            popupField.style.unityTextAlign = TextAnchor.MiddleLeft;
            popupField.style.width = 150;
            elementBottom.Add(popupField);
            popupField.RegisterValueChangedCallback(evt =>
            {
                config.soundGroup = Enum.Parse<Const.SoundGroup>(evt.newValue);
            });
        }
        {
            var textField = new TextField();
            textField.name = "AudioKeyword";
            textField.label = "AudioKeyword";
            textField.style.width = 200;
            textField.value = config.audioKeyword;
            elementBottom.Add(textField);
            var label = textField.Q<Label>();
            label.style.minWidth = 63;
            textField.RegisterValueChangedCallback(evt =>
            {
                config.audioKeyword = evt.newValue;
            });
        }
        {
            var floatField = new FloatField();
            floatField.name = "Volume";
            floatField.label = "Volume";
            floatField.style.width = 200;
            floatField.value = config.volume;
            elementBottom.Add(floatField);
            var label = floatField.Q<Label>();
            label.style.minWidth = 63;
            floatField.RegisterValueChangedCallback(evt =>
            {
                config.volume = evt.newValue;
            });
        }
        {
            var floatField = new FloatField();
            floatField.name = "Pitch";
            floatField.label = "Pitch";
            floatField.style.width = 200;
            floatField.value = config.pitch;
            elementBottom.Add(floatField);
            var label = floatField.Q<Label>();
            label.style.minWidth = 63;
            floatField.RegisterValueChangedCallback(evt =>
            {
                config.pitch = evt.newValue;
            });
        }
        GroupBox groupBox = new GroupBox();
        groupBox.Add(elementTop);
        groupBox.Add(elementBottom);
        element.Add(groupBox);
        return element;
    }
}
/// <summary>
/// GamePlay配置类型
/// </summary>
public enum EGamePlayConfigType
{
    None = 0,
    /// <summary>
    /// 场景
    /// </summary>
    Scene,
    /// <summary>
    /// 音频
    /// </summary>
    Audio,
}
/// <summary>
/// 资源搜索类型
/// </summary>
public enum EAssetSearchType
{
    All,
    RuntimeAnimatorController,
    AnimationClip,
    AudioClip,
    AudioMixer,
    Font,
    Material,
    Mesh,
    Model,
    PhysicMaterial,
    Prefab,
    Scene,
    Script,
    Shader,
    Sprite,
    Texture,
    VideoClip,
}

/// <summary>
/// 资源文件格式
/// </summary>
public enum EAssetFileExtension
{
    prefab,
    unity,
    fbx,
    anim,
    controller,
    png,
    jpg,
    mat,
    shader,
    ttf,
    cs,
}
/// <summary>
/// 音频尾缀格式
/// </summary>
public enum EAudioSuffix
{
    wav,
    mp3
}