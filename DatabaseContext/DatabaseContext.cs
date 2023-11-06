using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using mYSelfERPWeb.Models;
using mYSelfERPWeb.ViewModels;


namespace mYSelfERPWeb.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        private string query;

        public DatabaseContext() : base("LocalSqlServer")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180; // seconds


        }

        #region Database Table With Model

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SubMenu> SubMenu { get; set; }
        public virtual DbSet<UserMenu> UserMenu { get; set; }
        public virtual DbSet<Models.Menu> Menu { get; set; }
        public virtual DbSet<NestedMenu> NestedMenu { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<SalaryGroup> SalaryGroup { get; set; }
        public virtual DbSet<SalaryBreakupElement> SalaryBreakupElement { get; set; }
        public virtual DbSet<SalaryDeductionElement> SalaryDeductionElement { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<SisterConcern> SisterConcern { get; set; }
        public virtual DbSet<SalaryBreakupElementPolicy> SalaryBreakupElementPolicy { get; set; }
        public virtual DbSet<SalaryBreakupElementPolicyDetails> SalaryBreakupElementPolicyDetails { get; set; }


        #endregion

        public DatabaseContext Query(string q)
        {

            this.query = q;
            return this;
        }

        public virtual void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public List<Dictionary<string, object>> ToDynamicList()
        {
            if (this.Database.Connection.State != ConnectionState.Open)
            { this.Database.Connection.Open(); }
            var cmd = this.Database.Connection.CreateCommand();
            cmd.CommandText = query;

            var reader = cmd.ExecuteReader();
            var rows = new List<Dictionary<string, object>>();

            while (reader.Read())
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var key = reader.GetName(i);
                    //var type = reader.GetType();
                    var value = reader.GetValue(i);
                    row.Add(key, value);
                }

                rows.Add(row);
                row = null;

            }

            reader.Close();

            try
            {
                if (this.Database.Connection.State == ConnectionState.Open)
                { this.Database.Connection.Close(); }
            }
            catch { }

            return rows;

        }

        public async Task<List<Dictionary<string, object>>> ToDynamicListAsync()
        {
            if (this.Database.Connection.State != ConnectionState.Open)
            { await this.Database.Connection.OpenAsync(); }

            var cmd = this.Database.Connection.CreateCommand();
            cmd.CommandText = query;

            var reader = await cmd.ExecuteReaderAsync();
            var rows = new List<Dictionary<string, object>>();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var key = reader.GetName(i);
                    //var type = reader.GetType();
                    var value = reader.GetValue(i);
                    row.Add(key, value);
                }

                rows.Add(row);
                row = null;

            }

            reader.Close();
            try
            {
                if (this.Database.Connection.State == ConnectionState.Open)
                { this.Database.Connection.Close(); }
            }
            catch { }

            return rows;

        }

    }
}