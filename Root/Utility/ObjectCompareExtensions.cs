using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ObjectCompareExtensions
    {
        public static List<Variance> DetailedCompare<T>(this T val1, T val2)
        {
            List<Variance> variances = new List<Variance>();
            PropertyInfo[] fi = val1.GetType().GetProperties();
            foreach (PropertyInfo f in fi)
            {
                if (f.PropertyType == typeof(string))
                {
                    if (!f.GetValue(val1).Equals(f.GetValue(val2)))
                    {
                        variances.Add(new Variance
                        {
                            Prop = f.Name,
                            valA = f.GetValue(val1),
                            valB = f.GetValue(val2)
                        });
                    }
                }
                else if (f.PropertyType == typeof(DateTime))
                {
                    if (!f.GetValue(val1).Equals(f.GetValue(val2)))
                    {
                        variances.Add(new Variance
                        {
                            Prop = f.Name,
                            valA = f.GetValue(val1),
                            valB = f.GetValue(val2)
                        });
                    }
                }
                else
                {
                    if (f.ReflectedType.Name.Contains("List`1"))
                    {
                        var obj = f.GetValue(val1);
                    }
                    variances.AddRange(f.GetValue(val1).DetailedCompare(f.GetValue(val2)));
                }
            }
            return variances;
        }

    }
}
