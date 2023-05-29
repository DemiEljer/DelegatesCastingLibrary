using DelegatesCastingLib.Casters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DelegatesCastingLib
{
    /// <summary>
    /// Базовый класс кастера делегатов
    /// </summary>
    public abstract class DelegateCaster
    {
        /// <summary>
        /// Объект обрабатываесого делагата
        /// </summary>
        protected Delegate _Delegate { get; } = null;
        /// <summary>
        /// Конструктор объекта кастера
        /// </summary>
        /// <param name="delegateObject"></param>
        internal DelegateCaster(Delegate delegateObject)
        {
            _Delegate = delegateObject;
        }
        /// <summary>
        /// Метод анонимного вызова делегата
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract object Invoke(params object[] args);

        #region Static area

        private static Dictionary<string, Type> _CastingTypes = new Dictionary<string, Type>();
        /// <summary>
        /// Статический конструктор кастера
        /// </summary>
        static DelegateCaster()
        {
            // Тип для кастинга делегатов от 1-ого аргумента типа
            _CastingTypes.Add(typeof(Action).Name, Type.GetType("DelegatesCastingLib.Casters.DelegateCaster0Arg"));
            // Тип для кастинга делегатов от 2-х аргументов типа
            _CastingTypes.Add(typeof(Action<int>).Name, Type.GetType("DelegatesCastingLib.Casters.DelegateCaster1Arg`1"));
            _CastingTypes.Add(typeof(Func<int>).Name, Type.GetType("DelegatesCastingLib.Casters.DelegateCaster1Arg`1"));
            // Тип для кастинга делегатов от 2-х аргументов типа
            _CastingTypes.Add(typeof(Action<int, int>).Name, Type.GetType("DelegatesCastingLib.Casters.DelegateCaster2Arg`2"));
            _CastingTypes.Add(typeof(Func<int, int>).Name, Type.GetType("DelegatesCastingLib.Casters.DelegateCaster2Arg`2"));
            // Тип для кастинга делегатов от 3-х аргументов типа
            _CastingTypes.Add(typeof(Action<int, int, int>).Name, Type.GetType("DelegatesCastingLib.Casters.DelegateCaster3Arg`3"));
            _CastingTypes.Add(typeof(Func<int, int, int>).Name, Type.GetType("DelegatesCastingLib.Casters.DelegateCaster3Arg`3"));
            // Тип для кастинга делегатов от 4-х аргументов типа
            _CastingTypes.Add(typeof(Action<int, int, int, int>).Name, Type.GetType("DelegatesCastingLib.Casters.DelegateCaster4Arg`4"));
            _CastingTypes.Add(typeof(Func<int, int, int, int>).Name, Type.GetType("DelegatesCastingLib.Casters.DelegateCaster4Arg`4"));
            // Тип для кастинга делегатов от 5-и аргументов типа
            _CastingTypes.Add(typeof(Action<int, int, int, int, int>).Name, Type.GetType("DelegatesCastingLib.Casters.DelegateCaster5Arg`5"));
            _CastingTypes.Add(typeof(Func<int, int, int, int, int>).Name, Type.GetType("DelegatesCastingLib.Casters.DelegateCaster5Arg`5"));
            // Тип для кастинга делегатов от 6-и аргументов типа
            _CastingTypes.Add(typeof(Action<int, int, int, int, int, int>).Name, Type.GetType("DelegatesCastingLib.Casters.DelegateCaster6Arg`6"));
            _CastingTypes.Add(typeof(Func<int, int, int, int, int, int>).Name, Type.GetType("DelegatesCastingLib.Casters.DelegateCaster6Arg`6"));
        }
        /// <summary>
        /// Получить тип кастера по типу делегата
        /// </summary>
        /// <param name="delegateType"></param>
        /// <returns></returns>
        private static Type _GetCasterType(Type delegateType)
        {
            if (_CastingTypes.ContainsKey(delegateType.Name))
            {
                // Создание конкретного экземпляра типа с подстановкой "правильных" аргумента типа
                return _CastingTypes[delegateType.Name]?.MakeGenericType(delegateType.GenericTypeArguments);
            }
            return null;
        }
        /// <summary>
        /// Получить объект приведенного делегата к типам Action<...object...> или Func<...object...>
        /// </summary>
        /// <param name="delegateObject"></param>
        /// <returns></returns>
        public static object GetObjective(object delegateObject)
        {
            try
            {
                // Получаем тип делегата
                Type delegateObjectType = delegateObject.GetType();
                // Получаем коректный тип объекта-обертки (фабрики)
                Type casterType = _GetCasterType(delegateObjectType);
                
                if (casterType is null) return null;
                // Если делегат явялется действием
                if (delegateObjectType.Name.Contains("Action"))
                {
                    // С помощью рефлексии создаем анонимный делегат действия
                    return casterType.GetMethod("_GetObjectiveAction").Invoke(null, new object[] { delegateObject });
                }
                // Если делегат явялется функцией
                else
                {
                    // С помощью рефлексии создаем анонимный делегат функции
                    return casterType.GetMethod("_GetObjectiveFunction").Invoke(null, new object[] { delegateObject });
                }
            }
            catch (Exception e)
            {
                throw new DelegateCasterCreationErrorException(e.Message);
            }
        }
        /// <summary>
        /// Получить объект приведенного делегата к типу Action<...object...>
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static object GetObjectiveAction(object action)
        {
            try
            {
                // Получаем тип делегата
                Type actionType = action.GetType();
                // Если делегат не явялется действием
                if (!actionType.Name.Contains("Action")) return null;

                Type casterType = _GetCasterType(actionType);

                if (casterType is null) return null;
                // С помощью рефлексии создаем анонимный делегат действия
                return casterType.GetMethod("_GetObjectiveAction").Invoke(null, new object[] { action });
            }
            catch (Exception e)
            {
                throw new DelegateCasterCreationErrorException(e.Message);
            }
        }
        /// <summary>
        /// Получить объект приведенного делегата к типу Func<...object...>
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public static object GetObjectiveFunction(object function)
        {
            try
            {
                // Получаем тип делегата
                Type functionType = function.GetType();
                // Если делегат не явялется функцией
                if (!functionType.Name.Contains("Func")) return null;
                // Получаем коректный тип объекта-обертки (фабрики)
                Type casterType = _GetCasterType(functionType);

                if (casterType is null) return null;
                // С помощью рефлексии создаем анонимный делегат функции
                return casterType.GetMethod("_GetObjectiveFunction").Invoke(null, new object[] { function });
            }
            catch (Exception e)
            {
                throw new DelegateCasterCreationErrorException(e.Message);
            }
        }
        /// <summary>
        /// Получить анонимый делегат для вызова посредствам метода Invoke(params object args[])
        /// </summary>
        /// <param name="delegateObject"></param>
        /// <returns></returns>
        public static DelegateCaster GetCaster(object delegateObject)
        {
            try
            {
                // Получаем тип делегата
                Type delegateObjectType = delegateObject.GetType();
                // Получаем коректный тип объекта-обертки
                Type casterType = _GetCasterType(delegateObjectType);

                if (casterType is null) return null;
                // Если делегат явялется действием
                if (delegateObjectType.Name.Contains("Action"))
                {
                    // С помощью рефлексии создаем конктетный обработчик делегата действия
                    return casterType.GetMethod("_GetActionCaster")?.Invoke(null, new object[] { delegateObject }) as DelegateCaster;
                }
                // Если делегат явялется функцией
                else
                {
                    // С помощью рефлексии создаем конктетный обработчик делегата функции
                    return casterType.GetMethod("_GetFunctionCaster")?.Invoke(null, new object[] { delegateObject }) as DelegateCaster;
                }
            }
            catch (Exception e)
            {
                throw new DelegateCasterCreationErrorException(e.Message);
            }
        }

        #endregion Static area
    }
}
