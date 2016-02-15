﻿using System;
using System.Collections.Generic;
using System.Text;
using Waher.Script.Model;
using Waher.Script.Operators.Membership;

namespace Waher.Script.Operators.Assignments
{
	/// <summary>
	/// Named member Assignment operator.
	/// </summary>
	public class NamedMemberAssignment : UnaryOperator 
	{
		NamedMember namedMember;

		/// <summary>
		/// Named member Assignment operator.
		/// </summary>
		/// <param name="NamedMember">Named member</param>
		/// <param name="Operand">Operand.</param>
		/// <param name="Start">Start position in script expression.</param>
		/// <param name="Length">Length of expression covered by node.</param>
		public NamedMemberAssignment(NamedMember NamedMember, ScriptNode Operand, int Start, int Length)
			: base(Operand, Start, Length)
		{
			this.namedMember = NamedMember;
		}

		/// <summary>
		/// Named member.
		/// </summary>
		public NamedMember NamedMember
		{
			get { return this.namedMember; }
		}

	}
}
