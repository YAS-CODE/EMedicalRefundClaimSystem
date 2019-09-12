using Dapper;
using EMedicalClaimRefundSystem.Areas.Identity.Data;
using EMedicalClaimRefundSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMedicalClaimRefundSystem.Services
{
    public class ERefundUserApplicationRepo : SqlRepository<ERefundUserApplication>, IERefundUserApplicationRepo
    {
        public  ERefundUserApplicationRepo(string connectionString): base(connectionString) { }
        public override async void DeleteAsync(int id)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "DELETE FROM ERefundUserApplication WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.Int32);
                await conn.QueryFirstOrDefaultAsync<ERefundUserApplication>(sql, parameters);
            }
        }

        public override async Task<IEnumerable<ERefundUserApplication>> GetAllAsync()
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "SELECT * FROM [EMedicalClaimRefundSystem].[dbo].[ERefundUserApplication] AS A INNER JOIN [EMedicalClaimRefundSystem].[dbo].[AspNetUsers] AS B ON A.UserId = B.Id";
                //var sql = "SELECT * FROM [EMedicalClaimRefundSystem].[dbo].[ERefundUserApplication]";               

                var result = conn.Query<ERefundUserApplication, ApplicationUser, ERefundUserApplication>(sql, (ERefundUserApplication, ApplicationUser) =>
                {
                    ERefundUserApplication.ApplicationUser = ApplicationUser;
                    return ERefundUserApplication;
                },                
                splitOn: "Id")
                .Distinct()
                .ToList();
                //var result = conn.Query<ERefundUserApplication>(sql, parameters);
                return result;
            }
        }

        public override async Task<ERefundUserApplication> GetAsync(int id)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "SELECT * FROM [EMedicalClaimRefundSystem].[dbo].[ERefundUserApplication] AS A INNER JOIN [EMedicalClaimRefundSystem].[dbo].[AspNetUsers] AS B ON A.UserId = B.Id where A.Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.Int32);
                var result = conn.Query<ERefundUserApplication, ApplicationUser, ERefundUserApplication>(sql, (ERefundUserApplication, ApplicationUser) =>
                {
                    ERefundUserApplication.ApplicationUser = ApplicationUser;
                    return ERefundUserApplication;
                },
                parameters,
                splitOn: "Id")
                .Distinct()
                .FirstOrDefault();
                //var result = conn.Query<ERefundUserApplication>(sql, parameters);
                return result;
            }
        }

        public IEnumerable<ERefundUserApplication> GetByAppStatus(int AppStatus)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "SELECT * FROM [EMedicalClaimRefundSystem].[dbo].[ERefundUserApplication] AS A INNER JOIN [EMedicalClaimRefundSystem].[dbo].[AspNetUsers] AS B ON A.UserId = B.Id where A.Status = @StatusId";
                //var sql = "SELECT * FROM [EMedicalClaimRefundSystem].[dbo].[ERefundUserApplication]";
                var parameters = new DynamicParameters();
                parameters.Add("@StatusId", AppStatus, System.Data.DbType.Int32);


                var result = conn.Query<ERefundUserApplication, ApplicationUser, ERefundUserApplication>(sql, (ERefundUserApplication, ApplicationUser) =>
                {
                    ERefundUserApplication.ApplicationUser = ApplicationUser;
                    return ERefundUserApplication;
                },
                parameters,
                splitOn: "Id")
                .Distinct()
                .ToList();
                //var result = conn.Query<ERefundUserApplication>(sql, parameters);
                return result;
            }
        }

        public IEnumerable<ERefundUserApplication> GetByUser(string UserId)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "SELECT * FROM [EMedicalClaimRefundSystem].[dbo].[ERefundUserApplication] AS A INNER JOIN [EMedicalClaimRefundSystem].[dbo].[AspNetUsers] AS B ON A.UserId = B.Id where A.UserId = @UserId";
                //var sql = "SELECT * FROM [EMedicalClaimRefundSystem].[dbo].[ERefundUserApplication]";
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", UserId, System.Data.DbType.String);


                var result = conn.Query<ERefundUserApplication, ApplicationUser, ERefundUserApplication>(sql, (ERefundUserApplication, ApplicationUser) =>
                {
                    ERefundUserApplication.ApplicationUser = ApplicationUser;
                    return ERefundUserApplication;
                },
                parameters,
                splitOn: "Id")
                .Distinct()
                .ToList();
                //var result = conn.Query<ERefundUserApplication>(sql, parameters);
                return result;
            }
        }

        public override async Task<int> InsertAsync(ERefundUserApplication entity)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "INSERT INTO ERefundUserApplication(PatientName,PhoneNumber,Address,Notes,DateTime,Status,UserId) "
                    + "VALUES(@PatientName,@PhoneNumber,@Address,@Notes,@DateTime,@Status,@UserId); SELECT CAST(SCOPE_IDENTITY() as int)";

                var parameters = new DynamicParameters();
                parameters.Add("@Notes", entity.Notes, System.Data.DbType.String);
                parameters.Add("@Status", entity.Status, System.Data.DbType.String);
                parameters.Add("@UserId", entity.UserId, System.Data.DbType.String);
                parameters.Add("@DateTime", entity.DateTime, System.Data.DbType.DateTime);
                parameters.Add("@PatientName", entity.PatientName, System.Data.DbType.String);
                parameters.Add("@PhoneNumber", entity.PhoneNumber, System.Data.DbType.String);
                parameters.Add("@Address", entity.Address, System.Data.DbType.String);

                var id = await conn.QueryAsync<int>(sql, parameters);
                return id.Single();
            }
        }

        public override async void UpdateAsync(ERefundUserApplication entityToUpdate)
        {
            using (var conn = GetOpenConnection())
            {
                var existingEntity = await GetAsync(entityToUpdate.Id);

                var sql = "UPDATE ERefundUserApplication "
                    + "SET ";

                var parameters = new DynamicParameters();
                if (existingEntity.PatientName != entityToUpdate.PatientName)
                {
                    sql += "PatientName=@PatientName,";
                    parameters.Add("@PatientName", entityToUpdate.PatientName, DbType.String);
                }
                if (existingEntity.PhoneNumber != entityToUpdate.PhoneNumber)
                {
                    sql += "PhoneNumber=@PhoneNumber,";
                    parameters.Add("@PhoneNumber", entityToUpdate.PhoneNumber, DbType.String);
                }
                if (existingEntity.Address != entityToUpdate.Address)
                {
                    sql += "Address=@Address,";
                    parameters.Add("@Address", entityToUpdate.Address, DbType.String);
                }
                if (existingEntity.Notes != entityToUpdate.Notes)
                {
                    sql += "Notes=@Notes,";
                    parameters.Add("@Notes", entityToUpdate.Notes, DbType.String);
                }

                if (existingEntity.Status != entityToUpdate.Status)
                {
                    sql += "Status=@Status,";
                    parameters.Add("@Status", entityToUpdate.Status, DbType.String);
                }
                if (existingEntity.UserId != entityToUpdate.UserId)
                {
                    sql += "UserId=@UserId,";
                    parameters.Add("@UserId", entityToUpdate.UserId, DbType.String);
                }
                if (existingEntity.DateTime != entityToUpdate.DateTime)
                {
                    sql += "DateTime=@DateTime,";
                    parameters.Add("@DateTime", entityToUpdate.DateTime, DbType.DateTime);
                }

                sql = sql.TrimEnd(',');

                sql += " WHERE Id=@Id";
                parameters.Add("@Id", entityToUpdate.Id, DbType.Int32);

                await conn.QueryAsync(sql, parameters);
            }
        }        

        public async void UpdateStatusAsync(int Id, int Status)
        {
            using (var conn = GetOpenConnection())
            {
                //var existingEntity = await GetAsync(Id);

                var sql = "UPDATE ERefundUserApplication "
                    + "SET ";

                var parameters = new DynamicParameters();
                //if (existingEntity.Notes != entityToUpdate.Notes)
                //{
                //    sql += "Notes=@Notes,";
                //    parameters.Add("@Notes", entityToUpdate.Notes, DbType.String);
                //}

                //if (existingEntity.Status != entityToUpdate.Status)
                //{
                sql += "Status=@Status,";
                parameters.Add("@Status", Status, DbType.String);
                //}
                //if (existingEntity.UserId != entityToUpdate.UserId)
                //{
                //    sql += "UserId=@UserId,";
                //    parameters.Add("@UserId", entityToUpdate.UserId, DbType.String);
                //}
                //if (existingEntity.DateTime != entityToUpdate.DateTime)
                //{
                //    sql += "DateTime=@DateTime,";
                //    parameters.Add("@DateTime", entityToUpdate.DateTime, DbType.DateTime);
                //}

                sql = sql.TrimEnd(',');

                sql += " WHERE Id=@Id";
                parameters.Add("@Id", Id, DbType.Int32);

                await conn.QueryAsync(sql, parameters);
            }
        }
    }
}
