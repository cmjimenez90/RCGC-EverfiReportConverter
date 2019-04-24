using Autofac;

namespace RCGC.EverfiReportConverter
{
    public static class ContainerConfiguration
    {
        public static IContainer Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();

            return builder.Build();
        }
    }
}
