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
using Unigayo;
using System.IO;

namespace Unigayo
{
	/// <summary>
	/// Factory class to create lipsync sequence databases from a folder of MOHO files
	/// </summary>
	public static class LipsyncSequenceDatabaseFactory 
	{
		public static LipsyncSequenceDatabase BuildDatabase(DirectoryInfo directory)
		{
			LipsyncSequenceDatabase database = ScriptableObject.CreateInstance<LipsyncSequenceDatabase>();
			
			DirectoryWalkCallback callback = filePath =>
			{
				string[] lines = File.ReadAllLines(filePath.FullName);
				string name = filePath.Name.Replace(LipsyncAutomation.fileEnding, "");
				
				LipsyncSequence sequence = LipsyncSequenceFactory.BuildSequence(lines, name);
								
				database.sequences.Add( sequence );
			};	
			
			LipsyncFileUtility.WalkDirectoryRecursively( directory, ".dat", callback);													
			return database;		
		}
	}
}