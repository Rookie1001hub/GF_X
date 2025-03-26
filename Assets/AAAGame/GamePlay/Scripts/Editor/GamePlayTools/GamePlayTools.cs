using UGF.EditorTools;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[EditorToolMenu("GamePlay/GamePlay", null, 6, true)]
public class GamePlayTools : EditorToolBase
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    static readonly string ToolTitle="GamePlayTools";
    public override string ToolName => ToolTitle;

    public override Vector2Int WinSize => new Vector2Int(600, 800);

   
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }
}
