namespace MultiThread
{
    using Ninject.Modules;
    using NLog;
    using Interfaces;
    using Classes;

    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IApp)).To(typeof(App));
            Bind<ILog>().ToMethod(x =>
            {
                var scope = x.Request.ParentRequest.Service.FullName;
                var log = (ILog)LogManager.GetLogger(scope, typeof(Log));
                return log;
            });
            Bind(typeof(IWriteFile)).To(typeof(FileWriter));
            Bind(typeof(IHandleDatabaseConnection)).To(typeof(DatabaseConnectionHandler));
            Bind(typeof(IWriteLoadsOfStringsToFile)).To(typeof(WriteLoadsOfStringsToFile));
            Bind(typeof(IHandleDatabase)).To(typeof(DatabaseHandler)).InThreadScope();
            Bind(typeof(IFactoryGeneric<>)).To(typeof(GenericFactory<>));
        }
    }
}
