﻿using System;
using System.Collections.Generic;
using System.Text;
using Waher.Script.Model;

namespace Waher.Script.Operators.Assignments
{
	/// <summary>
	/// Shift self left operator.
	/// </summary>
	public class ShiftSelfLeft : Assignment 
	{
		/// <summary>
		/// Shift self left operator.
		/// </summary>
		/// <param name="VariableName">Variable name..</param>
		/// <param name="Operand">Operand.</param>
		/// <param name="Start">Start position in script expression.</param>
		/// <param name="Length">Length of expression covered by node.</param>
		public ShiftSelfLeft(string VariableName, ScriptNode Operand, int Start, int Length)
			: base(VariableName, Operand, Start, Length)
		{
		}
	}
}
