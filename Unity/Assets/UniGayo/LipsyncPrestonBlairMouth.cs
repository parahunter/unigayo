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

namespace Unigayo
{
	/// <summary>
	/// Stores the mouth shapes for the Preston Blair phonetic set along with what mood it is
	/// </summary>
	[System.Serializable]
	public class LipsyncPrestonBlairMouth
	{
		public string mood;
		public Sprite AI;
		public Sprite E;
		public Sprite etc;
		public Sprite FV;
		public Sprite L;
		public Sprite MBP;
		public Sprite O;
		public Sprite U;
		public Sprite WQ;
		public Sprite rest;
		
		/// <summary>
		/// Get phoneme based on index, handy if you need to enumerate over them
		/// </summary>
		/// <param name="i">The index.</param>
		public Sprite this[int i]
		{
			get
			{
				switch(i)
				{
				case 0:
					return AI;
					break;
				case 1:
					return E;
					break;
				case 2:
					return etc;
					break;
				case 3:
					return FV;
					break;
				case 4:
					return L;
					break;				
				case 5:
					return MBP;
					break;
				case 6:
					return O;
					break;						
				case 7:
					return U;
					break;
				case 8:
					return WQ;
					break;
				case 9:
					return rest;
					break;					
				}
				
				return null;
			}
			set
			{
				switch(i)
				{
				case 0:
					AI = value;
					break;
				case 1:
					E = value;
					break;
				case 2:
					etc = value;
					break;
				case 3:
					FV = value;
					break;
				case 4:
					L = value;
					break;				
				case 5:
					MBP = value;
					break;
				case 6:
					O = value;
					break;						
				case 7:
					U = value;
					break;
				case 8:
					WQ = value;
					break;
				case 9:
					rest = value;
					break;					
				}			
			}
		}
	}
}