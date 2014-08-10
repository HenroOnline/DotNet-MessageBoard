﻿using MessageBoard.Core.InformationKind;
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

		public virtual string RenderHTML(MessageKindSettingList settings, IInformationRepository informationRepository)
		{
			return string.Empty;
		}

		public abstract List<MessageKindSetting> Settings { get; }

		private static List<MessageKind> list;
		public static List<MessageKind> List
		{
			get
			{
				if (list == null)
				{
					list = new List<MessageKind>();

					foreach (Assembly assembly in BuildManager.GetReferencedAssemblies())
					{
						var messageKindBaseType = typeof(MessageKind);
						foreach (var assemblyType in assembly.GetTypes())
						{
							if (!assemblyType.IsAbstract && messageKindBaseType.IsAssignableFrom(assemblyType))
							{
								var messageKindInstance = (MessageKind)Activator.CreateInstance(assemblyType);
								messageKindInstance.Key = messageKindInstance.GetType().ToString();
								list.Add(messageKindInstance);
							}
						}
					}

					list = list.OrderBy(m => m.Title).ToList();
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
