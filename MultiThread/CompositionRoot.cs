namespace MultiThread
{
    using Ninject;
    using Ninject.Modules;

    public class CompositionRoot
    {
        private static IKernel NinjectKernel;

        public static void Wire(INinjectModule module)
        {
            NinjectKernel = new StandardKernel(module);
        }

        public static T Resolve<T>()
        {
            return NinjectKernel.Get<T>();
        }
    }
}
