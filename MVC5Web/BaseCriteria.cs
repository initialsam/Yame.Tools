using Microsoft.Ajax.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC5Web
{
    public class BaseCriteria<TModel>
    {
        private Dictionary<string, Expression<Func<TModel, bool>>> criterias = new Dictionary<string, Expression<Func<TModel, bool>>>();

        public List<Expression<Func<TModel, bool>>> EffectiveCriterias
        {
            get
            {
                return criterias.Select(pair => pair.Value).ToList();

            }
        }


        protected void TryAddCriteria(string propertyName, object value, Expression<Func<TModel, bool>> expression)
        {
            if (value == null)
            {
                deleteCriteria(propertyName);
            }
            else
            {
                addCriteria(propertyName, expression);
            }
        }

        private void addCriteria(string key, Expression<Func<TModel, bool>> expression)
        {
            if (criterias.ContainsKey(key))
            {
                criterias[key] = expression;
            }
            else
            {
                criterias.Add(key, expression);
            }
        }

        protected void deleteCriteria(string key)
        {
            if (criterias.ContainsKey(key)) { criterias.Remove(key); }
        }

        public static IQueryable<TModel> Search(IQueryable<TModel> query, List<Expression<Func<TModel, bool>>> criterias)
        {

            foreach (var expression in criterias)
            {
                query = query.Where(expression);
            }

            return query;
        }
    }
}
