using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Reflection;

namespace PrismAPI.Tools.FluentXS;

public class ArgumentBuilder
{
	#region Constructors

	public ArgumentBuilder(int I)
	{
		_TypeIndex = new();
		_Index = new();

		MethodBase? Info = new StackTrace().GetFrame(I)?.GetMethod()!;

		foreach (var parameter in Info.GetParameters().Reverse())
		{
			Add(parameter.ParameterType, parameter.Name ?? string.Empty);
		}
	}

	public ArgumentBuilder() : this(2) { }

	#endregion

	#region Methods

	public Type? GetType(string name) => _TypeIndex[name];

	public void Add<T>(string name, uint? index = null)
	{
		Add(typeof(T), name, index);
	}

	public void Add(Type type, string name, uint? index = null)
	{
		if (type.IsClass)
		{
			Add(4, name, index);	 // hard coded for now
									//size = (uint)IntPtr.Size;
		}
		else
		{
			Add((uint)Marshal.SizeOf(type), name, index);
		}

		_TypeIndex.Add(name, type);
	}

	public void Add(uint typeSize, string name, uint? index = null)
	{
		if (index.HasValue)
		{
			_Index.Insert((int)index, (name, (int)typeSize));
		}
		else
		{
			_Index.Add((name, (int)typeSize));
		}
	}

	[Pure]
	public PlugArgument GetArg<T>(Expression<Func<T>> expr)
	{
		var name = (expr.Body as MemberExpression)?.Member.Name;
		return new PlugArgument(name ?? string.Empty, Get(name ?? string.Empty), GetType(name ?? string.Empty));
	}

	[Pure]
	public PlugArgument GetArg(string name)
	{
		return new PlugArgument(name, Get(name), GetType(name));
	}

	[Pure]
	public int Get<T>(Expression<Func<T>> expr)
	{
		return Get((expr.Body as MemberExpression)?.Member.Name ?? string.Empty);
	}

	[Pure]
	public int Get(string Name)
	{
		bool IsFound = false;
		int I = 0;

		foreach ((string Name, int Size) Item in _Index)
		{
			I += Item.Size;

			if (Item.Name == Name)
			{
				IsFound = true;
				break;
			}
		}

		if (!IsFound)
		{
			throw new ArgumentException("Argument was not found.", Name);
		}

		return I;
	}

	[Pure]
	public string GetOffset(Expression<Func<object, object>> f)
	{
		return GetOffset((f.Body as MemberExpression)?.Member.Name ?? string.Empty);
	}

	[Pure]
	public string GetOffset(string name)
	{
		return $"[esp + {Get(name)}]";
	}

	#endregion

	#region Fields

	private readonly List<(string Name, int Size)> _Index;
	private readonly Dictionary<string, Type> _TypeIndex;

	#endregion
}