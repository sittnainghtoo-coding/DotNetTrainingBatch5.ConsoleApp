using System.Data;
using System.Data.SqlClient;

namespace DotNetTrainingBatch5.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;
        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DataTable Query(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", 1);
            if (sqlParameters != null) 
            {
                foreach (var param in sqlParameters)
                {
                    command.Parameters.AddWithValue(param.Name, param.Value);
                }
            }
           
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
            return dt;
        }

        public int ExecuteQuery(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", 1);
            if (sqlParameters != null)
            {
                foreach (var param in sqlParameters)
                {
                    command.Parameters.AddWithValue(param.Name, param.Value);
                }
            }

            int result = command.ExecuteNonQuery();

            connection.Close();

            return result;
        }
    }

    public class SqlParameterModel
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public SqlParameterModel() { }

        public SqlParameterModel(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
