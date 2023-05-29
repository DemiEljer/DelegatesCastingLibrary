using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesCastingLib.Casters
{
    /// <summary>
    /// Класс для кастинга делегатов
    /// </summary>
    internal abstract class DelegateCaster0Arg : DelegateCaster
    {
        /// <summary>
        /// Конструктор объекта
        /// </summary>
        internal DelegateCaster0Arg(Delegate delegateObject) : base(delegateObject)
        {
        }
        /// <summary>
        /// Получить объект кастера делегата действия
        /// </summary>
        /// <param name="delegateObject"></param>
        /// <returns></returns>
        public static Action<object> _GetObjectiveAction(object actionDelegate)
        {
            if (actionDelegate is Action)
            {
                return delegate (object p1)
                {
                    (actionDelegate as Action)?.Invoke();
                };
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Получить объект кастера делегата действия
        /// </summary>
        /// <param name="delegateObject"></param>
        /// <returns></returns>
        public static DelegateCaster _GetActionCaster(Delegate delegateObject)
        {
            if (delegateObject is Action)
            {
                return new ActionCaster0Arg(delegateObject);
            }
            return null;
        }
    }
    /// <summary>
    /// Класс кастинга анонимного делегата действия
    /// </summary>
    internal class ActionCaster0Arg : DelegateCaster0Arg
    {
        internal ActionCaster0Arg(Delegate delegateObject) : base(delegateObject)
        {
        }
        /// <summary>
        /// Реализованный метод вызова анонимного делегата действия
        /// </summary>
        /// <param name="args">Аргументы действия</param>
        /// <returns></returns>
        /// <exception cref="DelegateCastingErrorException"></exception>
        public override object Invoke(params object[] args)
        {
            if (_Delegate is Action)
            {
                (_Delegate as Action)?.Invoke();
            }
            return null;
        }
    }
}
