﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryAPI.Config {
    public class ConnectionContext {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public ConnectionContext(IConfiguration configuration) {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
