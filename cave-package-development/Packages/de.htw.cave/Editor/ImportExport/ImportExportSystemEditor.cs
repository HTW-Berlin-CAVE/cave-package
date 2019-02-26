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

		static ImportExportSystemEditor()
		{
			EditorApplication.playModeStateChanged += ApplyAssetChange;
		}

		private static void ApplyAssetChange(PlayModeStateChange state)
		{
			if(state == PlayModeStateChange.EnteredEditMode)
			{
				string[] assetPaths = new string[ImportExportSystem.Instance.Entries.Count];

				int index = 0;
				foreach(ImportExportEntry entry in ImportExportSystem.Instance.Entries)
					assetPaths[index++] = AssetDatabase.GetAssetPath(entry.scriptableObject);

				assetPaths = assetPaths.Distinct().ToArray();
				AssetDatabase.ForceReserializeAssets(assetPaths);
			}
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

			EditorGUILayout.LabelField("Select the GameObject that holds a reference to the ScriptableObject you want to import and export. Awake will be called on all components after the import was successful.", EditorStyles.wordWrappedMiniLabel);

			this.entriesList.DoLayoutList();

			string tail = this.me.ReadLogTail(3);

			if(tail != null)
			{
				EditorGUILayout.LabelField(tail, EditorStyles.helpBox);

				EditorGUILayout.BeginHorizontal();

				GUILayout.FlexibleSpace();

				if(GUILayout.Button("Clear Log"))
				{
					this.me.ClearLogFile();
				}
				EditorGUILayout.EndHorizontal();
			}

			serializedObject.ApplyModifiedProperties();
			EditorGUI.EndChangeCheck();
		}
    }
}
