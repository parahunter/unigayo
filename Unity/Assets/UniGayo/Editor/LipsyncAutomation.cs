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
using System.IO;
using Unigayo;

namespace Unigayo
{	
	/// <summary>
	/// Handles automation of importing MOHO files and storing them in the project as LipsyncSequenceDatabase asset files
	/// </summary>
	public class LipsyncAutomation
	{
		/// <summary>
		/// MOHO file ending.
		/// </summary>
		public const string fileEnding = ".dat";
	
		/// <summary>
		/// Call this to build sequence databases
		/// </summary>
		public static void BuildSequenceDatabases()
		{
			LipsyncAutomationData dataFile = LipsyncAutomationData.GetDataFile();
			string dataPath = dataFile.dataFolder;
			string outputPath = dataFile.outputFolder;
			
			BuildSequenceDatabases(dataPath, outputPath);
		}
		
		public static void BuildSequenceDatabases(string dataPath, string outputPath)
		{
			string projectPath = Application.dataPath.Replace("Assets", ""); 
			dataPath = projectPath + dataPath;
			
			DirectoryInfo dataFolder = new DirectoryInfo(dataPath);
			DirectoryInfo outputFolder = new DirectoryInfo(outputPath);
			
			DirectoryInfo[] sceneDataFolders = dataFolder.GetDirectories();
			
			foreach(DirectoryInfo sceneDataFolder in sceneDataFolders)
			{
				Debug.Log(sceneDataFolder.FullName);
				
				LipsyncSequenceDatabase database = LipsyncSequenceDatabaseFactory.BuildDatabase(sceneDataFolder);
				
				SaveDatabase(database, outputPath + "/" + sceneDataFolder.Name);
			}																			
		}
		
		static void SaveDatabase(LipsyncSequenceDatabase database, string path)
		{
			path += "SequenceDatabase.asset";
			
			LipsyncSequenceDatabase exisitingDatabase = AssetDatabase.LoadMainAssetAtPath (path) as LipsyncSequenceDatabase;
			if (exisitingDatabase != null) 
			{
				//if there is data we want to save the user might have altered inside the Unity editor we do it here
				foreach(LipsyncSequence existingSequence in exisitingDatabase.sequences)
				{
					foreach(LipsyncSequence sequence in database.sequences)
					{
						if(sequence.name == existingSequence.name)
						{
							sequence.mood = existingSequence.mood;
						}
					} 
				}
				
				EditorUtility.CopySerialized (database, exisitingDatabase);
				AssetDatabase.SaveAssets ();
			}
			else 
				AssetDatabase.CreateAsset (database, path);
		}		
	}
}