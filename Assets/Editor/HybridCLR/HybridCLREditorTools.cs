#region Comment Head

// Author:LiuXiYuan
// Date:2025/2/25 20:21:56
// Email:854327817@qq.com

#endregion

using HybridCLR.Editor.AOT;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace HybridCLR.Editor
{
    /// <summary>
    /// HybridCLR编辑器工具
    /// </summary>
    public static class HybridCLREditorTools
    {
        /// <summary>
        /// 深度裁剪aot元数据标记
        /// </summary>
        public const string DEEP_STRIP_AOTDLL = "DEEP_STRIP_AOTDLL";
        /// <summary>
        /// 深度裁剪aot元数据补充目录
        /// </summary>
        public static readonly string DeepStrippedAOTAssemblyPath = $"{SettingsUtil.HybridCLRDataDir}/StrippedAOTAssembly2";
        /// <summary>
        /// 注意:此方法尚未按照我的想法验证成功
        /// </summary>
        [MenuItem("Tools/HybridCLREditorTools/CheckAccessMissingMetadata")]
        public static void CheckAccessMissingMetadata()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            // aotDir指向 构建主包时生成的裁剪aot dll目录，而不是最新的SettingsUtil.GetAssembliesPostIl2CppStripDir(target)目录。
            // 一般来说，发布热更新包时，由于中间可能调用过generate/all，SettingsUtil.GetAssembliesPostIl2CppStripDir(target)目录中包含了最新的aot dll，
            // 肯定无法检查出类型或者函数裁剪的问题。
            // 需要在构建完主包后，将当时的aot dll保存下来，供后面补充元数据或者裁剪检查。
            //string aotDir = SettingsUtil.GetAssembliesPostIl2CppStripDir(target);
            string aotDir = "HybridCLRData/MainAOTDll/StandaloneWindows64";
            // 第2个参数hotUpdateAssNames为热更新程序集列表。对于旗舰版本，该列表需要包含DHE程序集，即SettingsUtil.HotUpdateAndDHEAssemblyNamesIncludePreserved。
            var checker = new HybridCLR.Editor.HotUpdate.MissingMetadataChecker(aotDir, SettingsUtil.HotUpdateAssemblyNamesIncludePreserved);

            string hotUpdateDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);
            foreach (var dll in SettingsUtil.HotUpdateAssemblyFilesExcludePreserved)
            {
                string dllPath = $"{hotUpdateDir}/{dll}";
                bool notAnyMissing = checker.Check(dllPath);
                if (!notAnyMissing)
                {
                    // DO SOMETHING
                }
                Debug.Log("检查热更新代码中是否引用了被裁剪的类型或函数" + dllPath + "MissingMetadataChecker notAnyMissing" + notAnyMissing);
            }
        }

        /// <summary>
        /// 进一步剔除AOT dll中非泛型函数元数据，输出到StrippedAOTAssembly2目录下
        /// 用于减少aot dll的大小
        /// </summary>
        [MenuItem("Tools/HybridCLREditorTools/DeepStripAOTAssembly")]
        public static void DeepStripAOTAssembly()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            string srcDir = SettingsUtil.GetAssembliesPostIl2CppStripDir(target);
            string dstDir = $"{DeepStrippedAOTAssemblyPath}/{target}";
            foreach (var src in Directory.GetFiles(srcDir, "*.dll"))
            {
                string dllName = Path.GetFileName(src);
                string dstFile = $"{dstDir}/{dllName}";
                AOTAssemblyMetadataStripper.Strip(src, dstFile);
            }
        }
    }
}
