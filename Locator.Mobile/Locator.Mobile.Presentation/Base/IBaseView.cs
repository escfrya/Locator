using System;
using Locator.Mobile.Presentation.Base;

namespace Locator.Mobile.Presentation
{
	public interface IBaseView : IProgressView, IMessageView, IDispatcher, IDisposable
    {
		/// <summary>Обновлен</summary>
		bool WasRefreshed { set; }

		/// <summary>Первая загрузка</summary>
		bool IsFirstLoad { set; }

		/// <summary>Обновить</summary>
		event Action Refresh;
    }
}
