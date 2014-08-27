//Copyright (c) 2014 Rumpus Animation
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
//to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
//and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//		
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//		
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Unigayo;

namespace Unigayo
{
	/// <summary>
	/// Editor window to setup the creation of mouth shapes if you have the phonemes stored in sprite sheets.
	/// </summary>
	public class LipsyncMouthSetCreator : EditorWindow
	{
		const string folderPath = "/Editor/CreatorSettings/";
		const string standardAssetName = "MouthImportSettings.asset";
		
		string[] phonemeNames = new string[]{"AI", "E", "etc", "FV", "L", "MBP", "O", "U", "WQ", "rest"};
		
		LipsyncMouthSetCreatorSettings currentSettings;
		List<LipsyncMouthSetCreatorSettings> allSettings = new List<LipsyncMouthSetCreatorSettings>();
				
		string FolderPath
		{
			get
			{
				return LipsyncFileUtility.unigayoPath + folderPath;
			}
		}				
						
		string DefaultPath
		{
			get
			{
				return FolderPath + standardAssetName;
			}
		}				
						
		void OnEnable()
		{
			System.IO.DirectoryInfo directoyInfo = new System.IO.DirectoryInfo( LipsyncFileUtility.unigayoPath + folderPath );
			Debug.Log(directoyInfo.FullName);
			
			LipsyncFileUtility.WalkDirectoryRecursively( directoyInfo, ".asset", fileInfo =>
			{
				string path = fileInfo.FullName.Replace("\\", "/");
								
				string relativePath = "Assets" + path.Replace(Application.dataPath, "");
				Debug.Log("Rel path " + relativePath);
				
				LipsyncMouthSetCreatorSettings settings; 
				settings = (LipsyncMouthSetCreatorSettings)
				AssetDatabase.LoadAssetAtPath(relativePath, typeof(LipsyncMouthSetCreatorSettings));
				
				settings.filePath = fileInfo.FullName;
				allSettings.Add( settings );														
			});
			
			if(allSettings.Count <= 0)
			{
				currentSettings = ScriptableObject.CreateInstance<LipsyncMouthSetCreatorSettings>();
				AssetDatabase.CreateAsset(currentSettings, DefaultPath);
				
				allSettings.Add(currentSettings);
				
				Debug.Log("had to add one");
            }
			else
			{
				Debug.Log("there is something");
				currentSettings = allSettings[0];
			}
		}									
					
		[MenuItem("Window/Unigayo/Preston Blair Mouth Set Creator")]
		public static void Open()
		{	
			EditorWindow.GetWindow<LipsyncMouthSetCreator>(true);
		}
		
