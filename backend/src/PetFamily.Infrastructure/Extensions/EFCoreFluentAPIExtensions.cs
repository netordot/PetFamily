﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Extensions
{
    public static class EFCoreFluentAPIExtensions
    {
        public static PropertyBuilder<IReadOnlyList<TValueObject>> ValueObjectJsonConversion<TValueObject, TDto>(
            this PropertyBuilder<IReadOnlyList<TValueObject>> builder,
            Func<TValueObject, TDto> toDtoSelector,
            Func<TDto, TValueObject> toValueObjectSelector)
        {
            return builder.HasConversion(
                valueObjects => SerializeValueObjectCollection(valueObjects, toDtoSelector),
                json => DeserializeValueObjectCollection(json, toValueObjectSelector),
            new ValueComparer<IReadOnlyList<TValueObject>> (
                (c1,c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a,v) => HashCode.Combine(a,v!.GetHashCode())), 
                c => (IReadOnlyList<TValueObject>)c.ToList()))
                .HasColumnType("jsonb");
        }


        private static string SerializeValueObjectCollection<TValueObject, TDto>(
            IReadOnlyList<TValueObject> valueObjects, Func<TValueObject, TDto> selector)
        {
            var dtos = valueObjects.Select(selector);

            return JsonSerializer.Serialize(dtos, JsonSerializerOptions.Default);
        }

        private static IReadOnlyList<TValueObject> DeserializeValueObjectCollection<TValueObject, TDto>(
           string json, Func<TDto, TValueObject> selector)
        {
            var dtos = JsonSerializer.Deserialize<IEnumerable<TDto>>(json, JsonSerializerOptions.Default) ?? [];

            return dtos.Select(selector).ToList();
        }

    }
}