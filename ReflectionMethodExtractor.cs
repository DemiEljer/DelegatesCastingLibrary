using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesCastingLib
{
    /// <summary>
    /// Вспомогательный класс для реализации логики извлечения делегатов методов объектов
    /// </summary>
    public static class ReflectionMethodExtractor
    {
        /// <summary>
        /// Получить делегат метода объекта
        /// </summary>
        /// <param name="ownerType">Тип объекта-владельца метода</param>
        /// <param name="methodInfo">Металанные метода</param>
        /// <returns></returns>
        public static Delegate GetMethodDelegate(Type ownerType, MethodInfo methodInfo)
        {
            try
            {
                // Получаем аргумент вызываемого типа объекта-владельца
                var instance = Expression.Parameter(ownerType);

                // Получаем список аргументов вызываемого метода
                var methodParams = methodInfo.GetParameters().Select((p) => Expression.Parameter(p.ParameterType)).ToList();

                // Получаем непосредственно вызываемый метод
                var call = Expression.Call(instance, methodInfo, methodParams);

                // Вставляем на первую позицию аргмент типа объекта-владельца
                methodParams.Insert(0, instance);

                // "Запекаем" полученный метод для получения делегата
                return Expression.Lambda(call, methodParams).Compile();
            }
            catch
            {
                return null;
            }
        }
    }
}
