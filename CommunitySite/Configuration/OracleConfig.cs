using Oracle.ManagedDataAccess.Client;

namespace CommunitySite.Configuration
{
    public class OracleConfig
    {
        public void Connection()
        {
            var builder = new OracleConnectionStringBuilder();


            OracleConfiguration.OracleDataSources.Add("orclpdb", "(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=664740b0eb8d)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=<service name>)(SERVER=dedicated)))");

        }
    }
}
