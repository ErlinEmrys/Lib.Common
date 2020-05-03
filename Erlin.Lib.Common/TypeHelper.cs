using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common
{
    /// <summary>
    /// Helper class for runtime types
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// Bool runtime type
        /// </summary>
        public static Type TypeBool { get; } = typeof(bool);

        /// <summary>
        /// String runtime type
        /// </summary>
        public static Type TypeString { get; } = typeof(string);

        /// <summary>
        /// Decimal runtime type
        /// </summary>
        public static Type TypeDecimal { get; } = typeof(decimal);

        /// <summary>
        /// Double runtime type
        /// </summary>
        public static Type TypeDouble { get; } = typeof(double);

        /// <summary>
        /// Float runtime type
        /// </summary>
        public static Type TypeFloat { get; } = typeof(float);

        /// <summary>
        /// Timespan runtime type
        /// </summary>
        public static Type TypeTimespan { get; } = typeof(TimeSpan);

        /// <summary>
        /// Nullable runtime type
        /// </summary>
        public static Type TypeNullable { get; } = typeof(Nullable<>);

        /// <summary>
        /// Guid runtime type
        /// </summary>
        public static Type TypeGuid { get; } = typeof(Guid);

        /// <summary>
        /// Int32 runtime type
        /// </summary>
        public static Type TypeInt32 { get; } = typeof(int);

        /// <summary>
        /// Byte runtime type
        /// </summary>
        public static Type TypeByte { get; } = typeof(byte);

        /// <summary>
        /// IList runtime type
        /// </summary>
        public static Type TypeIList { get; } = typeof(IList);

        /// <summary>
        /// Check if enetred values runtime type is null-able (value can be null)
        /// </summary>
        /// <param name="value">Checked object</param>
        /// <returns>True if value can be null</returns>
        public static bool IsNullable(object? value)
        {
            return value == null || IsNullable(value.GetType());
        }

        /// <summary>
        /// Check if entered runtime type is null-able (value can be null)
        /// </summary>
        /// <param name="type">Checked runtime type</param>
        /// <returns>True if value can be null</returns>
        public static bool IsNullable(Type type)
        {
            if (!type.IsValueType)
            {
                return true;// ref-type
            }

            return Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        /// Check if entered runtime type is number with floating point
        /// </summary>
        /// <param name="type">Runtime type to check</param>
        /// <returns>True - runtime type is floating point number type</returns>
        public static bool IsFloatingNumber(Type type)
        {
            return type == TypeDecimal || type == TypeDouble || type == TypeFloat;
        }

        /// <summary>
        /// Unfolds runtime type from nullable type (Extract non-nullable type)
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Unfolded type from nullable type or original value</returns>
        public static Type UnfoldNullable(Type type)
        {
            Type? underlyingType = Nullable.GetUnderlyingType(type);
            return underlyingType ?? type;
        }

        /// <summary>
        /// Get name of the property or field from LINQ query (Example: GetPropName( ()=> IsReadOnly ); )
        /// </summary>
        /// <typeparam name="TProperty">Property type</typeparam>
        /// <param name="property">LINQ query</param>
        /// <returns>Name of the property in</returns>
        public static string GetPropertyName<TProperty>(Expression<Func<TProperty>> property)
        {
            LambdaExpression lambda = property;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression body)
            {
                UnaryExpression unaryExpression = body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member.Name;
        }
    }
}