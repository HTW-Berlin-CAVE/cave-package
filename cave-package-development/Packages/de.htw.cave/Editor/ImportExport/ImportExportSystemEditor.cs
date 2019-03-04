using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Htw.Cave.ImportExport
{
	[CustomEditor(typeof(ImportExportSystem))]
	[InitializeOnLoadAttribute]
    public class ImportExportSystemEditor : Editor
    {
		private ImportExportSystem me;
		private Editor editor;
		private SerializedProperty settingsProperty;
		private SerializedProperty entriesProperty;
		private SerializedProperty overwriteLocalFilesProperty;
		private SerializedProperty enableInEditorProperty;
		private ReorderableList entriesList;
		private TextAsset importAsset;

		static ImportExportSystemEditor()
		{
			EditorApplication.playModeStateChanged += ApplyAssetChange;
		}

		public static void ReserializeEntries(ImportExportSystem system)
		{
			string[] assetPaths = new string[system.Entries.Count];

			int index = 0;
			foreach(ImportExportEntry entry in system.Entries)
				assetPaths[index++] = AssetDatabase.GetAssetPath(entry.scriptableObject);

			assetPaths = assetPaths.Distinct().ToArray();
			AssetDatabase.ForceReserializeAssets(assetPaths);
		}

		private static void ApplyAssetChange(PlayModeStateChange state)
		{
			if(state == PlayModeStateChange.EnteredEditMode)
				ReserializeEntries(ImportExportSystem.Instance);
		}

		public void Awake()
		{
			if(ImportExportSystem.Instance == null)
			{
				ImportExportSystem[] systems = FindObjectsOfType<ImportExportSystem>();
				if(systems.Length > 0)
					ImportExportSystem.Instance = systems[0];
			}
		}

		public void OnEnable()
		{
			this.me = (ImportExportSystem)base.target;
			this.editor = null;
			this.settingsProperty = serializedObject.FindProperty("settings");
			this.entriesProperty = serializedObject.FindProperty("entries");
			this.overwriteLocalFilesProperty = serializedObject.FindProperty("overwriteLocalFiles");
			this.enableInEditorProperty = serializedObject.FindProperty("enableInEditor");
			this.entriesList = new ReorderableList(serializedObject, this.entriesProperty, true, true, true, true);
			this.entriesList.drawHeaderCallback = (Rect rect) => {
				Rect left = rect;
				left.width = left.width * 0.5f;
				Rect right = left;
				right.x = left.x + left.width;
				EditorGUI.LabelField(left, "Game Object");
				EditorGUI.LabelField(right, "Asset");
			};
			this.entriesList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
				SerializedProperty element = this.entriesList.serializedProperty.GetArrayElementAtIndex(index);
				SerializedProperty gameObject = element.FindPropertyRelative("gameObject");
				SerializedProperty scriptableObject = element.FindPropertyRelative("scriptableObject");
				rect.height = rect.height * 0.75f;
				Rect left = rect;
				left.width = left.width * 0.5f;
				Rect right = left;
				right.x = left.x + left.width;
				EditorGUI.PropertyField(left, gameObject, new GUIContent());
				EditorGUI.PropertyField(right, scriptableObject, new GUIContent());
			};
			this.importAsset = null;
		}

		public override void OnInspectorGUI()
		{
			EditorGUI.BeginChangeCheck();
			serializedObject.Update();

			EditorUtils.BrandField();

			using(new EditorGUI.DisabledScope(true))
				EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));

			EditorGUILayout.PropertyField(this.settingsProperty);

			EditorGUI.indentLevel++;

			if(this.settingsProperty.objectReferenceValue == null)
				this.editor = null;

			if(this.editor == null && this.settingsProperty.objectReferenceValue != null)
				Editor.CreateCachedEditor(this.settingsProperty.objectReferenceValue, null, ref this.editor);

			if(this.editor != null)
			{
				this.editor.DrawDefaultInspector();
				this.editor.serializedObject.ApplyModifiedProperties();
			}

			EditorGUI.indentLevel--;

			EditorGUILayout.PropertyField(this.overwriteLocalFilesProperty);
			EditorGUILayout.PropertyField(this.enableInEditorProperty);

			if(this.enableInEditorProperty.boolValue)
				EditorGUILayout.HelpBox("Its recommended to disable this component in the editor to reduce loading time.", MessageType.Warning);

			EditorGUILayout.LabelField("Add ScriptableObject you want to import and export and the GameObject that holds a reference to it. Awake will be called on all GameObjects after a successful import.", EditorStyles.wordWrappedMiniLabel);

			this.entriesList.DoLayoutList();

			if(this.entriesList.index != -1)
			{
				SerializedProperty element = this.entriesList.serializedProperty.GetArrayElementAtIndex(this.entriesList.index);
				ScriptableObject scriptableObject = (ScriptableObject)element.FindPropertyRelative("scriptableObject").objectReferenceValue;

				EditorGUILayout.BeginHorizontal();

				GUILayout.FlexibleSpace();

				this.importAsset = (TextAsset)EditorGUILayout.ObjectField(this.importAsset, typeof(TextAsset), false);

				if(GUILayout.Button("Import"))
				{
					this.me.ImportManually(scriptableObject, Application.dataPath + "/" + AssetDatabase.GetAssetPath(this.importAsset));
					ReserializeEntries(this.me);
				}

				if(GUILayout.Button("Open"))
					EditorUtility.RevealInFinder(ImportExportHelper.ExistingPersistentPath(scriptableObject.name + ".json"));

				EditorGUILayout.EndHorizontal();
			}

			string tail = this.me.ReadLogTail(3);

			if(tail != null)
			{
				EditorGUILayout.LabelField(tail, EditorStyles.helpBox);

				EditorGUILayout.BeginHorizontal();

				GUILayout.FlexibleSpace();

				if(GUILayout.Button("Clear Log"))
					this.me.ClearLogFile();

				EditorGUILayout.EndHorizontal();
			}

			serializedObject.ApplyModifiedProperties();
			EditorGUI.EndChangeCheck();
		}
    }
}
