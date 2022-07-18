using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LearnAzure.Admin
{
    public static class ObjectHelpers
    {
        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }


        /// <summary>
        /// Maps the source object to target object.
        /// </summary>
        /// <typeparam name="T">Type of target object.</typeparam>
        /// <typeparam name="TU">Type of source object.</typeparam>
        /// <param name="target">Target object.</param>
        /// <param name="source">Source object.</param>
        /// <returns>Updated target object.</returns>
        public static T Map<T, TU>(T target, TU source) where T : class
        {
            // get property list of the target object.
            // this is a reflection extension which simply gets properties (CanWrite = true).
            var tprops = target.GetType().GetProperties();

            tprops.ToList().ForEach(prop =>
            {
                // check whether source object has the the property
                var sp = source.GetType().GetProperty(prop.Name);
                if (sp != null)
                {
                    // if yes, copy the value to the matching property
                    var value = sp.GetValue(source, null);
                    target.GetType().GetProperty(prop.Name).SetValue(target, value, null);
                }
            });

            return target;
        }
    }
}
