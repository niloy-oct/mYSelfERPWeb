using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace mYSelfERPWeb
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DatabaseContext.DatabaseContext _dbContext;

        public MenuRepository(DatabaseContext.DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Dictionary<string, object>> GetMenuList()
        {
            try
            {
                string sql = $"GetMenuList '{Sessions.Name.RoleId}'";
                var data = _dbContext.Query(sql).ToDynamicList();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Dictionary<string, object>> GetSubMenuList()
        {
            try
            {
                string sql = $"GetSubMenuList '{Sessions.Name.RoleId}'";
                var data = _dbContext.Query(sql).ToDynamicList();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Dictionary<string, object>> GetNestedMenuList()
        {
            try
            {
                string sql = $"GetNestedMenuList '{Sessions.Name.RoleId}'";
                var data = _dbContext.Query(sql).ToDynamicList();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public List<Dictionary<string, object>> GetModuleList()
        {
            try
            {
                string sql = $"GetModuleList '{Sessions.Name.RoleId}'";
                var data = _dbContext.Query(sql).ToDynamicList();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }
    }
}