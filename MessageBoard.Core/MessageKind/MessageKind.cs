using MessageBoard.Core.InformationKind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;

namespace MessageBoard.Core.MessageKind
{
	public abstract class MessageKind
	{
		public string Key { get; internal set; }
		public abstract string Title { get; }

		public virtual string RenderGlobalScript()
		{
			return string.Empty;
		}

		public virtual string RenderInstanceScript(int messageId, MessageKindSettingList settings)
		{
			return string.Empty;
		}

		public virtual string RenderHTML(int messageId, MessageKindSettingList settings, IInformationRepository informationRepository)
		{
			return string.Empty;
		}

		public abstract List<MessageKindSetting> Settings { get; }

		private static object lockObject = new object();
		private static List<MessageKind> list;
		public static List<MessageKind> List
		{
			get
			{
				if (list == null)
				{
					lock (lockObject)
					{
						if (list == null)
						{
							var result = new List<MessageKind>();

							foreach (Assembly assembly in BuildManager.GetReferencedAssemblies())
							{
								var messageKindBaseType = typeof(MessageKind);
								foreach (var assemblyType in assembly.GetTypes())
								{
									if (!assemblyType.IsAbstract && messageKindBaseType.IsAssignableFrom(assemblyType))
									{
										var messageKindInstance = (MessageKind)Activator.CreateInstance(assemblyType);
										messageKindInstance.Key = messageKindInstance.GetType().ToString();
										result.Add(messageKindInstance);
									}
								}
							}
							result = result.OrderBy(m => m.Title).ToList();
							list = result;
						}
					}
				}

				return list;
			}
		}

		public static MessageKind Select(string key)
		{
			return List.FirstOrDefault(m => m.Key == key);
		}
	}
}
