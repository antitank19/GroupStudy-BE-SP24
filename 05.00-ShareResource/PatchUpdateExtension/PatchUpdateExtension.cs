using ShareResource.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.UpdateApiExtension
{
    public static class PatchUpdateExtension
    {
        public static TDbEntity PatchUpdate<TDbEntity, TUpdateEntity>(this TDbEntity src, TUpdateEntity update)
           where TUpdateEntity : class
        {
            foreach (PropertyInfo field in update.GetType().GetProperties())
            {
                // check if the property has been set in the update
                // if it is null we ignore it. If you want to allow null values to be set, you could add a flag to the update object to allow specific nulls
                if (field.GetValue(update) != null)
                {
                    // if it has been set update the existing entity value
                    src.GetType().GetProperty(field.Name)?.SetValue(src, field.GetValue(update));
                }
            }
            return src;
        }
    }
}
