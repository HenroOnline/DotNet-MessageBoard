using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.MessageKind
{
	public class ImageCarouselMessageKind : MessageKind
	{
		public override string Title
		{
			get { return "Afbeelding slide show"; }
		}

		public override List<MessageKindSetting> Settings
		{
			get
			{
				return new List<MessageKindSetting>()
				{
					MessageKindSetting.Create("InformationHeader", "Afbeeldingen", SettingKind.Information),
					MessageKindSetting.Create("RotationSpeed", "Rotatiesnelheid", SettingKind.Text)
				};
			}
		}

		public override string RenderGlobalScript(string dataUrl)
		{
			return @"messageBoard.messages.imageCarouselMessageKind = messageBoard.messages.imageCarouselMessageKind || 
								{
									init: function()
									{
										var imageCarousels = $('div[data-role=""MessageBoard.Core.MessageKind.ImageCarouselMessageKind""]');
										imageCarousels.each(function()
										{
											var imageCarousel = $(this);

											var availableImages = $('img', imageCarousel);
											if (availableImages.length == 0)
											{
												return;
											}

											if (availableImages.length > 1)
											{
												messageBoard.messages.imageCarouselMessageKind.showNextImage(imageCarousel);
											}
											else 
											{
												$(availableImages[0]).show();
											}
										});
									},

									showNextImage: function(imageCarousel)
									{
										var availableImages = $('img', imageCarousel);
										var currentVisibleImage = availableImages.filter(':visible');
										var currentImageIndex = -1;
										if (currentVisibleImage != undefined && currentVisibleImage != null)
										{
											currentImageIndex = availableImages.index(currentVisibleImage);
										}

										if (currentImageIndex == -1 || currentImageIndex == (availableImages.length - 1))
										{
											currentImageIndex = 0;
										}
										else
										{
											currentImageIndex += 1;
										}	

										messageBoard.messages.imageCarouselMessageKind.toggleImage(currentVisibleImage, $(availableImages[currentImageIndex]));

										var rotationspeed = parseInt(imageCarousel.attr('data-rotationspeed')) * 1000;

										setTimeout(function() { messageBoard.messages.imageCarouselMessageKind.showNextImage(imageCarousel);  }, rotationspeed);
									},

									toggleImage: function(imageToHide, imageToShow)
									{
										if (imageToHide != undefined && imageToHide != null && imageToHide.length > 0)
										{
											imageToHide.fadeToggle('slow', 'linear', function () {
												imageToShow.fadeToggle('slow', 'linear');
											});
										}
										else
										{
											imageToShow.show();
										}
									}
								};

								$(function() { messageBoard.messages.imageCarouselMessageKind.init(); } );";
		}

		public override string RenderHTML(int messageId, MessageKindSettingList settings, InformationKind.IInformationRepository informationRepository, string dataUrl)
		{
			var informationHeaderSetting = settings["InformationHeader"];
			if (informationHeaderSetting == null || informationHeaderSetting.IntValue == 0)
			{
				return string.Empty;
			}

			var informationKind = informationRepository.RetrieveInformationKind(informationHeaderSetting.IntValue);
			if (informationKind == null)
			{
				return string.Empty;
			}

			var rotationSpeedSetting = settings["RotationSpeed"];
			// Default of 30 seconds
			var rotationSpeed = 30;
			if (rotationSpeedSetting != null && rotationSpeedSetting.IntValue > 0)
			{
				rotationSpeed = rotationSpeedSetting.IntValue;
			}			

			var informationData = informationRepository.RetrieveData(informationHeaderSetting.IntValue);
			var informationHTML = informationKind.RenderHTML(informationData);

			if (string.IsNullOrWhiteSpace(informationHTML))
			{
				return string.Empty;
			}

			return string.Format("<div data-role=\"MessageBoard.Core.MessageKind.ImageCarouselMessageKind\" data-id=\"{0}\" data-rotationspeed=\"{2}\">{1}</div>", messageId, informationHTML, rotationSpeed);
		}
	}
}
