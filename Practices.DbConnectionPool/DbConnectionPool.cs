﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Practices.DbConnectionPool
{
    public sealed class DbConnectionPool : ObjectPool
    {
        private DbConnectionPool() { }

        public static readonly DbConnectionPool Instance =
            new DbConnectionPool();

        private static string connectionString =
            @"server=(local);Trusted Connection=yes;database=northwind";

        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        protected override object Create()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        protected override bool Validate(object o)
        {
            try
            {
                SqlConnection conn = (SqlConnection)o;
                return !conn.State.Equals(ConnectionState.Closed);
            }
            catch (SqlException)
            {
                return false;
            }
        }

        protected override void Expire(object o)
        {
            try
            {
                SqlConnection conn = (SqlConnection)o;
                conn.Close();
            }
            catch (SqlException) { }
        }

        public SqlConnection BorrowDBConnection()
        {
            try
            {
                return (SqlConnection)base.GetObjectFromPool();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ReturnDBConnection(SqlConnection conn)
        {
            base.ReturnObjectToPool(conn);
        }
    }
}
