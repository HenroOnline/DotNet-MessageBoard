var messageBoard = messageBoard ||
	{
		init: function (fileManagerUrl)
		{
			tinymce.init({
				selector: "textarea", theme: "modern", width: 680, height: 300,
				plugins: [
						 "advlist autolink link image lists charmap print preview hr anchor pagebreak",
						 "searchreplace wordcount visualblocks visualchars insertdatetime media nonbreaking",
						 "table contextmenu directionality emoticons paste textcolor responsivefilemanager code"
				],
				toolbar1: "undo redo | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | styleselect",
				toolbar2: "| responsivefilemanager | link unlink anchor | image media | forecolor backcolor  | print preview code ",
				image_advtab: true,

				forced_root_block: "",
				force_br_newlines: true,
				force_p_newlines: false,
				entity_encoding: "named",

				filemanager_crossdomain: true,
				external_filemanager_path: fileManagerUrl,
				filemanager_title: "Responsive Filemanager",
				external_plugins: { "filemanager": "/Scripts/Libraries/tinymce//plugins/responsivefilemanager/plugin.min.js" }
			});
		}
	};
messageBoard.interaction = messageBoard.interaction ||
	{
		confirm: function(message)
		{
			return confirm(message);
		}
	};