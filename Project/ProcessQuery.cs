using System;
using System.Linq.Expressions;
using Project.Controllers.Resources.Requests;
using Project.Database.Models;

namespace Project
{
    public static class ProcessQuery<T> where T:class
    {
        

        public static Expression<Func<T, bool>> Filter(FilterRequest filter)
        {
            try
            {
                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                MemberExpression property = Expression.Property(parameter, filter.FilterProperty);
                ConstantExpression constant = Expression.Constant(filter.FilterValue);
                BinaryExpression comparison = Expression.Equal(property, constant);
                Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);

                return lambda;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static Expression<Func<T, object>> Sort(string sortString)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            MemberExpression property = Expression.Property(parameter, sortString);
            Expression<Func<T, object>> lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

            return lambda;
        }
    }
}

