using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.MessageKind
{
	public abstract class MessageKind
	{
		public string Key { get; internal set; }
		public abstract string Title { get; }

		public abstract List<MessageKindSetting> Settings { get; }

		private static List<MessageKind> list;
		public static List<MessageKind> List
		{
			get
			{
				if (list == null)
				{
					list = new List<MessageKind>();

					var executingAssembly = Assembly.GetExecutingAssembly();
					var messageKindBaseType = typeof(MessageKind);
					foreach (var assemblyType in executingAssembly.GetTypes())
					{
						if (!assemblyType.IsAbstract && messageKindBaseType.IsAssignableFrom(assemblyType))
						{
							var messageKindInstance = (MessageKind)Activator.CreateInstance(assemblyType);
							messageKindInstance.Key = messageKindInstance.GetType().ToString();
							list.Add(messageKindInstance);
						}
					}
				}

				return list;
			}
		}
	}
}
