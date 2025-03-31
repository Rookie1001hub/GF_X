using System;
using System.Collections.Generic;
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


    ScrollView Content;

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
        var configTypeEnum = labelFromUXML.Q<EnumField>("ConfigType");
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

    }
    private void OnConfigTypeChange(ChangeEvent<Enum> evt)
    {
        if ((EGamePlayConfigType)evt.newValue == EGamePlayConfigType.Audio)
        {
            //获取数据
            
            //生成元素
            MakeAudioConfigEle();
        }
    }
    private void MakeAudioConfigEle()
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
        button.clicked += ()=>{

            Content.Add(MakeAudioConfigElement());
        } ;
        top.Add(button);
        top.style.flexDirection = FlexDirection.Row;
        Content.Add(top);
    }

    private void Button_clicked()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 生成音频配置元素
    /// </summary>
    /// <returns></returns>
    private VisualElement MakeAudioConfigElement()
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
            elementTop.Add(button);
        }
        {
            var objectField = new ObjectField();
            objectField.name = "AudioPath";
            objectField.label = "AudioPath";
            objectField.objectType = typeof(UnityEngine.Object);
            objectField.style.unityTextAlign = TextAnchor.MiddleLeft;
            objectField.style.flexGrow = 1f;
            elementTop.Add(objectField);
            var label = objectField.Q<Label>();
            label.style.minWidth = 63;
        }
        {
            var popupField = new PopupField<string>(Enum.GetNames(typeof(Const.SoundGroup)).ToList(), 0);
            popupField.name = "SoundGroup";
            popupField.style.unityTextAlign = TextAnchor.MiddleLeft;
            popupField.style.width = 150;
            elementBottom.Add(popupField);
        }
        {
            var textField = new TextField();
            textField.name = "AudioKeyword";
            textField.label = "AudioKeyword";
            textField.style.width = 200;
            elementBottom.Add(textField);
            var label = textField.Q<Label>();
            label.style.minWidth = 63;
        }
        {
            var floatField = new FloatField();
            floatField.name = "Volume";
            floatField.label = "Volume";
            floatField.style.width = 200;
            elementBottom.Add(floatField);
            var label = floatField.Q<Label>();
            label.style.minWidth = 63;
        }
        {
            var floatField = new FloatField();
            floatField.name = "Pitch";
            floatField.label = "Pitch";
            floatField.style.width = 200;
            elementBottom.Add(floatField);
            var label = floatField.Q<Label>();
            label.style.minWidth = 63;
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
