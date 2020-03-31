using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Yame.FeatureTests.Dapper
{
    public class SqLiteCustomerRepository
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\SimpleDb.sqlite"; }
        }

        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        public SqLiteCustomerRepository()
        {
            if (File.Exists(DbFile))
            {
                File.Delete(DbFile);
            }
            CreateDatabase();
        }

        public void DapperDemo()
        {
            using (var cnn = SimpleDbConnection())
            {
                var sql = @"
                    INSERT INTO Customer 
                    ( FirstName, LastName, DateOfBirth ) 
                    VALUES 
                    ( @FirstName, @LastName, @DateOfBirth );
                    select last_insert_rowid()
                ";
                var customer = new Customer
                {
                    FirstName = "Sergey",
                    LastName = "Maskalik",
                    DateOfBirth = DateTime.Now
                };
                cnn.Open();
                //以下會新增五筆
                var Id1 = cnn.Query<long>(sql, customer).First();
                var Id2 = cnn.QueryFirst<long>(sql, customer);
                var Id3 = cnn.QueryFirstOrDefault<long>(sql, customer);
                var Id4 = cnn.QuerySingle<long>(sql, customer);
                var Id5 = cnn.QuerySingleOrDefault<long>(sql, customer);
                sql = @"
                    SELECT  Id, FirstName, LastName, DateOfBirth
                    FROM    Customer
                ";
                //確認沒有資料了
                var list = cnn.Query<Customer>(sql).ToList();
                var fiveData = list.Count == 5;

                sql = @"
                    UPDATE  Customer 
                    SET     FirstName = @firstName
                ";
                //更新全部的FirstName 為ABC
                var updateRow = cnn.Execute(sql, new { firstName = "ABC" });

                sql = @"
                    SELECT  COUNT(*) 
                    FROM    Customer;

                    SELECT  Id, FirstName, LastName, DateOfBirth
                    FROM    Customer
                    WHERE   Id = @Id;

                    SELECT  Id, FirstName, LastName, DateOfBirth
                    FROM    Customer
                    WHERE   Id > @Id;
                ";
                var p = new DynamicParameters();
                p.Add("@Id", 1);
                //一次執行 多個查詢 並取回內容
                var gridReader = cnn.QueryMultiple(sql, p);
                var totalCount1 = gridReader.ReadSingle<int>();
                var result1 = gridReader.ReadSingleOrDefault<Customer>();
                var result2 = gridReader.Read<Customer>();
                gridReader.Dispose();

                sql = @"
                    DELETE FROM Customer 
                    WHERE   Id > @Id
                ";
                //刪除內容
                var deleteRow = cnn.Execute(sql, new { Id = 0 });

                sql = @"
                    SELECT  Id, FirstName, LastName, DateOfBirth
                    FROM    Customer
                ";
                //確認沒有資料了
                list = cnn.Query<Customer>(sql).ToList();
                var noData = list.Any() == false;

                //新增多筆資料
                sql = @"
                    INSERT INTO Customer 
                    ( FirstName, LastName, DateOfBirth ) 
                    VALUES 
                    ( @FirstName, @LastName, @DateOfBirth );
                ";
                List<Customer> datas = new List<Customer>
                {
                    new Customer{ FirstName = "A",LastName="1", DateOfBirth=DateTime.Now},
                    new Customer{ FirstName = "B",LastName="2", DateOfBirth=DateTime.Now},
                    new Customer{ FirstName = "C",LastName="3", DateOfBirth=DateTime.Now}
                };
                //大量資料時 交易會提高效能
                using (var tran = cnn.BeginTransaction())
                {
                    cnn.Execute(sql, datas, tran);
                    tran.Commit();
                }

                sql = @"
                    UPDATE Customer SET FirstName=@FirstName WHERE LastName=@LastName
                ";
                datas = new List<Customer>
                {
                    new Customer{ FirstName = "AA",LastName="1"},
                    new Customer{ FirstName = "BB",LastName="2"},
                    new Customer{ FirstName = "CC",LastName="3"}
                };
                //大量資料時 交易會提高效能
                using (var tran = cnn.BeginTransaction())
                {
                    cnn.Execute(sql, datas, tran);
                    tran.Commit();
                }
                

                sql = @"
                    SELECT  Id, FirstName, LastName, DateOfBirth
                    FROM    Customer
                ";
                list = cnn.Query<Customer>(sql).ToList();
                var threeData = list.Count == 3;

                sql = @"
                    DELETE FROM Customer WHERE LastName=@LastName
                ";
                datas = new List<Customer>
                {
                    new Customer{LastName="1"},
                    new Customer{LastName="2"},
                    new Customer{LastName="3"}
                };
                cnn.Execute(sql, datas);

                sql = @"
                    SELECT  Id, FirstName, LastName, DateOfBirth
                    FROM    Customer
                ";
                list = cnn.Query<Customer>(sql).ToList();
                noData = list.Any() == false;
            }

            //交易
            using (var tranScope = new TransactionScope())
            {
                using (var cnn = SimpleDbConnection())
                {
                   //TODO
                }
                tranScope.Complete();
            }

            /*
              Stored Procedure
              using (SqlConnection conn = new SqlConnection(strConnection))
              {
                  //準備參數
                  DynamicParameters parameters = new DynamicParameters();
                  parameters.Add("@Param1", "abc",DbType.String, ParameterDirection.Input);
                  parameters.Add("@OutPut1", dbType: DbType.Int32,direction: ParameterDirection.Output);
                  parameters.Add("@Return1", dbType: DbType.Int32,direction: ParameterDirection.ReturnValue);
                  conn.Execute("MyStoredProcedure", parameters, commandType: CommandType.StoredProcedure);
                  //接回Output值
                  int outputResult = parameters.Get<int> ("@OutPut1");
                  //接回Return值
                  int returnResult = parameters.Get<int> ("@Return1");
              }
              */
        }

        private static void CreateDatabase()
        {
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                cnn.Execute(
                    @"create table Customer
                      (
                         ID                                  integer primary key AUTOINCREMENT,
                         FirstName                           varchar(100) not null,
                         LastName                            varchar(100) not null,
                         DateOfBirth                         datetime not null
                      )");
            }
        }
    }
}