		Vector2 settingsPanelScrollPos;
		void OnGUI()
		{
			GUILayout.Label("sprite assignment", EditorStyles.boldLabel);
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical(GUILayout.MinWidth(200));
			GUILayout.Label("Mouth Import Settings");
			
			
			settingsPanelScrollPos = GUILayout.BeginScrollView(settingsPanelScrollPos);
				foreach(LipsyncMouthSetCreatorSettings settings in allSettings)
				{
					if(currentSettings == settings)
						GUI.enabled = false;
						
					if(GUILayout.Button(settings.setName, EditorStyles.miniButton))
						currentSettings = settings;
						
					GUI.enabled = true;
				}			
			GUILayout.EndScrollView();
			
			GUILayout.EndVertical();
		
			GUILayout.BeginVertical();
			if(currentSettings != null)
			{
			
			GUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Name");
			currentSettings.setName = EditorGUILayout.TextField(currentSettings.setName);
			if(GUI.changed) 
				EditorUtility.SetDirty(currentSettings);
			GUILayout.EndHorizontal();
			
			GUILayout.Label("Sprite assignment", EditorStyles.boldLabel);
			GUILayout.BeginHorizontal();
				
				for(int i = 0 ; i < currentSettings.spriteAssignment.Count ; i++)
				{
					GUILayout.BeginVertical();
						GUILayout.Label(phonemeNames[i], EditorStyles.miniLabel);
						currentSettings.spriteAssignment[i] = EditorGUILayout.IntField( currentSettings.spriteAssignment[i] , GUILayout.ExpandWidth(false));
						
						if(GUI.changed)
						{	
							if(currentSettings.spriteAssignment[i] > 9 || currentSettings.spriteAssignment[i] < 0)
								currentSettings.spriteAssignment[i] = 0;
						
							EditorUtility.SetDirty(currentSettings);
						}
					GUILayout.EndVertical();
				}
				GUILayout.EndHorizontal();
				
				GUILayout.Label("Mood Settings", EditorStyles.boldLabel);
				
				GUILayout.BeginHorizontal();
					GUILayout.Label("Mood name", EditorStyles.miniLabel, GUILayout.Width(200));
					GUILayout.Label("Sprite sheet", EditorStyles.miniLabel);				
				GUILayout.EndHorizontal();
				
				int indexToDelete = -1;
				for(int i = 0 ; i < currentSettings.mouths.Count ; i++)
				{
					GUILayout.BeginHorizontal();
					currentSettings.mouths[i].mood = EditorGUILayout.TextField( currentSettings.mouths[i].mood , GUILayout.Width(200));
						
						if(GUI.changed) 
							EditorUtility.SetDirty(currentSettings);
						
						currentSettings.mouths[i].spritesheet = (Sprite)EditorGUILayout.ObjectField( currentSettings.mouths[i].spritesheet, typeof(Sprite), false);
						
						if(GUI.changed) 
							EditorUtility.SetDirty(currentSettings);
					
						if(GUILayout.Button("X", GUILayout.ExpandWidth(false)))
							indexToDelete = i;
						
					GUILayout.EndHorizontal();
				}
				
				if(indexToDelete >= 0)
				{
					currentSettings.mouths.RemoveAt(indexToDelete);
					EditorUtility.SetDirty(currentSettings);
				}
				
				if(GUILayout.Button("Add Mouth"))
					currentSettings.mouths.Add( new LipsyncMouthSetCreatorSettings.ImportMouth());
				
				GUILayout.Label("Export settings", EditorStyles.boldLabel);
				
				GUILayout.BeginHorizontal();
					EditorGUILayout.PrefixLabel("folder path");
					currentSettings.exportFolderPath = EditorGUILayout.TextField(currentSettings.exportFolderPath);
					
				GUILayout.EndHorizontal();
				
				if(GUI.changed)
					EditorUtility.SetDirty(currentSettings);
																																												
				if(GUI.changed)
					EditorUtility.SetDirty(currentSettings);
				
				if(GUILayout.Button("Build Mouth Set"))
				{
					CreateMouthSet();
				}
				
				GUILayout.Label("More Options", EditorStyles.boldLabel);
								
				if(GUILayout.Button("Copy Parameters"))
				{
					CopySettings();
				}
				
				GUI.enabled = false;
				if(GUILayout.Button("Delete Parameters"))
				{
					DeleteSettings();
				}
				GUI.enabled = true;
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();																																																
		}
		
		void DeleteSettings()
		{
			if(!EditorUtility.DisplayDialog("Delete settings", "Are you sure?", "Yup!", "Nope!"))
				return;
		
			allSettings.Remove(currentSettings);
			
			FileUtil.DeleteFileOrDirectory(currentSettings.filePath);
			DestroyImmediate(currentSettings, true);
			
			if(allSettings.Count > 0)
				currentSettings = allSettings[0];
		}
		
		void CopySettings()
		{
			string path = AssetDatabase.GenerateUniqueAssetPath( DefaultPath );// LipsyncFileUtility.unigayoPath + folderPath );
			
			LipsyncMouthSetCreatorSettings settings = ScriptableObject.CreateInstance<LipsyncMouthSetCreatorSettings>();
			EditorUtility.CopySerialized( currentSettings, settings);
			settings.setName = currentSettings.setName + " copy";
			AssetDatabase.CreateAsset(settings, path);
			
			allSettings.Add( settings );
			currentSettings = settings;									
		}
		
		void CreateMouthSet()
		{
			LipsyncMouthSet set;
			
			if(LipsyncMouthSetFactory.CreateSet(currentSettings.mouths.ToArray(), currentSettings.spriteAssignment.ToArray(), out set))
			{
				string path = currentSettings.exportFolderPath + currentSettings.setName + ".asset";
				LipsyncMouthSet existingSet = (LipsyncMouthSet)AssetDatabase.LoadAssetAtPath( path, typeof(LipsyncMouthSet));
				if(existingSet != null)
				{
					EditorUtility.CopySerialized(set, existingSet);
				}
				else
				{
					AssetDatabase.CreateAsset(set, path);
				}
			}			
		}
		
	}
}
