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
    internal abstract class DelegateCaster4Arg<T1, T2, T3, T4> : DelegateCaster
    {
        /// <summary>
        /// Конструктор объекта
        /// </summary>
        internal DelegateCaster4Arg(Delegate delegateObject) : base(delegateObject)
        {
        }
        /// <summary>
        /// Получить анонимный делегат действия
        /// </summary>
        /// <param name="actionDelegate"></param>
        /// <returns></returns>
        public static Action<object, object, object, object> _GetObjectiveAction(object actionDelegate)
        {
            if (actionDelegate is Action<T1, T2, T3, T4>)
            {
                return delegate (object p1, object p2, object p3, object p4)
                {
                    (actionDelegate as Action<T1, T2, T3, T4>)?.Invoke((T1)p1, (T2)p2, (T3)p3, (T4)p4);
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
        public static Func<object, object, object, object> _GetObjectiveFunction(object functionDelegate)
        {
            if (functionDelegate is Func<T1, T2, T3, T4>)
            {
                return delegate (object p1, object p2, object p3)
                {
                    return (functionDelegate as Func<T1, T2, T3, T4>).Invoke((T1)p1, (T2)p2, (T3)p3);
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
            if (delegateObject is Action<T1, T2, T3, T4>)
            {
                return new ActionCaster4Arg<T1, T2, T3, T4>(delegateObject);
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
            if (delegateObject is Func<T1, T2, T3, T4>)
            {
                return new FunctionCaster4Arg<T1, T2, T3, T4>(delegateObject);
            }
            return null;
        }
    }
    /// <summary>
    /// Класс кастинга анонимного делегата действия
    /// </summary>
    internal class ActionCaster4Arg<T1, T2, T3, T4> : DelegateCaster4Arg<T1, T2, T3, T4>
    {
        internal ActionCaster4Arg(Delegate delegateObject) : base(delegateObject)
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
                (_Delegate as Action<T1, T2, T3, T4>).Invoke((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3]);

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
    internal class FunctionCaster4Arg<T1, T2, T3, T4> : DelegateCaster4Arg<T1, T2, T3, T4>
    {
        internal FunctionCaster4Arg(Delegate delegateObject) : base(delegateObject)
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
                return (_Delegate as Func<T1, T2, T3, T4>).Invoke((T1)args[0], (T2)args[1], (T3)args[2]);
            }
            catch (Exception e)
            {
                throw new DelegateCastingErrorException(e.Message);
            }
        }
    }
}
