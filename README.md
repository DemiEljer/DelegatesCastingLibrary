# DelegatesCastingLib
Данная библиотека разработана для предоставления механизма обезличенного вызова делегатов. Реализованна обработка делегатов до 6-ти аргументов типа.
## Что в итоге получается?
- Action<Type1, Type2, ...> -> Action<object, object, ...>;

- Func<Type1, Type2, ...> -> Func<Type1, Type2, ...>.
# Описание функций библиотеки
DelegateCaster - является базовым классом библиотеки.
## Методы класса DelegateCaster
- DelegateCaster.Invoke(params object[] args) - метод обезличенного вызова "обернутого" делегата;

- static DelegateCaster.GetObjective(object delegateObject) - метод получения обезличенного делегата (Action<...object...>) или (Func<...object...>);

- static DelegateCaster.GetObjectiveAction(object action) - метод получения обезличенного делегата действия (Action<...object...>);


- static DelegateCaster.GetObjectiveFunction(object function) - метод получения обезличенного делегата функции (Func<...object...>);


- static DelegateCaster.GetCaster(object delegateObject) - метод получения экземпляра кастера для вызова через метод Invoke(...).

# Пример кода
``` C#
/// <summary>
/// Конструктор абстрактного свойства
/// </summary>
public AbstractProperty(Type ownerType, PropertyInfo property, string name)
{
    Name = name;
    Type = property.PropertyType;

    _SetMethod = property.SetMethod;

    // Инициализация делегата установки значения свойства
    _IsSetMethodAvailable = _SetMethod != null && _SetMethod.IsPublic && _SetMethod.GetParameters().Length == 1;
    if (IsSetMethodAvailable)
    {
        // Получение обезличенного делегата установки значения свойства
        _SetMethodDelegate = (Action<object, object>)DelegateCaster.GetObjectiveAction(ReflectionMethodExtractor.GetMethodDelegate(ownerType, _SetMethod));
    }

    // Инициализация делегата получения значения свойства
    _GetMethod = property.GetMethod;

    _IsGetMethodAvailable = _GetMethod != null && _GetMethod.IsPublic && _GetMethod.GetParameters().Length == 0;
    if (IsGetMethodAvailable)
    {
        // Получение обезличенного делегата чтения значения свойтсва
        _GetMethodDelegate = (Func<object, object>)DelegateCaster.GetObjectiveFunction(ReflectionMethodExtractor.GetMethodDelegate(ownerType, _GetMethod));
    }
}
```
