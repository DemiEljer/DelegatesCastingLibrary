
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
    internal abstract class DelegateCaster2Arg<T1, T2> : DelegateCaster
    {
        /// <summary>
        /// Конструктор объекта
        /// </summary>
        internal DelegateCaster2Arg(Delegate delegateObject) : base(delegateObject)
        {
        }
        /// <summary>
        /// Получить анонимный делегат действия
        /// </summary>
        /// <param name="actionDelegate"></param>
        /// <returns></returns>
        public static Action<object, object> _GetObjectiveAction(object actionDelegate)
        {
            if (actionDelegate is Action<T1, T2>)
            {
                return delegate (object p1, object p2)
                {
                    (actionDelegate as Action<T1, T2>)?.Invoke((T1)p1, (T2)p2);
                };
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Получить анонимный делегат функции
        /// </summary>
        /// <param name="functionDelegate"></param>
        /// <returns></returns>
        public static Func<object, object> _GetObjectiveFunction(object functionDelegate)
        {
            if (functionDelegate is Func<T1, T2>)
            {
                return delegate (object p1)
                {
                    return (functionDelegate as Func<T1, T2>).Invoke((T1)p1);
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
            if (delegateObject is Action<T1, T2>)
            {
                return new ActionCaster2Arg<T1, T2>(delegateObject);
            }
            return null;
        }
        /// <summary>
        /// Получить объект кастера делегата функции
        /// </summary>
        /// <param name="delegateObject"></param>
        /// <returns></returns>
        public static DelegateCaster _GetFunctionCaster(Delegate delegateObject)
        {
            if (delegateObject is Func<T1, T2>)
            {
                return new FunctionCaster2Arg<T1, T2>(delegateObject);
            }
            return null;
        }
    }
    /// <summary>
    /// Класс кастинга анонимного делегата действия
    /// </summary>
    internal class ActionCaster2Arg<T1, T2> : DelegateCaster2Arg<T1, T2>
    {
        internal ActionCaster2Arg(Delegate delegateObject) : base(delegateObject)
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
            try
            {
                (_Delegate as Action<T1, T2>).Invoke((T1)args[0], (T2)args[1]);

                return null;
            }
            catch (Exception e)
            {
                throw new DelegateCastingErrorException(e.Message);
            }
        }
    }
    /// <summary>
    /// Класс кастинга анонимного делегата функции
    /// </summary>
    internal class FunctionCaster2Arg<T1, T2> : DelegateCaster2Arg<T1, T2>
    {
        internal FunctionCaster2Arg(Delegate delegateObject) : base(delegateObject)
        {
        }
        /// <summary>
        /// Реализованный метод вызова анонимного делегата функции
        /// </summary>
        /// <param name="args">Аргементы функции</param>
        /// <returns></returns>
        /// <exception cref="DelegateCastingErrorException"></exception>
        public override object Invoke(params object[] args)
        {
            try
            {
                return (_Delegate as Func<T1, T2>).Invoke((T1)args[0]);
            }
            catch (Exception e)
            {
                throw new DelegateCastingErrorException(e.Message);
            }
        }
    }
}
