using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanowisko.Persistance
{
    public abstract class DAO
    {
        private readonly DBConnection _connection;

        protected DAO(DBConnection connection)
        {
            _connection = connection;
        }
    }
}
