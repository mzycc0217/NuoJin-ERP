using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.Mvc;

namespace XiAnOuDeERP.Models.Util
{
    public static class EnumExtensions
    {
        public static string GetDescription(this System.Enum obj)
        {
            return GetDescription(obj, false);
        }

        public static string GetDescription(this System.Enum obj, bool isTop)
        {
            if (obj == null)
                return string.Empty;

            try
            {
                Type enumType = obj.GetType();
                DescriptionAttribute dna;
                if (isTop)
                {
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(enumType, typeof(DescriptionAttribute));
                }
                else
                {
                    FieldInfo fi = enumType.GetField(System.Enum.GetName(enumType, obj));
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(
                       fi, typeof(DescriptionAttribute));
                }
                if (dna != null && string.IsNullOrEmpty(dna.Description) == false)
                    return dna.Description;
            }
            catch
            {

            }
            return obj.ToString();
        }

        public static List<SelectListItem> ToSelectList<T>()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in typeof(T).GetFields())
            {
                DescriptionAttribute dna = (DescriptionAttribute)Attribute.GetCustomAttribute(
                  item, typeof(DescriptionAttribute));
                if (dna != null)
                {
                    list.Add(new SelectListItem { Value = ((int)item.GetValue(typeof(T))).ToString(), Text = dna.Description });
                }
            }
            return list;
        }
    }
}

