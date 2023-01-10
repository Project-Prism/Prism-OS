// using System.Runtime.InteropServices

namespace PrismTools.Unsafe
{
	/// <summary>
	/// An unsafe class designed to allow using pointers for strings.
	/// </summary>
	public unsafe struct UnmanagedString
	{
		/// <summary>
		/// Creates a new instance of the <see cref="UnmanagedString"/> class.
		/// </summary>
		/// <param name="Value">Value to set as the string.</param>
		public UnmanagedString(string Value)
		{
			// Assign existing pointer.
			fixed (char* P = Value)
			{
				PrivateValue = P;
			}
		}

		/// <summary>
		/// Creates a new instance of the <see cref="UnmanagedString"/> class.
		/// </summary>
		/// <param name="Value">Value to set as the string.</param>
		public UnmanagedString(char* Value)
		{
			PrivateValue = Value;
		}

		#region Operators

		// Safe/Unsafe conversions.
		public static implicit operator UnmanagedString*(UnmanagedString Value)
		{
			return CreateUnsafe(Value.Value);
		}
		public static implicit operator UnmanagedString(UnmanagedString* Value)
		{
			return new(Value->Value);
		}

		// Assign strings to UnmanagedString and vise-versa.
		public static implicit operator UnmanagedString(string Value)
		{
			return new(Value);
		}
		public static implicit operator string(UnmanagedString Value)
		{
			return Value.Value;
		}

		// Concat for regular strings.
		public static UnmanagedString operator +(UnmanagedString Original, string ToAdd)
		{
			return CreateUnsafe(Original.Value + ToAdd);
		}

		// Equality operators.
		public static bool operator !=(UnmanagedString Value1, UnmanagedString Value2)
		{
			return Value1.Value != Value2.Value;
		}
		public static bool operator ==(UnmanagedString Value1, UnmanagedString Value2)
		{
			return Value1.Value == Value2.Value;
		}
		public static bool operator !=(UnmanagedString Value1, string Value2)
		{
			return Value1.Value != Value2;
		}
		public static bool operator ==(UnmanagedString Value1, string Value2)
		{
			return Value1.Value == Value2;
		}

		#endregion

		#region Indexers

		public char this[ulong Index]
		{
			get
			{
				if (Index >= (ulong)Value.Length)
				{
					throw new IndexOutOfRangeException($"'{nameof(Index)}' is out of the range of the string!");
				}

				return Value[(int)Index];
			}
			set
			{
				if (Index >= (ulong)Value.Length)
				{
					throw new IndexOutOfRangeException($"'{nameof(Index)}' is out of the range of the string!");
				}

				PrivateValue[Index] = value;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Creates an unsafe instance of the <see cref="UnmanagedString"/> class.
		/// </summary>
		/// <param name="Value">Value to set as the string.</param>
		/// <returns>Pointer to the new instance.</returns>
		public static UnmanagedString* CreateUnsafeNullTerminated(char* Value)
		{
			byte* Buffer = stackalloc byte[sizeof(UnmanagedString)];
			UnmanagedString* P = (UnmanagedString*)Buffer;

			// Add all chars from null-terminated string.
			for (int I = 0; Value[I] != '\0'; I++)
			{
				P->Value += Value[I];
			}

			return P;
		}

		/// <summary>
		/// Creates an unsafe instance of the <see cref="UnmanagedString"/> class.
		/// </summary>
		/// <param name="Value">Value to set as the string.</param>
		/// <returns>Pointer to the new instance.</returns>
		public static UnmanagedString* CreateUnsafe(string Value)
		{
			byte* Buffer = stackalloc byte[sizeof(UnmanagedString)];
			UnmanagedString* P = (UnmanagedString*)Buffer;
			P->Value = Value;
			return P;
		}

		/// <summary>
		/// Creates an unsafe instance of the <see cref="UnmanagedString"/> class.
		/// </summary>
		/// <returns>Pointer to the new instance.</returns>
		public static UnmanagedString* CreateUnsafe()
		{
			byte* Buffer = stackalloc byte[sizeof(UnmanagedString)];
			return (UnmanagedString*)Buffer;
		}

		#endregion

		#region Fields

		/// <summary>
		/// Gets/Sets the base string value.
		/// </summary>
		public string Value
		{
			get
			{
				return new string(PrivateValue);
			}
			set
			{
				// Free old data.
				// NativeMemory.Free(PrivateValue);

				// Assign new data.
				fixed (char* P = value)
				{
					PrivateValue = P;
				}
			}
		}

		/// <summary>
		/// The base string value, do not modify directly.
		/// </summary>
		private char* PrivateValue;

		#endregion
	}
}