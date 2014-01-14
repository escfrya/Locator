using System;

namespace Locator.Mobile.Presentation
{
    public interface IDispatcher
    {
        /// <summary>
        /// Выполнить действие в UI потоке
        /// </summary>
        /// <param name="action">Действие</param>
        void Invoke(Action action);
    }
}
