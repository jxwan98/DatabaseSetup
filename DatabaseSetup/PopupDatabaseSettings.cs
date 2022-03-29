using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseSetup
{
    public class PopupDatabaseSettings : IPopupDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string UserDatabaseName { get; set; }
        public string GlobalDatabaseName { get; set; }
    }

    public interface IPopupDatabaseSettings
    {
        string ConnectionString { get; set; }
        string UserDatabaseName { get; set; }
        string GlobalDatabaseName { get; set; }
    }
}
