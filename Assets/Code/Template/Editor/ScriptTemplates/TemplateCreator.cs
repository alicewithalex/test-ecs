// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

// alicewithalex:
// Based upon Leopotam template generator

using System;
using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace alicewithalex
{
    sealed class TemplateCreator : ScriptableObject
    {
        #region Constants

        const int Priority = int.MinValue;
        const string Title = "Custom Template Creator";
        const string ResourcePath = "EcsTemplate";

        const string StartupTemplate = "Startup.cs.txt";
        const string MonoProviderTemplate = "MonoProvider.cs.txt";
        const string ObjectProviderTemplate = "ObjectProvider.cs.txt";
        const string InitSystemTemplate = "InitSystem.cs.txt";
        const string RunSystemTemplate = "RunSystem.cs.txt";
        const string StateSystemTemplate = "StateSystem.cs.txt";
        const string UISystemTemplate = "UISystem.cs.txt";
        const string UIStateSystemTemplate = "UIStateSystem.cs.txt";
        const string ComponentTemplate = "Component.cs.txt";
        const string SignalTagTemplate = "SignalTag.cs.txt";
        const string ViewElementTemplate = "ViewElement.cs.txt";
        const string ViewComponentTemplate = "ViewComponent.cs.txt";
        const string EnumTemplate = "Enum.cs.txt";
        const string ConfigTemplate = "GameConfig.cs.txt";
        const string InterfaceTemplate = "Interface.cs.txt";
        const string ClassTemplate = "Class.cs.txt";
        const string MonoBehaviourTemplate = "MonoBehaviour.cs.txt";
        const string UIScreenTemplate = "UIScreen.cs.txt";
        const string MonoBindingTemplate = "MonoBinding.cs.txt";
        const string ObjectBindingTemplate = "ObjectBinding.cs.txt";

        private static readonly string[] FoldersToCreate = new[]
        {
            "Bindings",
            "Components",
            "Data",
            "Systems",
            "Views",
            "Mono"
        };

        #endregion

        #region Menu Items


        [MenuItem("Tools/Select Provider", false, Priority)]
        static void SelectProvider()
        {
            var files = Resources.LoadAll<EcsStartup>(ResourcePath);

            if (files.Length == 0) return;

            Selection.activeObject = files[0];
        }

        [MenuItem("Assets/Ecs/Folders", false, Priority)]
        static void CreateFolders()
        {
            var assetPath = GetAssetPath();

            string currentPath = string.Empty;
            foreach (var folder in FoldersToCreate)
            {
                currentPath = $"{assetPath}/{folder}";
                if (Directory.Exists(currentPath)) continue;

                Directory.CreateDirectory(currentPath);
            }

            AssetDatabase.Refresh();
        }


        [MenuItem("Assets/Ecs/Startup", false, Priority)]
        static void CreateStartupTpl()
        {
            var assetPath = GetAssetPath();
            CreateAndRenameAsset($"{assetPath}/EcsStartup.cs", GetIcon(), (name) =>
            {
                if (CreateTemplateInternal(GetTemplateContent(StartupTemplate, out var settings), name,
                    settings) == null)
                {
                    if (EditorUtility.DisplayDialog(Title, "Create data folders?", "Yes", "No"))
                    {
                        CreateEmptyFolder($"{assetPath}/Components");
                        CreateEmptyFolder($"{assetPath}/Data");
                        CreateEmptyFolder($"{assetPath}/Mono");
                        CreateEmptyFolder($"{assetPath}/Systems");
                        CreateEmptyFolder($"{assetPath}/Views");
                        AssetDatabase.Refresh();
                    }
                }
            });
        }


        [MenuItem("Assets/Ecs/Providers/Mono Provider", false, Priority)]
        static void CreateMonoProviderTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/MonoProvider.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(MonoProviderTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Providers/Object Provider", false, Priority)]
        static void CreateObjectProviderTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/ObjectProvider.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(ObjectProviderTemplate,
                out var settings), name, settings));
        }


        [MenuItem("Assets/Ecs/Systems/Init System", false, Priority)]
        static void CreateInitSystemTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/InitSystem.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(InitSystemTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Systems/Run System", false, Priority)]
        static void CreateRunSystemTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/RunSystem.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(RunSystemTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Systems/State System", false, Priority)]
        static void CreateStateSystemTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/StateSystem.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(StateSystemTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Systems/UI System", false, Priority)]
        static void CreateUISystemTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/UISystem.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(UISystemTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Systems/UI State System", false, Priority)]
        static void CreateUIStateSystemTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/UIStateSystem.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(UIStateSystemTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Components/Component", false, Priority)]
        static void CreateComponentTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/Component.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(ComponentTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Components/Signal or Tag", false, Priority)]
        static void CreateComponentFlagTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/SignalTag.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(SignalTagTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Views/View Element", false, Priority)]
        static void CreateViewElementTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/ViewElement.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(ViewElementTemplate,
                out var settings), name, settings));
        }


        [MenuItem("Assets/Ecs/Views/View Component", false, Priority)]
        static void CreateViewComponentTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/ViewComponent.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(ViewComponentTemplate,
                out var settings), name, settings));
        }


        [MenuItem("Assets/Ecs/Data/Enum", false, Priority)]
        static void CreateEnumTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/Enum.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(EnumTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Data/GameConfig", false, Priority)]
        static void CreateConfigTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/GameConfig.cs",
                GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(ConfigTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Data/Interface", false, Priority)]
        static void CreateInterfaceTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/Interface.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(InterfaceTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Base/Class", false, Priority)]
        static void CreateClassTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/Class.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(ClassTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Base/MonoBehaviour", false, Priority)]
        static void CreateMonoBehaviourTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/MonoBehaviour.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(MonoBehaviourTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/UI/UI Screen", false, Priority)]
        static void CreateUIScreenTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/UIScreen.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(UIScreenTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Bindings/MonoBinding", false, Priority)]
        static void CreateMonoBindingTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/MonoBinding.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(MonoBindingTemplate,
                out var settings), name, settings));
        }

        [MenuItem("Assets/Ecs/Bindings/ObjectBinding", false, Priority)]
        static void CreateObjectBindingTpl()
        {
            CreateAndRenameAsset($"{GetAssetPath()}/ObjectBinding.cs", GetIcon(),
                (name) => CreateTemplateInternal(GetTemplateContent(ObjectBindingTemplate,
                out var settings), name, settings));
        }

        #endregion

        #region Logic

        public static string CreateTemplate(string proto, string fileName, string prefix,
            int deepness)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return "Invalid filename";
            }

            var folderTree = Path.GetDirectoryName(fileName).Split(new[] { '\\' }, StringSplitOptions.None);

            var currentNamespace = "";
            var finalNamespace = "";

            if (folderTree.Length <= deepness)
            {
                finalNamespace = EditorSettings.projectGenerationRootNamespace;

                if (string.IsNullOrEmpty(EditorSettings.projectGenerationRootNamespace))
                {
                    finalNamespace = "Application";
                }
            }
            else
            {
                for (int i = deepness; i < folderTree.Length; i++)
                {
                    if (i < folderTree.Length - 1)
                    {
                        currentNamespace += folderTree[i] + ".";
                    }
                    else
                    {
                        currentNamespace += folderTree[i];
                    }
                }

                finalNamespace = prefix + "." + currentNamespace;
            }

            proto = proto.Replace("#NAMESPACE", finalNamespace);
            proto = proto.Replace("#SCRIPTNAME", Path.GetFileNameWithoutExtension(fileName));
            proto = proto.Replace("#PATH", finalNamespace.Replace('.', '/'));

            try
            {
                File.WriteAllText(AssetDatabase.GenerateUniqueAssetPath(fileName), proto);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            AssetDatabase.Refresh();
            return null;
        }

        static void CreateEmptyFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                    File.Create($"{folderPath}/.gitkeep");
                }
                catch
                {
                    // ignored
                }
            }
        }

        static string CreateTemplateInternal(string proto, string fileName,
            TemplateSettings settings)
        {
            var res = CreateTemplate(proto, fileName, settings.NamespacePrefix, settings.SkipDeepness);
            if (res != null)
            {
                EditorUtility.DisplayDialog(Title, res, "Close");
            }
            return res;
        }

        static string GetTemplateContent(string proto, out TemplateSettings settings)
        {
            // hack: its only one way to get current editor script path. :(
            var pathHelper = CreateInstance<TemplateCreator>();
            var path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(pathHelper)));
            DestroyImmediate(pathHelper);

            var content = "";
            try
            {
                content = File.ReadAllText(Path.Combine(path ?? "", proto));
            }
            catch
            {
                content = null;
            }

            var settingsPath = $"{path}/TemplateSettings.asset";
            settings = AssetDatabase.LoadAssetAtPath<TemplateSettings>(settingsPath);

            if (settings == null)
            {
                settings = CreateInstance<TemplateSettings>();
                AssetDatabase.CreateAsset(settings, settingsPath);
                AssetDatabase.Refresh();
            }

            return content;
        }

        static string GetAssetPath()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (!string.IsNullOrEmpty(path) && AssetDatabase.Contains(Selection.activeObject))
            {
                if (!AssetDatabase.IsValidFolder(path))
                {
                    path = Path.GetDirectoryName(path);
                }
            }
            else
            {
                path = "Assets";
            }
            return path;
        }

        static Texture2D GetIcon()
        {
            return EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;
        }

        static void CreateAndRenameAsset(string fileName, Texture2D icon, Action<string> onSuccess)
        {
            var action = CreateInstance<CustomEndNameAction>();
            action.Callback = onSuccess;
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, action, fileName, icon, null);
        }

        #endregion

        sealed class CustomEndNameAction : EndNameEditAction
        {
            [NonSerialized] public Action<string> Callback;

            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                Callback?.Invoke(pathName);
            }
        }

    }
}