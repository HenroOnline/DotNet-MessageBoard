using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;

namespace MessageBoard.Core.InformationKind
{
	public abstract class InformationKind
	{
		public abstract string Title { get; }

		public virtual string SortKey
		{
			get
			{
				return Title;
			}
		}

		public virtual bool FreeContent { get; private set; }
		public virtual bool TabularData { get; private set; }

		public virtual int TabularDataDefaultRows
		{
			get
			{
				return 10;
			}
			private set
			{
				
			}
		}
		public virtual List<InformationColumn> TabularDataColumns
		{
			get;
			private set;
		}

		public string Key { get; internal set; }

		public virtual string RenderHTML(InformationDataList informationData)
		{
			return string.Empty;
		}

		private static object lockObject = new object();
		private static List<InformationKind> list;		
		public static List<InformationKind> List
		{
			get
			{
				if (list == null)
				{
					lock(lockObject)
					{
						// Double check
						if (list == null)
						{
							var result = new List<InformationKind>();

							foreach (Assembly assembly in BuildManager.GetReferencedAssemblies())
							{
								var informationKindBaseType = typeof(InformationKind);
								foreach (var assemblyType in assembly.GetTypes())
								{
									if (!assemblyType.IsAbstract && informationKindBaseType.IsAssignableFrom(assemblyType))
									{
										var informationKindInstance = (InformationKind)Activator.CreateInstance(assemblyType);
										informationKindInstance.Key = informationKindInstance.GetType().ToString();
										result.Add(informationKindInstance);
									}
								}
							}

							result = result.OrderBy(ik => ik.SortKey).ToList();
							list = result;
						}
					}					
				}

				return list;
			}
		}

		public static InformationKind Select(string key)
		{
			return List.FirstOrDefault(m => m.Key == key);
		}
	}
}
