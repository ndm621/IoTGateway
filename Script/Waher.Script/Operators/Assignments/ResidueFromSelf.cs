﻿using System;
using System.Collections.Generic;
using System.Text;
using Waher.Script.Model;

namespace Waher.Script.Operators.Assignments
{
	/// <summary>
	/// Residue from self operator.
	/// </summary>
	public class ResidueFromSelf : Assignment 
	{
		/// <summary>
		/// Residue from self operator.
		/// </summary>
		/// <param name="VariableName">Variable name..</param>
		/// <param name="Operand">Operand.</param>
		/// <param name="Start">Start position in script expression.</param>
		/// <param name="Length">Length of expression covered by node.</param>
		public ResidueFromSelf(string VariableName, ScriptNode Operand, int Start, int Length)
			: base(VariableName, Operand, Start, Length)
		{
		}
	}
}
