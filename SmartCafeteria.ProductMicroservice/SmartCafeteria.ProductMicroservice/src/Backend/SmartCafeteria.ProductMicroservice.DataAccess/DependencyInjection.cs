using Microsoft.Extensions.DependencyInjection;
using SmartCafeteria.ProductMicroservice.DataAccess.Repositories;
using SmartCafeteria.ProductMicroservice.DataAccess.RepositoryInterfaces;
using SmartCafeteria.ProductMicroservice.DataAccess.UnitOfWork;

namespace SmartCafeteria.ProductMicroservice.DataAccess;

public static class DependencyInjection
{
	public static  IServiceCollection AddDataAccess(this IServiceCollection services)
	{
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
		return services;
	}
}