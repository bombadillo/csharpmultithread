namespace MultiThread.Classes
{
    using Interfaces;
    using Ninject;
    using System.Reflection;

    public class GenericFactory<T> : IFactoryGeneric<T>
    {
        private IKernel Kernel;

        public T Create()
        {
            Kernel = new StandardKernel();
            Kernel.Load(Assembly.GetExecutingAssembly());

            return Kernel.Get<T>();
        }
    }
}
