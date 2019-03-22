namespace TipoCambioSunat
{
    using Boxed.Mapping;
    using Microsoft.Extensions.DependencyInjection;
    using TipoCambioSunat.Commands;
    using TipoCambioSunat.Mappers;
    using TipoCambioSunat.Repositories;
    using TipoCambioSunat.Services;
    using TipoCambioSunat.ViewModels;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods add project services.
    /// </summary>
    /// <remarks>
    /// AddSingleton - Only one instance is ever created and returned.
    /// AddScoped - A new instance is created and returned for each request/response cycle.
    /// AddTransient - A new instance is created and returned each time.
    /// </remarks>
    public static class ProjectServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectCommands(this IServiceCollection services) =>
            services               
                .AddSingleton<IGetTipoCambioCommand, GetTipoCambioCommand>()
                .AddSingleton<IGetTipoCambioListCommand, GetTipoCambioListCommand>();

        public static IServiceCollection AddProjectMappers(this IServiceCollection services) =>
            services
                .AddSingleton<IMapper<Models.TipoCambio, TipoCambio>, TipoCambioToTipoCambioMapper>();

        public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
            services
                .AddSingleton<ITipoCambioRepository, TipoCambioRepository>();

        public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
            services
                .AddSingleton<IClockService, ClockService>();
    }
}
