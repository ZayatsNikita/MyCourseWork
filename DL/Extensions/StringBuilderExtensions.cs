using System;
using System.Text;

namespace DL.Extensions
{
    public static class StringBuilderExtensions
    {
        private const string Where = " where ";
        private const string Set = " set ";
        private const string And = " and ";

        public static void AddWhereWord(this StringBuilder query)
        {
            if (query != null)
            {
                query.Append(Where);
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        public static void AddSetWord(this StringBuilder query)
        {
            if (query != null)
            {
                query.Append(Set);
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        public static void AddWhereParam(this StringBuilder query, int minValue, int maxValue, string paramName)
        {
            paramName = AddSpacesToParamname(paramName);
            if (minValue != maxValue)
            {
                if (minValue != -1)
                {
                    if (query.Length > 7)
                    {
                        query.Append(And);
                    }
                    query.Append(paramName + " > " + minValue.ToString());
                }
                if (maxValue != -1)
                {
                    if (query.Length > 7)
                    {
                        query.Append(And);
                    }
                    query.Append(paramName  + " < " + maxValue.ToString());
                }
            }
            else
            {
                if (maxValue != -1)
                {
                    if (query.Length > 7)
                    {
                        query.Append(And);
                    }
                    query.Append(paramName+ " = " + maxValue.ToString());
                }
            }
        }
        public static void AddWhereParam(this StringBuilder query, decimal minValue, decimal maxValue, string paramName)
        {
            paramName = AddSpacesToParamname(paramName);
            
            if (minValue != maxValue)
            {
                if (minValue != -1)
                {
                    if (query.Length > 7)
                    {
                        query.Append(And);
                    }
                    query.Append(paramName + " > " + minValue.ToString().Replace(',', '.'));
                }
                if (maxValue != -1)
                {
                    if (query.Length > 7)
                    {
                        query.Append(And);
                    }
                    query.Append(paramName + " < " + maxValue.ToString().Replace(',', '.'));
                }
            }
            
            else
            {
                if (maxValue != -1)
                {
                    if (query.Length > 7)
                    {
                        query.Append(And);
                    }
                    query.Append(paramName + " = " + maxValue.ToString().Replace(',', '.'));
                }
            }
        }
        public static void AddWhereParam(this StringBuilder query, DateTime? minValue, DateTime? maxValue, string paramName)
        {
            paramName = AddSpacesToParamname(paramName);

            if (minValue != maxValue)
            {
                if (minValue != null)
                {
                    if (query.Length > 7)
                    {
                        query.Append(And);
                    }
                    query.Append(paramName + " > " + GetDate(minValue));
                }
                if (maxValue != null)
                {
                    if (query.Length > 7)
                    {
                        query.Append(And);
                    }
                    query.Append(paramName + " < " + GetDate(maxValue));
                }
            }
            else
            {
                if (maxValue != null)
                {
                    if (query.Length > 7)
                    {
                        query.Append(And);
                    }
                    query.Append(paramName + " = " + GetDate(maxValue));
                }
            }
        }
        public static void AddWhereParam(this StringBuilder query, string value, string paramName)
        {
            paramName = AddSpacesToParamname(paramName);
            if (value != null)
            {
                if (query.Length > 7)
                {
                    query.Append(" and ");
                }
                query.Append(paramName + " = \"" + value + "\"");
            }
        }
        public static void AddSetParam(this StringBuilder query, string value, string paramName)
        {
            paramName = AddSpacesToParamname(paramName);

            if (value != null)
            {
                if (query.Length > 5)
                {
                    query.Append(" , ");
                }
                query.Append(paramName + " = \"" + value + "\"");
            }
        }
        public static void AddSetParam(this StringBuilder query, decimal value, string paramName)
        {
            paramName = AddSpacesToParamname(paramName);

            if (value != -1)
            {
                if (query.Length > 5)
                {
                    query.Append(" , ");
                }
                query.Append(paramName + " = " + value.ToString().Replace(',', '.'));
            }
        }
        public static void AddSetParam(this StringBuilder query, int value, string paramName)
        {
            paramName = AddSpacesToParamname(paramName);

            if (value != -1)
            {
                if (query.Length > 5)
                {
                    query.Append(" , ");
                }
                query.Append(paramName + " = " + value.ToString());
            }
        }
        public static void AddSetParam(this StringBuilder query, System.DateTime? value, string paramName)
        {
            paramName = AddSpacesToParamname(paramName);

            if (value != null)
            {
                if (query.Length > 5)
                {
                    query.Append(" , ");
                }
                query.Append(paramName + " = " + GetDate(value));
            }
        }
        private static string GetDate(System.DateTime? value)
        {
            return "\'" + value.Value.ToShortDateString() + "\'";
        }
        private static string AddSpacesToParamname(string paramName)
        {
            paramName = paramName.PadLeft(paramName.Length + 1);
            paramName = paramName.PadRight(paramName.Length + 1);
            return paramName;
        }
    }
}
