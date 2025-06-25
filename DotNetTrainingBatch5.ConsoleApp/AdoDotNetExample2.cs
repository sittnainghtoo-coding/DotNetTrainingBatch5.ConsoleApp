using DotNetTrainingBatch5.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class AdoDotNetExample2
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=DotNetTrainingBatch5;Integrated Security=True;TrustServerCertificate=True";
        private readonly AdoDotNetService _adoDotNetService;
        public AdoDotNetExample2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        public void Read()
        {
            string query = @"SELECT [BlogId]
                  ,[BlogTitle]
                  ,[BlogAuthor]
                  ,[BlogContent]
                  ,[DeleteFlag]
              FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            var dt = _adoDotNetService.Query(query);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }
        }

        public void Edit()
        {
            Console.WriteLine("Blog ID:");
            string id = Console.ReadLine();


            string query = $@"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            //SqlParameterModel[] sqlParameters = new SqlParameterModel[1];
            //sqlParameters[0] = new SqlParameterModel
            //{
            //    Name = "@BlogId",
            //    Value = id
            //};

            var dt = _adoDotNetService.Query(query, new SqlParameterModel
            {
                Name = "@BlogId",
                Value = id
            });

        }

        public void Create()
        {
            Console.WriteLine("Enter BlogTitle:");
            string title = Console.ReadLine();
            Console.WriteLine("Enter BlogAuthor:");
            string author = Console.ReadLine();
            Console.WriteLine("Enter BlogContent:");
            string content = Console.ReadLine();

            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                ([BlogTitle]
                ,[BlogAuthor]
                ,[BlogContent],[DeleteFlag])
    VALUES
                (@BlogTitle,@BlogAuthor,@BlogContent,0)";

           int result =  _adoDotNetService.ExecuteQuery(query, new SqlParameterModel("@BlogTitle",title),new SqlParameterModel("@BlogAuthor",author),new SqlParameterModel("@BlogContent",content));

            Console.WriteLine(result == 1 ? "Create Successful": "Create Failed");

        }

    }

   
}
