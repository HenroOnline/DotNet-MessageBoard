using MessageBoard.BLL.Repositories;
using MessageBoard.Core.MessageKind;
using MessageBoard.Web.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Web.Management.Models.Slide
{
	public class MessageDetailViewModel : BaseViewModel
	{
		public MessageModel Message { get; set; }

		public List<SelectListItem> MessageKindDropDownList { get; set; }

		public List<MessageKindModel> MessageKindList { get; set; }

		public List<SelectListItem> BooleanValuesDropDownList { get; set; }

		public List<SelectListItem> InformationHeaderDropDownList { get; set; }

		public List<InformationHeaderModel> InformationHeaderList { get; set; }

		public MessageDetailViewModel()
		{
			Menu = "Slide";
		}

		public static MessageDetailViewModel Create(int id, int layerId)
		{
			var result = new MessageDetailViewModel();

			DAL.Entity.Message dbMessage = null;
			if (id != 0)
			{
				dbMessage = MessageRepository.Instance.Select(id);
			}
			else
			{
				dbMessage = MessageRepository.Instance.NewEntity();
				dbMessage.LayerId = layerId;
			}

			DAL.Entity.Layer dbLayer = null;
			if (layerId != 0)
			{
				dbLayer = LayerRepository.Instance.Select(layerId);
			}
			else
			{
				dbLayer = LayerRepository.Instance.Select(dbMessage.LayerId);
			}

			var dbSlide = SlideRepository.Instance.Select(dbLayer.SlideId);

			result.Message = MessageModel.Create(dbMessage, dbLayer);

			result.MessageKindDropDownList = new List<SelectListItem>();
			result.MessageKindList = new List<MessageKindModel>();

			result.MessageKindDropDownList.Add(new SelectListItem());

			foreach (var messageKind in MessageKind.List)
			{
				result.MessageKindDropDownList.Add(new SelectListItem
					{
						Text = messageKind.Title,
						Value = messageKind.Key
					});

				result.MessageKindList.Add(MessageKindModel.Create(messageKind, dbMessage.Id, messageKind.Key == dbMessage.MessageKind));
			}

			result.BooleanValuesDropDownList = new List<SelectListItem>();
			result.BooleanValuesDropDownList.Add(new SelectListItem { Text = string.Empty, Value = string.Empty });
			result.BooleanValuesDropDownList.Add(new SelectListItem { Text = "Ja", Value = Boolean.TrueString });
			result.BooleanValuesDropDownList.Add(new SelectListItem { Text = "Nee", Value = Boolean.FalseString });

			result.InformationHeaderDropDownList = new List<SelectListItem>();
			result.InformationHeaderList = new List<InformationHeaderModel>();
			foreach (var informationHeader in InformationHeaderRepository.Instance.ListOrderedByName())
			{
				result.InformationHeaderDropDownList.Add(new SelectListItem
					{
						Text = informationHeader.Name,
						Value = informationHeader.Id.ToString()
					});

				result.InformationHeaderList.Add(InformationHeaderModel.Create(informationHeader, false));
			}

			result.AddCrumblePath("Slides", "~/Slide");
			result.AddCrumblePath(dbSlide.Description, string.Concat("~/Slide/Detail/", dbSlide.Id));
			result.AddCrumblePath(dbLayer.Description, string.Format("~/Slide/LayerDetail/?slideId={0}&id={1}", dbSlide.Id, dbLayer.Id));
			result.AddCrumblePath((result.Message.Id == 0) ? "Bericht toevoegen" : result.Message.Description, string.Format("~/Slide/MessageDetail/?layerId={0}&id={1}", dbLayer.Id, result.Message.Id));

			return result;
		}

		public void Save()
		{
			DAL.Entity.Message dbMessage = null;
			if (Message.Id != 0)
			{
				dbMessage = MessageRepository.Instance.Select(Message.Id);
			}

			if (dbMessage == null)
			{
				dbMessage = new DAL.Entity.Message();
				dbMessage.LayerId = Message.LayerId;
			}

			dbMessage.Description = Message.Description;
			dbMessage.PositionX = Message.PositionX;
			dbMessage.PositionY = Message.PositionY;
			dbMessage.Height = Message.Height;
			dbMessage.Width = Message.Width;
			dbMessage.MessageKind = Message.MessageKind;

			MessageRepository.Instance.Save(dbMessage);
			Message.Id = dbMessage.Id;

			var messageKind = MessageKindList.FirstOrDefault(mk => mk.Key == dbMessage.MessageKind);

			var validSettingIds = new List<int>();
			foreach (var setting in messageKind.Settings)
			{
				var dbSetting = SettingRepository.Instance.Select(dbMessage.Id, setting.Key);
				if (dbSetting == null)
				{
					dbSetting = new DAL.Entity.Setting();
					dbSetting.MessageId = dbMessage.Id;
					dbSetting.Key = setting.Key;
				}
				dbSetting.StringValue = setting.Value;
				SettingRepository.Instance.Save(dbSetting);

				validSettingIds.Add(dbSetting.Id);
			}

			foreach (var dbSetting in SettingRepository.Instance.List(dbMessage.Id))
			{
				if (!validSettingIds.Contains(dbSetting.Id))
				{
					SettingRepository.Instance.Delete(dbSetting);
				}
			}
		}

		public void Delete()
		{
			var dbMessage = MessageRepository.Instance.Select(Message.Id);
			if (dbMessage != null)
			{
				MessageRepository.Instance.Delete(dbMessage);
			}
		}
	}
}