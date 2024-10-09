using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System.Configuration;
using System.Reflection;


namespace KAFKA_PARSE
{

    public class MyDbContext : SqlSugarScope
    {
        private readonly IConfiguration _Config;
        public MyDbContext(IConfiguration config) :
            base(new ConnectionConfig()
            {
                DbType = DbType.SqlServer,
                ConnectionString = config.GetConnectionString("DB"),
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    EntityNameService = (type, entity) =>
                    {

                    }
                }
            })
        {
            _Config = config;
        }
        //public  static class MyDbContext
        //{


        //    public static IServiceCollection AddSqlSugar(this IServiceCollection services, IConfiguration configuration)
        //    {
        //        services.AddScoped(provider =>
        //        {

        //            var connectionString = configuration.GetConnectionString("DB");
        //            if (string.IsNullOrWhiteSpace(connectionString))
        //            {
        //                throw new InvalidOperationException("数据库连接字符串未配置");
        //            }

        //            return new SqlSugarClient(new ConnectionConfig()
        //            {
        //                ConnectionString = connectionString,
        //                DbType = DbType.SqlServer // 根据你的数据库类型进行调整  
        //                //IsAutoCloseConnection = true, // 注意：这个设置可能不是必需的，因为连接池会管理连接的打开和关闭  
        //                                              // 其他配置...  
        //            });
        //        });

        //        // 如果你有一个 ISqlSugarClient 接口，并且想要显式地注册它，你可以这样做：  
        //        // services.AddScoped<ISqlSugarClient>(provider =>  
        //        // {  
        //        //     // ... 同上，但返回的是实现了 ISqlSugarClient 接口的实例（如果 SqlSugarClient 已经实现了它）  
        //        // });  

        //        return services;
        //    }

        //} 


    }
}