﻿using System;
using System.Collections.Generic;
using Waher.Script;

namespace Waher.Layout.Layout2D.Model.Groups
{
	/// <summary>
	/// Lays out cells in a grid.
	/// </summary>
	public class GridCells : ICellLayout
	{
		private readonly List<GridPadding> elements = new List<GridPadding>();
		private readonly Variables session;
		private readonly int nrColumns;
		private readonly float[] rights;
		private readonly List<float> bottoms;
		private int x = 0;
		private int y = 0;

		/// <summary>
		/// Lays out cells in a grid.
		/// </summary>
		/// <param name="Session">Current session</param>
		/// <param name="NrColumns">Number of columns in grid</param>
		public GridCells(Variables Session, int NrColumns)
		{
			this.session = Session;
			this.nrColumns = NrColumns;
			this.rights = new float[this.nrColumns];
			this.bottoms = new List<float>();
		}

		private GridPadding GetElement(int X, int Y)
		{
			if (X < 0 || X >= this.nrColumns)
				return null;

			if (Y < 0)
				return null;

			int i = Y * this.nrColumns + X;
			if (i >= this.elements.Count)
				return null;

			return this.elements[i];
		}

		private void IncPos()
		{
			this.x++;
			if (this.x >= this.nrColumns)
			{
				this.x = 0;
				this.y++;
			}
		}

		private float GetRight(int x)
		{
			if (x < 0)
				return 0;
			else
				return this.rights[x];
		}

		private float GetBottom(int y)
		{
			if (y < 0)
				return 0;

			int c = this.bottoms.Count;
			while (c <= y)
			{
				if (c == 0)
					this.bottoms.Add(0);
				else
					this.bottoms.Add(this.bottoms[c - 1]);

				c++;
			}

			return this.bottoms[y];
		}

		/// <summary>
		/// Adds a cell to the layout.
		/// </summary>
		/// <param name="Element">Cell element</param>
		public void Add(ILayoutElement Element)
		{
			int ColSpan;
			int RowSpan;

			if (Element is Cell Cell)
				Cell.CalcSpan(this.session, out ColSpan, out RowSpan);
			else
				ColSpan = RowSpan = 1;

			while (!(this.GetElement(this.x, this.y) is null))
				this.IncPos();

			this.SetElement(this.x, this.y, ColSpan, RowSpan, Element);

			int X2 = this.x + ColSpan - 1;
			if (X2 >= this.nrColumns)
				X2 = this.nrColumns - 1;

			float Right = this.GetRight(this.x - 1) + (Element.Width ?? 0);
			float Right2 = this.GetRight(X2);
			float Diff = Right - Right2;

			if (Diff > 0)
			{
				while (X2 < this.nrColumns)
					this.rights[X2++] += Diff;
			}

			int Y2 = this.y + RowSpan - 1;

			float Bottom = this.GetBottom(this.y - 1) + (Element.Height ?? 0);
			float Bottom2 = this.GetBottom(Y2);

			Diff = Bottom - Bottom2;
			if (Diff > 0)
			{
				int c = this.bottoms.Count;

				while (Y2 < c)
					this.bottoms[Y2++] += Diff;
			}

			this.x += ColSpan;
			if (this.x >= this.nrColumns)
			{
				this.x = 0;
				this.y++;
			}
		}

		private void SetElement(int X, int Y, int ColSpan, int RowSpan, ILayoutElement Element)
		{
			GridPadding P = new GridPadding(Element, 0, 0, X, Y, ColSpan, RowSpan);

			if (ColSpan > 1 || RowSpan > 1)
			{
				int i;
				bool First = true;

				for (; RowSpan > 0; RowSpan--)
				{
					for (i = 0; i < ColSpan; i++)
					{
						this.SetElement(X + i, Y, P);

						if (First)
						{
							First = false;
							P = new GridPadding(null, 0, 0, 0, 0, 0, 0);
						}
					}

					Y++;
				}
			}
			else
				this.SetElement(X, Y, P);
		}

		private void SetElement(int X, int Y, GridPadding Element)
		{
			if (X < 0 || X >= this.nrColumns)
				return;

			if (Y < 0)
				return;

			int i = Y * this.nrColumns + X;
			int c = this.elements.Count;

			while (i > c)
			{
				this.elements.Add(null);
				c++;
			}

			if (i == c)
				this.elements.Add(Element);
			else
				this.elements[i] = Element;
		}

		/// <summary>
		/// Total width of layout
		/// </summary>
		public float TotWidth => this.GetRight(this.nrColumns - 1);

		/// <summary>
		/// Total height of layout
		/// </summary>
		public float TotHeight => this.GetBottom(this.bottoms.Count - 1);

		/// <summary>
		/// Measures layout entities and defines unassigned properties, related to positions.
		/// </summary>
		/// <param name="State">Current drawing state.</param>
		public void MeasurePositions(DrawingState State)
		{
			ILayoutElement Element;

			foreach (GridPadding P in this.elements)
			{
				Element = P.Element;

				if (!(Element is null))
				{
					Element.MeasurePositions(State);
					P.OffsetX -= Element.Left ?? 0;
					P.OffsetY -= Element.Top ?? 0;
				}
			}
		}

		/// <summary>
		/// Aligns cells and returns an array of padded cells.
		/// </summary>
		/// <returns>Array of padded cells.</returns>
		public Padding[] Align()
		{
			List<Padding> Result = new List<Padding>();

			foreach (GridPadding P in this.elements)
			{
				if (!(P.Element is null))
				{
					int X = P.X;
					int Y = P.Y;
					float Left = this.GetRight(X - 1);
					float Top = this.GetBottom(Y - 1);
					float Right = this.GetRight(X + P.ColSpan - 1);
					float Bottom = this.GetBottom(Y + P.RowSpan - 1);
					float MaxWidth = Right - Left;
					float MaxHeight = Bottom - Top;

					P.OffsetX += Left;
					P.OffsetY += Top;
					P.AlignedMeasuredCell(MaxWidth, MaxHeight, this.session);
					Result.Add(P);
				}
			}

			return Result.ToArray();
		}
	}
}
