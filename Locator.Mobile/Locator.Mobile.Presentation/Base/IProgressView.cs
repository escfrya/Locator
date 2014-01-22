namespace Locator.Mobile.Presentation.Base
{
	/// <summary>Отображение прогреса</summary>
	public interface IProgressView
	{
		/// <summary>Прогресс</summary>
		bool Progress { set; }

		/// <summary>Передача данных по сети</summary>
		bool Transfer { set; }

		/// <summary>Блокирующее сообщение</summary>
		void ShowLockMessage(string message);

		/// <summary>Разблокировать сообщение</summary>
		void HideLockMessage();
	}
}

