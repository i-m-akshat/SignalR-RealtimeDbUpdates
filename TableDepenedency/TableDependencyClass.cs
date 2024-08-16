using SignalRCRUDPractice.Models;
using System.Configuration;
using TableDependency.SqlClient;
using TableDependency;
using WebApplication1.Models;

namespace SignalRCRUDPractice.TableDepenedency
{
    public class TableDependencyClass
    {
        private readonly string ConnectionString="";
        private readonly IConfiguration _config;
        SignalRServer signalRhub;
        SqlTableDependency<Products> tableDependency;

        public TableDependencyClass(IConfiguration config,SignalRServer signalRServer)
        {
            _config = config;
            signalRhub = signalRServer;

        }
        public void ProductTableDependency(string ConnectionString)
        {
            //string ConnectionString = _config.GetConnectionString("DefaultConnection");
            tableDependency = new SqlTableDependency<Products>(ConnectionString);
            tableDependency.OnChanged += TableDependency_OnChanged; 
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Products> e)
        {
            
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                signalRhub.SendProducts();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Products)} SqlTable Dependency Error:{e.Error.Message}");
        }

      

    }
}
