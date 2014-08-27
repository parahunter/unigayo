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
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Unigayo
{
	public delegate void DirectoryWalkCallback(FileInfo info);
	/// <summary>
	/// Utility stuff to handle file operations
	/// </summary>	
	public class LipsyncFileUtility
	{
		public const string unigayoPath = "Assets/Unigayo";
	
		public static void WalkDirectoryRecursively(DirectoryInfo root, string filetype, DirectoryWalkCallback callback )
		{
			System.IO.FileInfo[] files = null;
			System.IO.DirectoryInfo[] subDirs = null;
			
			// First, process all the files directly under this folder 
			try
			{
				files = root.GetFiles("*" + filetype);
			}
			// This is thrown if even one of the files requires permissions greater 
			// than the application provides. 
			catch (IOException e)
			{
				// This code just writes out the message and continues to recurse. 
				// You may decide to do something different here. For example, you 
				// can try to elevate your privileges and access the file again.
				Debug.LogError(e.Message);
			}
			
			if (files != null)
			{
								
				for(int i = 0 ; i < files.Length ; i++)
				{
					System.IO.FileInfo file = files[i];
					
					if(EditorUtility.DisplayCancelableProgressBar("walk directory", file.Name, i / (float)files.Length))
						return;					
					
					callback( file );
				}
				
				EditorUtility.ClearProgressBar();
				
				// Now find all the subdirectories under this directory.
				subDirs = root.GetDirectories();
				
				foreach (System.IO.DirectoryInfo dirInfo in subDirs)
				{
					// Recursive call for each subdirectory.
					WalkDirectoryRecursively(dirInfo, filetype, callback);
				}
			}     
		}
	}
}