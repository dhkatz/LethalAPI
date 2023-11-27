// -----------------------------------------------------------------------
// <copyright file="CodeMatcherExtension.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Core;
using Events;
using HarmonyLib;
using Features;
using Interfaces;
using static HarmonyLib.AccessTools;

/// <summary>
/// Event related extension methods for <see cref="CodeMatcher"/>.
/// </summary>
public static class CodeMatcherExtension
{
    private static List<TypeInfo>? _types;

    /// <summary>
    /// Insert a log message at the current position of the matcher.
    /// </summary>
    /// <param name="matcher">A <see cref="CodeMatcher"/> instance.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="enabled">Whether the log message should be logged. For debugging purposes.</param>
    /// <returns>The <see cref="CodeMatcher"/> instance.</returns>
    public static CodeMatcher InsertLog(this CodeMatcher matcher, string message, bool enabled = true)
    {
        return matcher.InsertAndAdvance(
            new CodeInstruction(OpCodes.Ldstr, message),
            new CodeInstruction(OpCodes.Ldstr, string.Empty),
            new CodeInstruction(OpCodes.Ldc_I4_1, enabled),
            new CodeInstruction(OpCodes.Call, Method(typeof(Log), nameof(Log.Debug))));
    }

    /// <summary>
    /// Inserts instructions to emit an event at the current position of the matcher.
    /// </summary>
    /// <param name="matcher">A <see cref="CodeMatcher"/> instance.</param>
    /// <param name="originalMethod">The original method that is being patched.</param>
    /// <typeparam name="T">An <see cref="IEvent"/> type.</typeparam>
    /// <returns>The <see cref="CodeMatcher"/> instance.</returns>
    public static CodeMatcher InsertEvent<T>(this CodeMatcher matcher, MethodBase? originalMethod = null)
        where T : IEvent
    {
        var originalPos = matcher.Pos;
        var originalInstruction = matcher.Instruction;

        matcher
            .InsertAndAdvance(CreateEventParameters<T>(originalMethod))
            .InsertAndAdvance(CreateEventArgs<T>())
            .InsertAndAdvance(CreateEventAction<T>());

        if (originalInstruction.labels.Count <= 0)
        {
            return matcher;
        }

        matcher.Instructions()[originalPos].labels.AddRange(originalInstruction.labels);
        originalInstruction.labels.Clear();

        return matcher;
    }

    /// <summary>
    /// Insert instructions to emit a deniable event at the current position of the matcher.
    /// </summary>
    /// <param name="matcher">A <see cref="CodeMatcher"/> instance.</param>
    /// <param name="originalMethod">The original method that is being patched.</param>
    /// <typeparam name="T">An <see cref="IDeniableEvent"/> type.</typeparam>
    /// <returns>The <see cref="CodeMatcher"/> instance.</returns>
    public static CodeMatcher InsertDeniableEvent<T>(this CodeMatcher matcher, MethodBase? originalMethod = null)
        where T : IDeniableEvent
    {
        var originalPos = matcher.Pos;
        var originalInstruction = matcher.Instruction;

        matcher
            .CreateLabelAt(matcher.Length - 1, out var ret)
            .InsertAndAdvance(CreateEventParameters<T>(originalMethod))
            .InsertAndAdvance(CreateEventArgs<T>(), new CodeInstruction(OpCodes.Dup))
            .InsertAndAdvance(CreateEventAction<T>())
            .InsertAndAdvance(CreateEventReturn<T>(ret));

        if (originalInstruction.labels.Count > 0)
        {
            matcher.Instructions()[originalPos].labels.AddRange(originalInstruction.labels);
            originalInstruction.labels.Clear();
        }

        return matcher;
    }

    private static CodeInstruction CreateEventArgs<T>()
        where T : IEvent
    {
        return new CodeInstruction(OpCodes.Newobj, GetDeclaredConstructors(typeof(T))[0]);
    }

    private static CodeInstruction CreateEventAction<T>()
        where T : IEvent
    {
        var propertyInfo = GetPropertyInfo<T>();

        if (propertyInfo?.GetValue(null) is not Event<T> @event)
        {
            throw new Exception($"Failed to get event {typeof(T).Name}!");
        }

        var action = new Action<T>(eventArgs => @event.InvokeSafely(eventArgs));

        return Transpilers.EmitDelegate(action);
    }

    private static IEnumerable<CodeInstruction> CreateEventReturn<T>(Label ret)
        where T : IDeniableEvent
    {
        return new[]
        {
            new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(T), nameof(IDeniableEvent.IsAllowed))),
            new CodeInstruction(OpCodes.Brfalse, ret),
        };
    }

    private static PropertyInfo? GetPropertyInfo<T>()
        where T : IEvent
    {
        _types ??= typeof(CodeMatcherExtension).Assembly.DefinedTypes
            .Where(x => x.FullName?.Contains(".Handlers") ?? false).ToList();

        return _types
            .Select(type => type
                .GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .FirstOrDefault(pty => pty.PropertyType == typeof(Event<T>)))
            .FirstOrDefault(propertyInfo => propertyInfo is not null);
    }

    private static IEnumerable<CodeInstruction> CreateEventParameters<T>(MethodBase? originalMethod = null)
        where T : IEvent
    {
        if (originalMethod is null)
        {
            return new[] { new CodeInstruction(OpCodes.Ldarg_0) };
        }

        var originalMethodParameters = originalMethod.GetParameters();
        var eventConstructorParameters = GetDeclaredConstructors(typeof(T))[0].GetParameters();

        var parameterStack = new List<CodeInstruction>();
        for (var i = 0; i < eventConstructorParameters.Length; i++)
        {
            var parameter = eventConstructorParameters[i];

            if (i == 0 && parameter.ParameterType == originalMethod.DeclaringType && !originalMethod.IsStatic)
            {
                parameterStack.Insert(parameterStack.Count, new CodeInstruction(OpCodes.Ldarg_0));
                continue;
            }

            if (i == eventConstructorParameters.Length - 1 && parameter.ParameterType == typeof(bool))
            {
                parameterStack.Insert(parameterStack.Count, new CodeInstruction(OpCodes.Ldc_I4_1));
                continue;
            }

            for (var j = 0; j < originalMethodParameters.Length; j++)
            {
                var originalMethodParameter = originalMethodParameters[j];
                if (originalMethodParameter.ParameterType != parameter.ParameterType || originalMethodParameter.Name != parameter.Name)
                {
                    continue;
                }

                var index = j + (originalMethod.IsStatic ? 0 : 1);
                var instruction = index switch
                {
                    0 => new CodeInstruction(OpCodes.Ldarg_0),
                    1 => new CodeInstruction(OpCodes.Ldarg_1),
                    2 => new CodeInstruction(OpCodes.Ldarg_2),
                    3 => new CodeInstruction(OpCodes.Ldarg_3),
                    _ => new CodeInstruction(OpCodes.Ldarga_S, index)
                };

                parameterStack.Insert(parameterStack.Count, instruction);
            }
        }

        return parameterStack;
    }
}
