using SignalRCRUDPractice.TableDepenedency;

namespace SignalRCRUDPractice.MiddlewareExtension
{
    public static class ApplicationBuilderExtension
    {
        public static void UseProductTableDependency(this IApplicationBuilder _applicationBuilder,string _conString)
        {
            
            var serviceProvider = _applicationBuilder.ApplicationServices;
            //getting the tabledependency service which we have added in program.cs in builder.services.add
            var service=serviceProvider.GetService<TableDependencyClass>();
            service.ProductTableDependency(_conString);
        }
    }
}


//in future we will be having multiple middleware per each sql table so for that we can create one generic method
/*
 public static void commonTableDependency<T>(this IApplicationBuilder _appbuilder,string ConString)
where T: ItableDependency(Interface commong method like we have void tabledependencyClass)
{
    var serviceProvider=_appbuilder.ApplicationServices;
     var service=serviceProvider.GetService<T>();
    service.ProductTableDependency(Constring);
}
 */
