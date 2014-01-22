namespace Locator.Mobile.Presentation.Base
{
	/// <summary>Модальные сообщения.</summary>
	public interface IMessageView
	{
		/// <summary>Информационное сообщение.</summary>
		void ShowInfo(string message);

		/// <summary>Информация об ошибке.</summary>
		void ShowError(string message);
	}
}

