﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var lst = db.Query<T>(query).ToList();
            return lst;
        }
        public T QueryFirstOrDefault<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var item = db.QueryFirstOrDefault<T>(query, param);
            return item;
        }

        public int ExecuteQuery(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            int result = db.Execute(query, param);
            return result;


        }




    }
}
