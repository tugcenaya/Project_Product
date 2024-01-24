﻿namespace Project.Core.Repositories
{
public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId);
}
}