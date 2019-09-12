using Dapper;
using EMedicalClaimRefundSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EMedicalClaimRefundSystem.Services
{
    public class ApplicationHistoryRepo : SqlRepository<ApplicationHistory>, IApplicationHistoryRepo
    {
        public ApplicationHistoryRepo(string connectionString) : base(connectionString) { }
        public override async void DeleteAsync(int id)
        {
            using (var conn = GetOpenConnection())
            {
                var
                    sql = "DELETE FROM ApplicationHistory WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.Int32);
                await conn.QueryFirstOrDefaultAsync<ApplicationHistory>(sql, parameters);
            }
        }

        public override async Task<IEnumerable<ApplicationHistory>> GetAllAsync()
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "SELECT * FROM ApplicationHistory  AS A INNER JOIN AspNetUsers AS B ON A.UserId = B.UserId;";
                return await conn.QueryAsync<ApplicationHistory>(sql);
            }
        }

        public override async Task<ApplicationHistory> GetAsync(int id)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "SELECT * FROM ApplicationHistory WHERE Id = @Id  AS A INNER JOIN AspNetUsers AS B ON A.UserId = B.UserId;";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.Int32);
                return await conn.QueryFirstOrDefaultAsync<ApplicationHistory>(sql, parameters);
            }
        }

        public Task<IEnumerable<ApplicationHistory>> GetByUserAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<int> InsertAsync(ApplicationHistory entity)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "INSERT INTO ApplicationHistory(Status,DateTime,UserId) "
                    + "VALUES(@Status,@DateTime,@UserId); SELECT CAST(SCOPE_IDENTITY() as int)";

                var parameters = new DynamicParameters();
                parameters.Add("@Status", entity.Status, System.Data.DbType.Boolean);
                parameters.Add("@DateTime", entity.Status, System.Data.DbType.DateTime);
                parameters.Add("@UserId", entity.Status, System.Data.DbType.String);

                var id = await conn.QueryAsync(sql, parameters);
                return id.Single();

            }
        }

        public override async void UpdateAsync(ApplicationHistory entityToUpdate)
        {
            using (var conn = GetOpenConnection())
            {
                var existingEntity = await GetAsync(entityToUpdate.Id);

                var sql = "UPDATE ApplicationHistory "
                    + "SET ";

                var parameters = new DynamicParameters();
                if (existingEntity.Status != entityToUpdate.Status)
                {
                    sql += "Status=@Status,";
                    parameters.Add("@Status", entityToUpdate.Status, DbType.Boolean);
                }
                if (existingEntity.DateTime != entityToUpdate.DateTime)
                {
                    sql += "DateTime=@DateTime,";
                    parameters.Add("@DateTime", entityToUpdate.DateTime, DbType.DateTime);
                }
                if (existingEntity.UserId != entityToUpdate.UserId)
                {
                    sql += "UserId=@UserId,";
                    parameters.Add("@UserId", entityToUpdate.UserId, DbType.String);
                }

                sql = sql.TrimEnd(',');

                sql += " WHERE Id=@Id";
                parameters.Add("@Id", entityToUpdate.Id, DbType.Int32);

                await conn.QueryAsync(sql, parameters);
            }
        }        
    }
}
