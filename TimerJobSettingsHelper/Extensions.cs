using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TimerJobSettingsHelper
{
    public static class Extensions
    {
        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static Object GetFieldValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                FieldInfo info = type.GetField(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj);
            }
            return obj;
        }

        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }

        public static T GetFieldValue<T>(this Object obj, String name)
        {
            Object retval = GetFieldValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }

        public static void SetFieldValue(this Object obj, String name, Object value)
        {
            if (obj == null) { return; }

            Type type = obj.GetType();
            FieldInfo info = type.GetField(name);
            if (info == null) { return; }

            info.SetValue(obj, value);
            return;
        }
    }
}
