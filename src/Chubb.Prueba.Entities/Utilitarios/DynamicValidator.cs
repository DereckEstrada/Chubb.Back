using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chubb.Prueba.Entities.Utilitarios
{
    public class DynamicValidator
    {
        public bool IsDynamicEmpty(dynamic obj)
        {
            if (obj is IEnumerable<dynamic> list)
            {
                return !list.Any();
            }
            else if (obj == null)
            {
                return true;
            }
            return false;
        }

    }
}
