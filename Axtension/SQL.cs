namespace Axtension
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Text;
    using System.Threading;

    public class SQL
    {
        public static SqlConnection ConnectionObject = null;
        public static SqlCommand CommandObject = null;
        public Exception LastError = null;

        public SQL()
        {
        }

        public void NullifyLastError()
        {
            LastError = null;
        }

        public SQL(string connectionString)
        {
            LastError = null;
            if (DebugPoints.DebugPointRequested("SQL"))
            {
                Debugger.Launch();
            }

            try
            {
                this.Connect(connectionString);
            }
            catch (Exception E)
            {
                LastError = E;
            }
        }

        public void Connect(string connectionString)
        {
            LastError = null;
            if (DebugPoints.DebugPointRequested("SQL.Connect"))
            {
                Debugger.Launch();
            }
            try
            {
                ConnectionObject = new SqlConnection(connectionString);
                ConnectionObject.Open();
                while (ConnectionObject.State == ConnectionState.Connecting)
                {
                    Thread.Sleep(1);
                }
                CommandObject = ConnectionObject.CreateCommand();
            }
            catch (Exception E)
            {
                LastError = E;
            }
        }

        public string Eval(string sql, int timeout = 60)
        {
            LastError = null;
            if (DebugPoints.DebugPointRequested("SQL.Eval"))
            {
                Debugger.Launch();
            }

            StringBuilder evalResult = new StringBuilder();
            DataTable table = new DataTable();
            CommandObject.CommandText = sql;
            CommandObject.CommandTimeout = timeout;
            CommandObject.CommandType = CommandType.Text;
            try
            {
                using (SqlDataReader reader = CommandObject.ExecuteReader())
                {
                    table.Load(reader);
                }


                foreach (var col in table.Columns)
                {
                    evalResult.Append(col.ToString() + ",");
                }

                evalResult.Replace(",", System.Environment.NewLine, evalResult.Length - 1, 1);

                foreach (DataRow dr in table.Rows)
                {
                    foreach (var column in dr.ItemArray)
                    {
                        var typ = column.GetType();
                        evalResult.Append("\"" + column.ToString() + "\",");
                    }

                    evalResult.Replace(",", System.Environment.NewLine, evalResult.Length - 1, 1);
                }

                table.Dispose();
                LastError = null;
            }
            catch (Exception E)
            {
                LastError = E;
                return null;
            }

            return evalResult.ToString();
        }

        public string Eval(SqlConnection connection, string sql, int timeout = 60)
        {
            LastError = null;
            if (DebugPoints.DebugPointRequested("SQL.Eval"))
            {
                Debugger.Launch();
            }

            SqlCommand command = connection.CreateCommand();
            StringBuilder evalResult = new StringBuilder();
            DataTable table = new DataTable();
            command.CommandText = sql;
            command.CommandTimeout = timeout;
            command.CommandType = CommandType.Text;
            while (command.Connection.State == ConnectionState.Connecting)
            {
                Thread.Sleep(1);
            }
            using (SqlDataReader reader = command.ExecuteReader())
            {
                table.Load(reader);
            }

            foreach (var col in table.Columns)
            {
                evalResult.Append(col.ToString() + ",");
            }

            evalResult.Replace(",", System.Environment.NewLine, evalResult.Length - 1, 1);

            foreach (DataRow dr in table.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    var typ = column.GetType();
                    evalResult.Append("\"" + column.ToString() + "\",");
                }

                evalResult.Replace(",", System.Environment.NewLine, evalResult.Length - 1, 1);
            }

            table.Dispose();
            return evalResult.ToString();
        }

        public DataTable DTEval(string sql, int timeout = 60)
        {
            LastError = null;
            if (DebugPoints.DebugPointRequested("SQL.DTEval"))
            {
                Debugger.Launch();
            }

            DataTable table = new DataTable();
            try
            {
                CommandObject.CommandType = CommandType.Text;
                CommandObject.CommandTimeout = timeout;
                CommandObject.CommandText = sql;
                while (CommandObject.Connection.State == ConnectionState.Connecting)
                {
                    Thread.Sleep(1);
                }
                using (SqlDataReader reader = CommandObject.ExecuteReader())
                {
                    table.Load(reader);
                }
                LastError = null;
            }
            catch (Exception E)
            {
                LastError = E;
                return null;
            }
            return table;
        }

        public DataTable DTEval(SqlConnection connection, string sql, int timeout = 60)
        {
            LastError = null;
            if (DebugPoints.DebugPointRequested("SQL.DTEval"))
            {
                Debugger.Launch();
            }

            SqlCommand command = connection.CreateCommand();

            DataTable table = new DataTable();
            command.CommandType = CommandType.Text;
            command.CommandTimeout = timeout;
            command.CommandText = sql;
            while (command.Connection.State == ConnectionState.Connecting)
            {
                Thread.Sleep(1);
            }
            using (SqlDataReader reader = command.ExecuteReader())
            {
                table.Load(reader);
            }

            return table;
        }

        public void Exec(string sql, int timeout = 60)
        {
            LastError = null;
            if (DebugPoints.DebugPointRequested("SQL.Exec"))
            {
                Debugger.Launch();
            }

            try
            {
                CommandObject.CommandType = CommandType.Text;
                CommandObject.CommandTimeout = timeout;
                CommandObject.CommandText = sql;
                while (CommandObject.Connection.State == ConnectionState.Connecting)
                {
                    Thread.Sleep(1);
                }
                CommandObject.CommandText = sql;

                CommandObject.ExecuteNonQuery();
                LastError = null;
            }
            catch (Exception E)
            {
                LastError = E;
                return ;
            }
        }

        public void Exec(SqlConnection connection, string sql, int timeout = 60)
        {
            LastError = null;
            if (DebugPoints.DebugPointRequested("SQL.Exec"))
            {
                Debugger.Launch();
            }
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandTimeout = timeout;
            command.CommandText = sql;
            while (command.Connection.State == ConnectionState.Connecting)
            {
                Thread.Sleep(1);
            }
            command.ExecuteNonQuery();
        }

    }
}
