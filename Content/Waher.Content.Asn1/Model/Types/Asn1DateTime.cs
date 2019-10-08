﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Waher.Content.Asn1.Model.Types
{
	/// <summary>
	/// DATE-TIME
	/// </summary>
	public class Asn1DateTime : Asn1Type
	{
		/// <summary>
		/// DATE-TIME
		/// </summary>
		/// <param name="Implicit">Implicit type definition</param>
		public Asn1DateTime(bool Implicit)
			: base(Implicit)
		{
		}

		/// <summary>
		/// C# type reference.
		/// </summary>
		public override string CSharpTypeReference => "DateTime";

		/// <summary>
		/// If type is nullable.
		/// </summary>
		public override bool CSharpTypeNullable => false;
	}
}
