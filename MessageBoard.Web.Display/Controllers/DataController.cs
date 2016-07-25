using MessageBoard.BLL.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Web.Display.Controllers
{
	public class DataController : Controller
	{
		public ActionResult GetData(int messageId)
		{
			var message = MessageRepository.Instance.Select(messageId);

			if (message == null)
			{
				return new ContentResult();
			}

			var messageKind = MessageBoard.Core.MessageKind.MessageKind.Select(message.MessageKind);
			if (messageKind == null)
			{
				return new ContentResult();
			}

			var settings = SettingRepository.Instance.ListAsMessageKindSetting(message.Id);
			var data = messageKind.GetData(message.Id, settings, Request.QueryString);

			if (data == null)
			{
				return new ContentResult();
			}

			return new ContentResult()
			{
				Content = JsonConvert.SerializeObject(data),
				ContentType = "application/json"
			};
		}
	}
}