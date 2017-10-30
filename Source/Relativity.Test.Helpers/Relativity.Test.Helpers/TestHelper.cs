﻿using System;
using Relativity.API;
using Relativity.Test.Helpers.ServiceFactory;
using kCura.Relativity.Client;
using System.Data.SqlClient;
// IHelper = Relativity.Test.Helpers.Interface.IHelper;
//using IServicesMgr = Relativity.Test.Helpers.Interface.IServicesMgr;

namespace Relativity.Test.Helpers
{
	public class TestHelper : IHelper
	{
        private readonly string _username;
        private readonly string _password;

        [Obsolete("Please use the TestHelper.System static method instead")]
        public TestHelper()
        {
            _username = SharedTestHelpers.ConfigurationHelper.ADMIN_USERNAME;
            _password = SharedTestHelpers.ConfigurationHelper.DEFAULT_PASSWORD;
        }

        private TestHelper(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public static IHelper ForUser(string username, string password)
        {
            return new TestHelper(username, password); 
        }

        public static IHelper System()
        {
            var username = SharedTestHelpers.ConfigurationHelper.ADMIN_USERNAME;
            var password = SharedTestHelpers.ConfigurationHelper.DEFAULT_PASSWORD;
            return ForUser(username, password);
        }


        public IDBContext GetDBContext(int caseID)
		{
			kCura.Data.RowDataGateway.Context context = new kCura.Data.RowDataGateway.Context(SharedTestHelpers.ConfigurationHelper.SQL_SERVER_ADDRESS, string.Format("EDDS{0}", caseID == -1 ? "" : caseID.ToString()), SharedTestHelpers.ConfigurationHelper.SQL_USER_NAME, SharedTestHelpers.ConfigurationHelper.SQL_PASSWORD);
			return new DBContext(context);
		}

		public Guid GetGuid(int workspaceID, int artifactID)
		{
            var sql = "select ArtifactGuid from eddsdbo.ArtifactGuid where artifactId = @artifactId";
            var context = GetDBContext(workspaceID);
            var result = context.ExecuteSqlStatementAsScalar<Guid>(sql, new SqlParameter("artifactId", artifactID));
            return result;
		}

		public ILogFactory GetLoggerFactory()
		{
			throw new NotImplementedException();
		}

		public string GetSchemalessResourceDataBasePrepend(IDBContext context)
		{
			throw new NotImplementedException();
		}

		public IServicesMgr GetServicesManager()
		{
			return new ServicesManager(_username, _password);
		}

		public IUrlHelper GetUrlHelper()
		{
            return new URLHelper();
		}

		public string ResourceDBPrepend()
		{
			throw new NotImplementedException();
		}

        public string ResourceDBPrepend(IDBContext context)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }


}
