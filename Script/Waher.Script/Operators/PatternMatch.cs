﻿using System;
using System.Collections.Generic;
using System.Text;
using Waher.Script.Model;

namespace Waher.Script.Operators
{
	/// <summary>
	/// Assignment operator.
	/// </summary>
	public class PatternMatch : BinaryOperator 
	{
		/// <summary>
		/// Assignment operator.
		/// </summary>
		/// <param name="Left">Left Operand.</param>
		/// <param name="Right">Right Operand.</param>
		/// <param name="Start">Start position in script expression.</param>
		/// <param name="Length">Length of expression covered by node.</param>
		public PatternMatch(ScriptNode Left, ScriptNode Right, int Start, int Length)
			: base(Left, Right, Start, Length)
		{
		}
	}
}
