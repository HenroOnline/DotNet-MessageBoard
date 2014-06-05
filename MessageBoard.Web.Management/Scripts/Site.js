var messageBoard = messageBoard || {};
messageBoard.interaction = messageBoard.interaction ||
	{
		confirm: function(message)
		{
			return confirm(message);
		}
	};