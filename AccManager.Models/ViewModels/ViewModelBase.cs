using System.Linq;
using System.Reflection;

namespace AccManager.Models.ViewModels
{
    public abstract class ViewModelBase<T> where T : class
    {
        protected dynamic SetFromEntity(T entity)
        {
            var entityProperties = typeof(T).GetProperties();
            var thatType = this.GetType();

            foreach (var propertyInfo in entityProperties)
            {
                var pi = thatType.GetProperty(

                    propertyInfo.Name,

                    BindingFlags.IgnoreCase | BindingFlags.Instance |

                    BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.SetProperty

                    );

                if (pi != null && pi.PropertyType == propertyInfo.PropertyType && pi.CanWrite)
                {
                    pi.SetValue(this, propertyInfo.GetValue(entity));
                }
            }

            return this;
        }

        /// <summary>
        /// Map fields to provided model, except Primary Key and Foreign Key (e.g properties starts or ends with "Id").</summary>
        /// <param name="entity">Entity to be updated</param>
        public virtual T UpdateEntity(T entity)
        {
            var entityProperties = typeof(T).GetProperties(

                BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.SetProperty

                );

            var thatType = this.GetType();

            foreach (var propertyInfo in entityProperties.Where(x => x.Name != "Id" && !x.Name.EndsWith("Id")))
            {
                var pi = thatType.GetProperty(

                    propertyInfo.Name,

                    BindingFlags.IgnoreCase | BindingFlags.Instance |

                    BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.SetProperty

                    );

                if (pi != null && pi.PropertyType == propertyInfo.PropertyType && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(entity, pi.GetValue(this));
                }
            }

            return entity;
        }
    }
}
