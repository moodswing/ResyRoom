using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ResyRoom.Infraestructura.Extensiones
{
    public static class ObjectQueryExt
    {
        /// <summary>  
        /// Specifies the related objects to include in the query results using  
        /// a lambda expression mentioning the path members.  
        /// </summary>  
        /// <returns>A new System.Data.Objects.ObjectQuery with the defined query path.</returns>  
        //public static DbQuery<T> Include<T>(this DbSet<T> query, Expression<Func<T, object>> path) where T : class
        //{
        //    return query.Include(NombrePropiedad(path));
        //}

        //public static DbQuery<T> Include<T>(this DbQuery<T> query, Expression<Func<T, object>> path) where T : class
        //{
        //    return query.Include(NombrePropiedad(path));
        //}

        internal static string NombrePropiedad<T>(Expression<Func<T, object>> path) where T : class
        {
            // Retrieve member path:  
            var members = new List<PropertyInfo>();
            CollectRelationalMembers(path, members);

            // Build string path:  
            var sb = new StringBuilder();
            var separator = "";
            foreach (var member in members)
            {
                sb.Append(separator);
                sb.Append(member.Name);
                separator = ".";
            }

            // Apply Include:  
            return sb.ToString();
        }

        internal static void CollectRelationalMembers(Expression exp, IList<PropertyInfo> members)
        {
            if (exp.NodeType == ExpressionType.Lambda)
            {
                // At root, explore body:  
                CollectRelationalMembers(((LambdaExpression)exp).Body, members);
            }
            else if (exp.NodeType == ExpressionType.MemberAccess)
            {
                var mexp = (MemberExpression)exp;
                CollectRelationalMembers(mexp.Expression, members);
                members.Add((PropertyInfo)mexp.Member);
            }
            else if (exp.NodeType == ExpressionType.Call)
            {
                var cexp = (MethodCallExpression)exp;

                if (cexp.Method.IsStatic == false)
                    throw new InvalidOperationException("Invalid type of expression.");

                foreach (var arg in cexp.Arguments)
                    CollectRelationalMembers(arg, members);
            }
            else if (exp.NodeType == ExpressionType.Parameter)
            {
                // Reached the toplevel:  
                return;
            }
            else
            {
                throw new InvalidOperationException("Invalid type of expression.");
            }
        }
    }
}