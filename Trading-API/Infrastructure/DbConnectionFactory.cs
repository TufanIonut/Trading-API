﻿using Microsoft.Data.SqlClient;
using System.Data;
using Trading_API.Interfaces;

namespace Trading_API.Infrastructure
{
    public class DbConnectionFactory : IDbConnectionFactory
        {
            IConfiguration _configuration;
            public DbConnectionFactory(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public IDbConnection ConnectToDataBase()
            {
                var conectionString = _configuration.GetConnectionString("DefaultConnection");
                IDbConnection connection = new SqlConnection(conectionString);
                connection.Open();
                return connection;
            }
        }
    }

